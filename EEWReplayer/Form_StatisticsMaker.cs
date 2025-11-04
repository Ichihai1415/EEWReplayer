using System.Text.Json;

namespace EEWReplayer.Utils
{
    public partial class Form_StatisticsMaker : Form
    {
        public const string DIR = "datas";
        public Form_StatisticsMaker()
        {
            InitializeComponent();
        }

        private void Form_StatisticsMaker_Load(object sender, EventArgs e)
        {
            var jsonFiles = Directory.EnumerateFiles(DIR, "*.json", SearchOption.AllDirectories);
            var datas = new List<Data>();
            var codeCounter = new Dictionary<int, int>();
            foreach (var kvp in DataConverter.ConvertSource.AreaForecastE_C2N)
            {
                codeCounter[kvp.Key] = 0;
            }
            foreach (string filePath in jsonFiles)
            {
                var jsonString = File.ReadAllText(filePath);
                var json = JsonSerializer.Deserialize<Data>(jsonString, Form1.options);
                datas.Add(json);
                var warnLastEEW = json.EEWLists[0].EEWs.Where(x => x.IsWarn == true).Last();
                var (warnAreas, warnCodes) = warnLastEEW.GetWarningAreas();

                Form1.f.AddLine(json.Earthquakes[0].OriginTime + " " + json.Earthquakes[0].HypoName + " " + string.Join(' ', warnAreas));
                Form1.f.AddLine("---");
                foreach (var code in warnCodes)
                {
                    if (codeCounter.TryGetValue(code, out int value))
                        codeCounter[code] = ++value;
                }
            }
            Console.WriteLine("Done");
            Form1.f.AddLine("warn: codes");
            foreach (var kvp in codeCounter.OrderBy(x => x.Key))
                Form1.f.AddLine($"{kvp.Key} {DataConverter.ConvertSource.AreaForecastE_C2N[kvp.Key]}: {kvp.Value}");
            Form1.f.AddLine("---\nwarn: codes");
            foreach (var kvp in codeCounter.OrderByDescending(x => x.Value))
                Form1.f.AddLine($"{kvp.Key} {DataConverter.ConvertSource.AreaForecastE_C2N[kvp.Key]}: {kvp.Value}");

        }
    }
}
