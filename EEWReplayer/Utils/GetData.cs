using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using static EEWReplayer.Utils.Common;
using static EEWReplayer.Utils.DataConverter;

namespace EEWReplayer.Utils
{
    public class GetData
    {
        public static readonly HttpClient client = new();
        public static readonly HtmlParser parser = new();

        public static async Task<Data> GetDetail(string url)
        {
            Console.WriteLine("取得中...");
            var response = await client.GetStringAsync(url);
            Console.WriteLine("解析中...");
            var document = await parser.ParseDocumentAsync(response);

            Console.WriteLine("処理中[1/3]...");
            var eqInfo_ = new List<Data.Earthquake>();
            var hypoTable = document.QuerySelectorAll("table[id='hypocentral_element_list']");
            var hypoRows = hypoTable[0].QuerySelectorAll("tr").Skip(1);
            var mainOriginTime = DateTime.MinValue;
            var mainHypoName = "";
            var magTemp = -1d;
            foreach (var hypoRow in hypoRows)
            {
                var hypoCells = hypoRow.QuerySelectorAll("td");
                if (hypoCells.Length > 0)
                {
                    var originTimeSt = hypoCells[0].TextContent.Replace("元", "1").Replace("月", "/").Replace("日", " ").Replace("時", ":").Replace("分", ":").Replace("秒", "");//令和 6年06月03日06時31分40.3秒
                    var originTimeSts = originTimeSt.Split("年");
                    if (originTimeSt.EndsWith(':'))//速報値の時は分で終わる
                        originTimeSts[1] = originTimeSts[1][..^1];
                    var year = originTimeSts[0].StartsWith("平成") ? int.Parse(originTimeSts[0].Replace("平成 ", "200").Replace("平成", "20")) - 12
                        : originTimeSts[0].StartsWith("令和") ? int.Parse(originTimeSts[0].Replace("令和 ", "200").Replace("令和", "20")) + 18 : 3000;
                    var originTime = DateTime.Parse(year + "/" + originTimeSts[1]);
                    var info = new Data.Earthquake
                    {
                        OriginTime = originTime,
                        HypoName = hypoCells[1].TextContent.Replace("(", "").Replace(")", ""),
                        HypoLat = LatLon60to10(hypoCells[2].TextContent),
                        HypoLon = LatLon60to10(hypoCells[3].TextContent),
                        HypoDepth = double.Parse(hypoCells[4].TextContent.Replace("km", "")),
                        Magnitude = hypoCells[5].TextContent == "---" || hypoCells[5].TextContent == "不明" ? double.NaN : double.Parse(hypoCells[5].TextContent),
                        MaxIntensity = ConvertSource.Shindo_StringEnum[hypoCells[6].TextContent]
                    };
                    if (double.IsNormal(info.Magnitude))
                        if (info.Magnitude > magTemp)
                        {
                            mainOriginTime = info.OriginTime;
                            mainHypoName = info.HypoName;
                            magTemp = info.Magnitude;
                        }
                    eqInfo_.Add(info);
                }
            }

            Console.WriteLine("処理中[2/3]...");
            var intAreas = new Dictionary<string, List<Data.EEWList.EEW.IntensityArea>>();
            var estIntTable = document.QuerySelector("table.eew_estimate_intensity_list");
            var estIntRows = new List<IElement>();
            if (estIntTable != null)//旧版
                estIntRows = [.. estIntTable.QuerySelectorAll("tr")];
            else//新版
            {
                var estIntTablesNew = document.QuerySelectorAll("table.eew_estimate_intensity_lg_list");
                foreach (var estIntTableNew in estIntTablesNew)
                    estIntRows.AddRange([.. estIntTableNew.QuerySelectorAll("tr")]);
            }

            var serialLast = "";
            var intLast = new DetailedIntensity();
            foreach (var estIntRow in estIntRows)
            {
                var intCells = estIntRow.QuerySelectorAll("td");
                if (intCells.Length == 3)
                {
                    var serialSt = intCells[0].TextContent;
                    var intensity = new DetailedIntensity(intCells[1].TextContent);
                    var areas = intCells[2].TextContent;
                    serialLast = serialSt;
                    intLast = intensity;

                    if (!intAreas.ContainsKey(serialSt))//新版はすべて取ると重複の可能性あるため
                    {
                        intAreas.Add(serialSt,
                        [
                            new()
                            {
                                MaxIntensityD = intensity,
                                AreaNames = areas.Split('，'),
                                AreaCodes = AreasSt2Ints(areas, '、')
                            }
                        ]);
                    }
                }
                else if (intCells.Length == 2)//番号同じ
                {
                    var intensity = new DetailedIntensity(intCells[0].TextContent);
                    var areas = intCells[1].TextContent;
                    intLast = intensity;

                    if (!intAreas[serialLast].Any(existInt => existInt.MaxIntensityD == intensity))//新版はすべて取ると重複の可能性あるため
                        intAreas[serialLast].Add(new()
                        {
                            MaxIntensityD = intensity,
                            AreaNames = areas.Split('，'),
                            AreaCodes = AreasSt2Ints(areas, '、')
                        });
                }
                else if (intCells.Length == 2)//震度同じ
                {
                    var areas = intCells[0].TextContent;

                    for (int i = intAreas[serialLast].Count; i >= 0; i--)//#1,#2...と増えてくから後ろからのほうが良い　Lastだと新版で重複したものが混入するはずだからｘ
                    {
                        var intArea = intAreas[serialLast][i];
                        if (intArea.MaxIntensityD == intLast)
                        {
                            intAreas[serialLast][i].AreaNames = [.. intAreas[serialLast][i].AreaNames.Concat(areas.Split('，'))];
                            break;
                        }
                    }

                }
            }

            Console.WriteLine("処理中[3/3]...");
            var eew = new List<Data.EEWList.EEW>();
            var eewListTable = document.QuerySelector("table[id='information_list']") ?? throw new Exception("テーブルの取得に失敗しました。");
            var eewListRows = eewListTable.QuerySelectorAll("tr").Skip(3);
            //.Where(row => !row.ClassList.Contains("hide-tr") && !row.ClassList.Contains("odd"));//新版の展開式震度一覧を除外(うまくいかない)(ClassListに出ないから後付け？)

            var detectTime = DateTime.MinValue;
            foreach (var eewRow in eewListRows)
            {
                if (eewRow.GetAttribute("class") == "hide-tr")
                    continue;
                var eewCells = eewRow.QuerySelectorAll("td");
                if (eewCells.Length == 8)//新版の展開式震度一覧を除外
                {
                    if (detectTime == DateTime.MinValue)
                    {
                        detectTime = DateTime.Parse(eqInfo_[0].OriginTime.ToString("yyyy/MM/dd ") + eewCells[1].TextContent.Replace("時", ":").Replace("分", ":").Replace("秒", ""));
                        continue;
                    }

                    var intRef = eewCells[7].TextContent;
                    var intArea = intRef.StartsWith('※') ? intAreas[intRef] :
                    [
                        new()
                        {
                            MaxIntensityD = new DetailedIntensity(intRef),
                            AreaNames = ["(エリアなし)"],
                            AreaCodes = []
                        }
                    ];
                    var info = new Data.EEWList.EEW
                    {
                        Serial = int.Parse(eewCells[0].TextContent),
                        UpdateTime = detectTime.AddMilliseconds((int)(1000 * double.Parse(eewCells[2].TextContent))),
                        OriginTime = mainOriginTime,
                        HypoName = "(" + mainHypoName + ")",
                        HypoLat = double.Parse(eewCells[3].TextContent),
                        HypoLon = double.Parse(eewCells[4].TextContent),
                        HypoDepth = double.Parse(eewCells[5].TextContent.Replace("km", "")),
                        Magnitude = eewCells[6].TextContent == "---" || eewCells[6].TextContent == "不明" ? double.NaN : double.Parse(eewCells[6].TextContent),
                        IsWarn = eewRow.ClassList.Contains("eew_public_warning_row"),
                        MaxIntensity = intArea.First().MaxIntensityD.Max,
                        IntensityAreas = [.. intArea]
                    };
                    eew.Add(info);
                }
            }
            //Console.WriteLine("完了");
            return new Data
            {
                Description = "気象庁ホームページ(緊急地震速報(予報)の内容)より生成,通常は暫定値ですが、新しく未更新の場合は速報値となります,各報での震央名、発生時刻は実際の地震(複数ある場合は通常一番大きなもの)の値となり、震央名にはかっこがつきます,詳細: " + url,
                Created = DateTime.Now,
                Version = Form1.VERSION,
                Earthquakes = [.. eqInfo_],
                EEWLists = [new Data.EEWList([.. eew], "JMA-WEB")]
            };
        }
    }
}
