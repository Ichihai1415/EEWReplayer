using EEWReplayer.Utils;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Text.Json;
using EEWReplayer.Devs;

namespace EEWReplayer
{
    public partial class Form1 : Form
    {

        public const string VERSION = "1.0.0";

        public static readonly JsonSerializerOptions options = new()
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true,
            NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.AllowNamedFloatingPointLiterals,
        };

        public Form1()
        {
            InitializeComponent();
        }
        public static Form2_progress f = new();

        private void Form1_Load(object sender, EventArgs e)
        {
            f.Show();
            //Form_GetAllEEW form_GetAllEEW = new();
            //form_GetAllEEW.Show();
            //Form_StatisticsMaker form_StatisticsMaker = new();
            //form_StatisticsMaker.Show();
            Form_DevHelper form_DevHelper = new();
            form_DevHelper.Show();

            //f.displayText.Text += "\noob";
        }
    }
}
