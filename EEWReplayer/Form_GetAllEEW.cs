using AngleSharp.Html.Parser;
using EEWReplayer.Utils;
using System.Data;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EEWReplayer
{
    public partial class Form_GetAllEEW : Form
    {

        public static readonly HttpClient client = new();
        public static readonly HtmlParser parser = new();
        public static readonly JsonSerializerOptions options = new()
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true,
            NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.AllowNamedFloatingPointLiterals,
        };
        public Form_GetAllEEW()
        {
            InitializeComponent();
        }

        private async void Form_GetAllEEW_Load(object sender, EventArgs e)
        {
            //var d_new = await GetData.GetDetail("https://www.data.jma.go.jp/eew/data/nc/pub_hist/2024/01/20240101161010/fc/index.html");
            //Directory.CreateDirectory("Test");
            //File.WriteAllText("Test\\d_new.json", JsonSerializer.Serialize(d_new,options));
            //var d_old = await GetData.GetDetail("https://www.data.jma.go.jp/eew/data/nc/pub_hist/2022/03/20220316233646/content/content_out.html");
            //File.WriteAllText("Test\\d_old.json", JsonSerializer.Serialize(d_old,options));
            //var d_s0 = await GetData.GetDetail("https://www.data.jma.go.jp/eew/data/nc/pub_hist/2009/08/20090825063712/content/content_out.html");
            //File.WriteAllText("Test\\d_s0.json", JsonSerializer.Serialize(d_s0, options));

            //return;

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
                var d = await GetData.GetDetail(detailUrl);
                Directory.CreateDirectory("datas");
                File.WriteAllText("datas\\" + detailUrl.Split('/')[9] + ".json", JsonSerializer.Serialize(d, options));
            }


        }
    }
}
