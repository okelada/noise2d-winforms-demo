namespace Noise2D
{
    partial class ViewerForm
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
            pbViewer = new PictureBox();
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)pbViewer).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // pbViewer
            // 
            pbViewer.Location = new Point(0, 0);
            pbViewer.Margin = new Padding(0);
            pbViewer.Name = "pbViewer";
            pbViewer.Size = new Size(1170, 1365);
            pbViewer.SizeMode = PictureBoxSizeMode.StretchImage;
            pbViewer.TabIndex = 0;
            pbViewer.TabStop = false;
            pbViewer.VisibleChanged += pbViewer_VisibleChanged;
            pbViewer.MouseClick += pbViewer_MouseClick;
            // 
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.AutoSize = true;
            panel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel1.Controls.Add(pbViewer);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(1042, 1013);
            panel1.TabIndex = 1;
            // 
            // ViewerForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1042, 1013);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            KeyPreview = true;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ViewerForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Viewer";
            TopMost = true;
            FormClosing += ViewerForm_FormClosing;
            Load += ViewerForm_Load;
            KeyDown += ViewerForm_KeyDown;
            ((System.ComponentModel.ISupportInitialize)pbViewer).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public PictureBox pbViewer;
        private Panel panel1;
    }
}