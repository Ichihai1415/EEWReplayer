using AngleSharp.Browser;
using AngleSharp.Html.Parser;
using EEWReplayer.Utils;
using System.Text.Json;
using System.Xml.Serialization;
using static EEWReplayer.Utils.Common;
using static EEWReplayer.Utils.DataConverter;
using static JmaXmlViewer.Utilities.XmlClass_XSD;
using static System.Windows.Forms.Design.AxImporter;

namespace EEWReplayer.Devs
{
    public partial class Form_DevHelper : Form
    {
        public Form_DevHelper()
        {
            InitializeComponent();
        }

        private void Form_DevHelper_Load(object sender, EventArgs e)
        {
            GetAllEEW();
            JMAXML2OriginalJSON();
        }

        private async static void GetAllEEW()
        {
            Directory.CreateDirectory("Test");

            //var d_new = await GetData.GetDetail("https://www.data.jma.go.jp/eew/data/nc/pub_hist/2024/01/20240101161010/fc/index.html");
            //File.WriteAllText("Test\\d_new.json", JsonSerializer.Serialize(d_new,options));
            //var d_old = await GetData.GetDetail("https://www.data.jma.go.jp/eew/data/nc/pub_hist/2022/03/20220316233646/content/content_out.html");
            //File.WriteAllText("Test\\d_old.json", JsonSerializer.Serialize(d_old,options));
            //var d_s0 = await GetData.GetDetail("https://www.data.jma.go.jp/eew/data/nc/pub_hist/2009/08/20090825063712/content/content_out.html");
            //File.WriteAllText("Test\\d_s0.json", JsonSerializer.Serialize(d_s0, options));


            var d = await GetData.GetDetail("https://www.data.jma.go.jp/eew/data/nc/fc_hist/2025/10/20251019222354/index.html");
            File.WriteAllText("Test\\d.json", JsonSerializer.Serialize(d, Form1.options));
            return;
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
                Form1.f.AddLine(eqCells[0].TextContent + "　" + detailUrl);
                var data = await GetData.GetDetail(detailUrl);
                Directory.CreateDirectory("datas");
                File.WriteAllText("datas\\" + detailUrl.Split('/')[9] + ".json", JsonSerializer.Serialize(data, Form1.options));
            }
        }


        private static void JMAXML2OriginalJSON()
        {

            var serializer = new XmlSerializer(typeof(C_Report)) ?? throw new Exception("XmlSerializerの初期化に失敗しました。");
            var dir = @"D:\Ichihai1415\data\json\dmdss\";
            //var dir = @"C:\Users\proje\Downloads\dmdss\";
            var filesA = new string[] { "VXSE45_RJTD_20240101071016_2e3c66b", "VXSE45_RJTD_20240101071016_cbd2993", "VXSE45_RJTD_20240101071040_a87ff13", "VXSE45_RJTD_20240101071043_c1d2523" };
            var filesB = new string[] { "VXSE45_RJTD_20251105010000_03043bf", "VXSE45_RJTD_20251105010001_1951c50", "VXSE45_RJTD_20251105010002_ba86932", "VXSE45_RJTD_20251105010003_b71c135", "VXSE45_RJTD_20251105010004_d1e7a2e", "VXSE45_RJTD_20251105010005_807b072", "VXSE45_RJTD_20251105010006_fe70bb8" };

            var files = new string[] { "VXSE45_JPOS_20250317200042_49acff6" };

            foreach (var fileName in files)
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
                foreach (var area in report.Body_seismology1.Intensity.Forecast.Pref.Select(p => p.Area))
                {
                    var maxInt = IntensityD_JMAxmlString2Enum(area[0].ForecastInt.From, area[0].ForecastInt.To);
                    var maxIntLg = area[0].ForecastLgInt == null ? new DetailedIntensity() : IntensityLgD_JMAxmlString2Enum(area[0].ForecastLgInt.From, area[0].ForecastLgInt.To);
                    var areaName = area[0].Name;
                    var areaCode = int.TryParse(area[0].Code, out var c) ? c : 0;
                    if (!intensityAreas_dict.ContainsKey(maxInt))
                        intensityAreas_dict[maxInt] = [];
                    if (!intensityAreas_dict.ContainsKey(maxIntLg))
                        intensityAreas_dict[maxIntLg] = [];
                    intensityAreas_dict[maxInt].Add((areaName, areaCode));
                    if (maxIntLg.IsNull)
                        intensityAreas_dict[maxIntLg].Add((areaName, areaCode));
                }


                var data = new Data()
                {
                    Version = Form1.VERSION,
                    Created = DateTime.Now,
                    Description = "気象庁防災情報XMLから作成",
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
                        MaxIntensityLgD = report.Body_seismology1.Intensity.Forecast.ForecastLgInt==null ? new DetailedIntensity() : IntensityLgD_JMAxmlString2Enum(report.Body_seismology1.Intensity.Forecast.ForecastLgInt.From, report.Body_seismology1.Intensity.Forecast.ForecastLgInt.To),
                        IntensityAreas = SortIntensityAreas(intensityAreas_dict),

                    }], report.Head.EventID)]
                };

                Directory.CreateDirectory("datasx");
                File.WriteAllText("datasx\\" + fileName + "_" + data.EEWLists[0].ID + "-" + data.EEWLists[0].EEWs[0].Serial + ".json", JsonSerializer.Serialize(data, Form1.options));
            }
            Console.WriteLine();
        }

        private static void JSONMarger()
        {
        }
    }
}
