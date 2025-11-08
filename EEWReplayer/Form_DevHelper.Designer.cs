namespace EEWReplayer.Devs
{
    partial class Form_DevHelper
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
            Img = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)Img).BeginInit();
            SuspendLayout();
            // 
            // Img
            // 
            Img.Dock = DockStyle.Fill;
            Img.Location = new Point(0, 0);
            Img.Name = "Img";
            Img.Size = new Size(800, 450);
            Img.SizeMode = PictureBoxSizeMode.Zoom;
            Img.TabIndex = 0;
            Img.TabStop = false;
            // 
            // Form_DevHelper
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.Zoom;
            ClientSize = new Size(800, 450);
            Controls.Add(Img);
            Margin = new Padding(2);
            Name = "Form_DevHelper";
            Text = "Form_DevHelper";
            Load += Form_DevHelper_Load;
            ((System.ComponentModel.ISupportInitialize)Img).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox Img;
    }
}