using AngleSharp.Browser;
using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EEWReplayer
{
    public partial class Form_GetAllEEW : Form
    {

        public static readonly HttpClient client = new();
        public static readonly HtmlParser parser = new();

        public Form_GetAllEEW()
        {
            InitializeComponent();
        }

        private async void Form_GetAllEEW_Load(object sender, EventArgs e)
        {
            var response = await client.GetStringAsync("https://www.data.jma.go.jp/eew/data/nc/pub_hist/index.html");
            var document = await parser.ParseDocumentAsync(response);

            var eqTables = document.QuerySelectorAll("table[id='event_list']");
            var eqRows = eqTables.Select(x=>x.QuerySelectorAll("tr").Skip(1).ToArray());

            foreach (var eqRow in eqRows.SelectMany(x=>x))
            {
                var eqCells = eqRow.QuerySelectorAll("td");
                var detailUrl = eqCells[4].QuerySelector("a").GetAttribute("href").Replace("./", "https://www.data.jma.go.jp/eew/data/nc/pub_hist/").Replace("reachtime/reachtime.html", "fc/index.html");
                Form1.f.AddLine(eqCells[0].TextContent+"　"+detailUrl);
            }


        }
    }
}
