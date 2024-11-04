namespace EEWReplayer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Form2_progress f = new();
            f.Show();

            //f.displayText.Text += "\noob";
        }
    }
}
