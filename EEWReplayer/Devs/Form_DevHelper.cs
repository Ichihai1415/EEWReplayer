using EEWReplayer.Utils;
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

        private void Form_DevHelper_Load(object sender, EventArgs e)
        {
            JMAXML2OriginalJSON();
        }

        private void JMAXML2OriginalJSON()
        {

            var serializer = new XmlSerializer(typeof(C_Report)) ?? throw new Exception("XmlSerializerの初期化に失敗しました。");
            var xmlSt = File.ReadAllText(@"D:\Ichihai1415\data\json\dmdss\VXSE45_RJTD_20251105010000_03043bf.xml");
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

            var intensityAreas = new Dictionary<Intensity, List<(string areaName, int areaCode)>>();
            foreach (var area in report.Body_seismology1.Intensity.Forecast.Pref.Select(p => p.Area))
            {
                var maxInt = Intensity_JMAString2Enum_FromTo2Max(area[0].ForecastInt.From, area[0].ForecastInt.To);
                var areaName = area[0].Name;
                var areaCode = int.TryParse(area[0].Code, out var c) ? c : 0;
                if (!intensityAreas.ContainsKey(maxInt))
                    intensityAreas[maxInt] = [];
                intensityAreas[maxInt].Add((areaName, areaCode));
            }
            intensityAreas = (Dictionary<Intensity, List<(string areaName, int areaCode)>>)intensityAreas.OrderBy(x => x.Key);


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
                    MaxIntensity = Intensity_JMAString2Enum_FromTo2Max(report.Body_seismology1.Intensity.Forecast.ForecastInt.From, report.Body_seismology1.Intensity.Forecast.ForecastInt.To),
                    MaxIntensityL = Intensity_JMAString2Enum_FromTo2Max(report.Body_seismology1.Intensity.Forecast.ForecastLgInt.From, report.Body_seismology1.Intensity.Forecast.ForecastLgInt.To),
                    IntensityAreas = new Data.EEWList.EEW.IntensityArea[]
                    {

                    }

                }], report.Head.EventID)]
            };



            Console.WriteLine();
        }

        private void JSONMarger()
        {
        }
    }
}
