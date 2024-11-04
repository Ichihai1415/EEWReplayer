namespace EEWReplayer
{
    partial class Form2_progress
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2_progress));
            TB_log = new TextBox();
            CMS_textbox = new ContextMenuStrip(components);
            TSMI_CopyToClipboard = new ToolStripMenuItem();
            TSMI_SaveLog = new ToolStripMenuItem();
            CMS_textbox.SuspendLayout();
            SuspendLayout();
            // 
            // TB_log
            // 
            TB_log.BackColor = SystemColors.ControlText;
            TB_log.BorderStyle = BorderStyle.None;
            TB_log.ContextMenuStrip = CMS_textbox;
            TB_log.Dock = DockStyle.Fill;
            TB_log.Font = new Font("ＭＳ ゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point, 128);
            TB_log.ForeColor = SystemColors.Control;
            TB_log.Location = new Point(0, 0);
            TB_log.MaxLength = int.MaxValue;
            TB_log.Multiline = true;
            TB_log.Name = "TB_log";
            TB_log.ReadOnly = true;
            TB_log.ScrollBars = ScrollBars.Vertical;
            TB_log.Size = new Size(720, 360);
            TB_log.TabIndex = 0;
            TB_log.Text = resources.GetString("TB_log.Text");
            // 
            // CMS_textbox
            // 
            CMS_textbox.Items.AddRange(new ToolStripItem[] { TSMI_CopyToClipboard, TSMI_SaveLog });
            CMS_textbox.Name = "CMS_textbox";
            CMS_textbox.Size = new Size(205, 48);
            // 
            // TSMI_CopyToClipboard
            // 
            TSMI_CopyToClipboard.Name = "TSMI_CopyToClipboard";
            TSMI_CopyToClipboard.Size = new Size(204, 22);
            TSMI_CopyToClipboard.Text = "クリップボードにコピー";
            TSMI_CopyToClipboard.Click += TSMI_CopyToClipboard_Click;
            // 
            // TSMI_SaveLog
            // 
            TSMI_SaveLog.Name = "TSMI_SaveLog";
            TSMI_SaveLog.Size = new Size(204, 22);
            TSMI_SaveLog.Text = "ファイルに保存(Logフォルダ)";
            TSMI_SaveLog.Click += TSMI_SaveLog_Click;
            // 
            // Form2_progress
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlText;
            ClientSize = new Size(720, 360);
            Controls.Add(TB_log);
            ForeColor = SystemColors.Control;
            Name = "Form2_progress";
            Text = "EEWReplayer - Progress";
            CMS_textbox.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox TB_log;
        private ContextMenuStrip CMS_textbox;
        private ToolStripMenuItem TSMI_CopyToClipboard;
        private ToolStripMenuItem TSMI_SaveLog;
    }
}