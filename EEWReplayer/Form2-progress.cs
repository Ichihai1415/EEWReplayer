using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EEWReplayer
{
    public partial class Form2_progress : Form
    {
        public Form2_progress(double opacity = 0.5)
        {
            InitializeComponent();
            Opacity = opacity;
            displayText.PropertyChanged += DisplayText_PropertyChanged;
        }

        /// <summary>
        /// テキスト表示内容データ
        /// </summary>
        public DisplayText displayText = new();

        /// <summary>
        /// <see cref="INotifyPropertyChanged"/>を継承したテキスト表示内容データのクラス
        /// </summary>
        public class DisplayText : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler? PropertyChanged;

            /// <summary>
            /// <see cref="Text"/>変更検知時に呼び出される
            /// </summary>
            /// <param name="propertyName"></param>
            private void TextChanged([CallerMemberName] string? propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            /// <summary>
            /// <see cref="Text"/>の更新検知用データ
            /// </summary>
            private string _Text = "";

            /// <summary>
            /// 表示内容(\rは<see cref="DisplayText_PropertyChanged"/>側で追加)
            /// </summary>
            public string Text
            {
                get
                {
                    return _Text;
                }
                set
                {
                    if (value == _Text)
                        return;
                    _Text = value;
                    TextChanged(nameof(Text));
                }
            }
        }

        /// <summary>
        /// <see cref="displayText"/>の更新検知時の動作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DisplayText_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            TB_log.Text = displayText.Text.Replace("\n", "\r\n");
        }

        private void TSMI_CopyToClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(TB_log.Text);
        }

        private void TSMI_SaveLog_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory("Log");
            File.WriteAllText("Log\\" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".log", TB_log.Text);
        }

        public void AddLine(string text)
        {
            displayText.Text += text + "\n";
        }
    }
}
