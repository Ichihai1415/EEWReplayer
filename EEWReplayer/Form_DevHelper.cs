using AngleSharp.Html.Parser;
using EEWReplayer.Utils;
using System.Text.Json;
using System.Xml.Serialization;
using static EEWReplayer.Utils.Common;
using static EEWReplayer.Utils.DataConverter;
using static JmaXmlViewer.Utilities.XmlClass_XSD;

namespace EEWReplayer.Devs
{
    public partial class Form_DevHelper : Form
    {
        public Form_DevHelper()
        {
            InitializeComponent();
        }

        private async void Form_DevHelper_Load(object sender, EventArgs e)
        {
            //GetAllEEW();
            //JMAXML2OriginalJSON();
            //StatisticsMaker();
            //DataMerger();

            await Draw.DrawFlowEx();
        }

        internal void ChangeImage(Image img)
        {
            Img.Image = img;
        }

        private static async void GetAllEEW()
        {
            Directory.CreateDirectory("Test");

            //var d_new = await GetData.GetDetail("https://www.data.jma.go.jp/eew/data/nc/pub_hist/2024/01/20240101161010/fc/index.html");
            //File.WriteAllText("Test\\d_new.json", JsonSerializer.Serialize(d_new,options));
            //var d_old = await GetData.GetDetail("https://www.data.jma.go.jp/eew/data/nc/pub_hist/2022/03/20220316233646/content/content_out.html");
            //File.WriteAllText("Test\\d_old.json", JsonSerializer.Serialize(d_old,options));
            //var d_s0 = await GetData.GetDetail("https://www.data.jma.go.jp/eew/data/nc/pub_hist/2009/08/20090825063712/content/content_out.html");
            //File.WriteAllText("Test\\d_s0.json", JsonSerializer.Serialize(d_s0, options));

            //var d_sa1 = await GetData.GetDetail("https://www.data.jma.go.jp/eew/data/nc/fc_hist/2025/11/20251109170343/index.html");
            //File.WriteAllText("Test\\d_sa1.json", JsonSerializer.Serialize(d_sa1, Form1.options));
            //var d_sa2 = await GetData.GetDetail("https://www.data.jma.go.jp/eew/data/nc/fc_hist/2025/11/20251109170446/index.html");
            //File.WriteAllText("Test\\d_sa2.json", JsonSerializer.Serialize(d_sa2, Form1.options));


            //var d = await GetData.GetDetail("https://www.data.jma.go.jp/eew/data/nc/pub_hist/2008/06/20080614084350/content/content_out.html");

            ////var d = await GetData.GetDetail("https://www.data.jma.go.jp/eew/data/nc/fc_hist/2025/10/20251019222354/index.html");

            //var da = d.EEWLists[0].GetAllWarningAreas();
            //var db = d.EEWLists[0].GetAllWarningAreas(true);
            //var dc = d.EEWLists[0].GetAllWarningAreas(4);
            //var dd = d.EEWLists[0].GetAllWarningAreas(1);

            //File.WriteAllText("Test\\d.json", JsonSerializer.Serialize(d, Form1.options));
            //return;

            var client = new HttpClient();
            var parser = new HtmlParser();

            var response = await client.GetStringAsync("https://www.data.jma.go.jp/eew/data/nc/pub_hist/index.html");
            var document = await parser.ParseDocumentAsync(response);

            var eqTables = document.QuerySelectorAll("table[id='event_list']");
            var eqRows = eqTables.Select(x => x.QuerySelectorAll("tr").Skip(1).ToArray());
            var detailURLs = new List<string>();
            foreach (var eqRow in eqRows.SelectMany(x => x))
            {
                var eqCells = eqRow.QuerySelectorAll("td");
                var detailUrl = eqCells[4].QuerySelector("a")?.GetAttribute("href")!.Replace("./", "https://www.data.jma.go.jp/eew/data/nc/pub_hist/").Replace("reachtime/reachtime.html", "fc/index.html")!;
                detailURLs.Add(detailUrl);
                Form1.f2.AddLine(eqCells[0].TextContent + "　" + detailUrl);
                var data = await GetData.GetDetail(detailUrl);
                Directory.CreateDirectory("datas");
                File.WriteAllText("datas\\" + detailUrl.Split('/')[9] + ".json", JsonSerializer.Serialize(data, Form1.options));
            }
        }


        private static void JMAXML2OriginalJSON()
        {

            var serializer = new XmlSerializer(typeof(C_Report)) ?? throw new Exception("XmlSerializerの初期化に失敗しました。");
            var dir = @"D:\Ichihai1415\data\xml\dmdss\";
            //var dir = @"C:\Users\proje\Downloads\dmdss\";
            var filesA = new string[] { "VXSE45_RJTD_20240101071016_2e3c66b", "VXSE45_RJTD_20240101071016_cbd2993", "VXSE45_RJTD_20240101071040_a87ff13", "VXSE45_RJTD_20240101071043_c1d2523" };
            var filesB = new string[] { "VXSE45_RJTD_20251105010000_03043bf", "VXSE45_RJTD_20251105010001_1951c50", "VXSE45_RJTD_20251105010002_ba86932", "VXSE45_RJTD_20251105010003_b71c135", "VXSE45_RJTD_20251105010004_d1e7a2e", "VXSE45_RJTD_20251105010005_807b072", "VXSE45_RJTD_20251105010006_fe70bb8" };

            var files = new string[] { "VXSE45_JPOS_20250317200042_49acff6" };

            foreach (var fileName in files.Concat(filesA).Concat(filesB))
            {
                var xmlSt = File.ReadAllText(dir + fileName + ".xml");
                using var reader = new StringReader(xmlSt);
                var report = (C_Report?)serializer.Deserialize(reader) ?? throw new Exception("Feedの取得に失敗しました。");
                var (lat, lon, depth) = JMAXML_LatLonDepthConverter(report.Body_seismology1.Earthquake[0].Hypocenter.Area.Coordinate[0].Value);
                var warnHead = report.Head.Headline.Information.FirstOrDefault(x => x.type == "緊急地震速報（細分区域）");
                var isWarn = false;
                foreach (var item in warnHead?.Item ?? [])
                {
                    if (item.Kind[0].Name != item.LastKind[0].Name)
                    {
                        isWarn = true;
                        break;
                    }
                }

                var intensityAreas_dict = new Dictionary<DetailedIntensity, List<(string areaName, int areaCode)>>();
                var intensityLgAreas_dict = new Dictionary<DetailedIntensity, List<(string areaName, int areaCode)>>();
                foreach (var area in report.Body_seismology1.Intensity.Forecast.Pref.Select(p => p.Area))
                {
                    var maxInt = IntensityD_JMAxmlString2Enum(area[0].ForecastInt.From, area[0].ForecastInt.To);
                    var maxIntLg = area[0].ForecastLgInt == null ? new DetailedIntensity() : IntensityLgD_JMAxmlString2Enum(area[0].ForecastLgInt.From, area[0].ForecastLgInt.To);
                    var areaName = area[0].Name;
                    var areaCode = int.TryParse(area[0].Code, out var c) ? c : 0;
                    if (!intensityAreas_dict.ContainsKey(maxInt))
                        intensityAreas_dict[maxInt] = [];
                    if (!intensityLgAreas_dict.ContainsKey(maxIntLg))
                        intensityLgAreas_dict[maxIntLg] = [];
                    if (!maxInt.IsNull)
                        intensityAreas_dict[maxInt].Add((areaName, areaCode));
                    if (!maxIntLg.IsNull)
                        intensityLgAreas_dict[maxIntLg].Add((areaName, areaCode));
                }

                var data = new Data()
                {
                    Version = Form1.VERSION,
                    Created = DateTime.Now,
                    Description = "気象庁防災情報XMLから生成。",
                    ID = "jma-xml_" + report.Head.EventID,
                    Earthquakes = [],
                    EEWLists = [new Data.EEWList([new Data.EEWList.EEW()
                    {
                        Serial = int.TryParse(report.Head.Serial, out var s) ? s : 0,
                        UpdateTime = report.Head.ReportDateTime,
                        OriginTime = report.Body_seismology1.Earthquake[0].OriginTime,
                        HypoName = report.Body_seismology1.Earthquake[0].Hypocenter.Area.Name,
                        HypoLat = (double)lat!,
                        HypoLon = (double)lon!,
                        HypoDepth = (double)depth!,
                        Magnitude = report.Body_seismology1.Earthquake[0].Magnitude[0].Value,
                        IsWarn = isWarn,
                        MaxIntensityD = IntensityD_JMAxmlString2Enum(report.Body_seismology1.Intensity.Forecast.ForecastInt.From, report.Body_seismology1.Intensity.Forecast.ForecastInt.To),
                        MaxIntensityLgD = report.Body_seismology1.Intensity.Forecast.ForecastLgInt == null ? new DetailedIntensity() : IntensityLgD_JMAxmlString2Enum(report.Body_seismology1.Intensity.Forecast.ForecastLgInt.From, report.Body_seismology1.Intensity.Forecast.ForecastLgInt.To),
                        IntensityAreas = SortIntensityAreas(intensityAreas_dict),
                        IntensityLgAreas = SortIntensityAreas(intensityLgAreas_dict),

                    }],"JMA-XML", report.Head.EventID)]
                };

                Directory.CreateDirectory("datasx");
                File.WriteAllText("datasx\\" + fileName + "-" + data.EEWLists[0].ID + "-" + data.EEWLists[0].EEWs[0].Serial + ".json", JsonSerializer.Serialize(data, Form1.options));
            }
            Console.WriteLine();
        }


        public const string DIR = "datas";
        private static void StatisticsMaker()
        {
            var jsonFiles = Directory.EnumerateFiles(DIR, "*.json", SearchOption.AllDirectories);
            var datas = new List<Data>();
            var codeCounter = new Dictionary<int, int>();
            foreach (var kvp in ConvertSource.AreaForecastE_Code2Name)
                codeCounter[kvp.Key] = 0;
            foreach (string filePath in jsonFiles)
            {
                var jsonString = File.ReadAllText(filePath);
                var json = JsonSerializer.Deserialize<Data>(jsonString, Form1.options)!;
                datas.Add(json);
                //var warnLastEEW = json.EEWLists[0].EEWs.Where(x => x.IsWarn == true).Last();
                //var (warnAreas, warnCodes) = warnLastEEW.GetWarningAreas();
                var a = json.EEWLists[0].GetAllWarningAreas();

                Form1.f2.AddLine(json.Earthquakes[0].OriginTime + " " + json.Earthquakes[0].HypoName + ((a.Length == 0) ? "" : "\n"));
                foreach (var warnAreas in a)
                    Form1.f2.AddLine(string.Join(' ', warnAreas.areaNames!) + "\n");
                Form1.f2.AddLine("---");
                foreach (var code in a[^1].areaCodes ?? [])
                    if (codeCounter.TryGetValue(code, out int value))
                        codeCounter[code] = ++value;
            }
            Console.WriteLine("Done");
            Form1.f2.AddLine("warn: codes");
            foreach (var kvp in codeCounter.OrderBy(x => x.Key))
                Form1.f2.AddLine($"{kvp.Key} {ConvertSource.AreaForecastE_Code2Name[kvp.Key]}: {kvp.Value}");
            Form1.f2.AddLine("---\nwarn: codes");
            foreach (var kvp in codeCounter.OrderByDescending(x => x.Value))
                Form1.f2.AddLine($"{kvp.Key} {ConvertSource.AreaForecastE_Code2Name[kvp.Key]}: {kvp.Value}");

        }

        private static void DataMerger()
        {
            string mdir = "data-merge\\sa";
            var jsonFiles = Directory.EnumerateFiles(mdir, "*.json", SearchOption.AllDirectories);
            var datas = new List<Data>();
            foreach (string filePath in jsonFiles)
            {
                var jsonString = File.ReadAllText(filePath);
                Form1.f2.AddLine(filePath);
                var json = JsonSerializer.Deserialize<Data>(jsonString, Form1.options)!;
                datas.Add(json);
            }
            var data = datas.First().DeepCopy();
            for (int i = 1; i < datas.Count; i++)
                data.AddEarthquakeEEW(datas[i]);
            File.WriteAllText(mdir + "\\" + data.ID + ".json", JsonSerializer.Serialize(data, Form1.options));

        }
    }
}
