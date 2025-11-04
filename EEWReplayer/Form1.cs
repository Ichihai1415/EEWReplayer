namespace EEWReplayer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static Form2_progress f = new();

        private void Form1_Load(object sender, EventArgs e)
        {
            f.Show();
            Form_GetAllEEW form_GetAllEEW = new();
            form_GetAllEEW.Show();

            //f.displayText.Text += "\noob";
        }
    }
}
