namespace Noise2D
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            groupBox1 = new GroupBox();
            label9 = new Label();
            label10 = new Label();
            Turb = new Label();
            Fractal = new Label();
            label13 = new Label();
            label14 = new Label();
            pbPerlinNoise = new PictureBox();
            pbMarbleNoise = new PictureBox();
            pbTurbNoise = new PictureBox();
            pbFractalNoise = new PictureBox();
            pbValueNoise = new PictureBox();
            pbWhiteNoise = new PictureBox();
            linkLabel2 = new LinkLabel();
            linkLabel1 = new LinkLabel();
            label8 = new Label();
            tbGlobalRandomSeed = new TextBox();
            label6 = new Label();
            tbPerlinScale = new TextBox();
            label7 = new Label();
            udFSnLayers = new NumericUpDown();
            label5 = new Label();
            tbFSAmplitudeMultiplier = new TextBox();
            label4 = new Label();
            tbFSFrequencyMultiplier = new TextBox();
            label3 = new Label();
            tbFSFrequency = new TextBox();
            folderBrowserDialog1 = new FolderBrowserDialog();
            tbImageWidth = new TextBox();
            label1 = new Label();
            label2 = new Label();
            tbImageHeight = new TextBox();
            groupBox2 = new GroupBox();
            groupBox3 = new GroupBox();
            groupBox4 = new GroupBox();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbPerlinNoise).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbMarbleNoise).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbTurbNoise).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbFractalNoise).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbValueNoise).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbWhiteNoise).BeginInit();
            ((System.ComponentModel.ISupportInitialize)udFSnLayers).BeginInit();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label9);
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(Turb);
            groupBox1.Controls.Add(Fractal);
            groupBox1.Controls.Add(label13);
            groupBox1.Controls.Add(label14);
            groupBox1.Controls.Add(pbPerlinNoise);
            groupBox1.Controls.Add(pbMarbleNoise);
            groupBox1.Controls.Add(pbTurbNoise);
            groupBox1.Controls.Add(pbFractalNoise);
            groupBox1.Controls.Add(pbValueNoise);
            groupBox1.Controls.Add(pbWhiteNoise);
            groupBox1.Location = new Point(11, 109);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1355, 840);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Outputs";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 7.8F);
            label9.Location = new Point(959, 806);
            label9.Name = "label9";
            label9.Size = new Size(40, 17);
            label9.TabIndex = 38;
            label9.Text = "Perlin";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 7.8F);
            label10.Location = new Point(493, 806);
            label10.Name = "label10";
            label10.Size = new Size(50, 17);
            label10.TabIndex = 37;
            label10.Text = "Marble";
            // 
            // Turb
            // 
            Turb.AutoSize = true;
            Turb.Font = new Font("Segoe UI", 7.8F);
            Turb.Location = new Point(27, 806);
            Turb.Name = "Turb";
            Turb.Size = new Size(35, 17);
            Turb.TabIndex = 36;
            Turb.Text = "Turb";
            // 
            // Fractal
            // 
            Fractal.AutoSize = true;
            Fractal.Font = new Font("Segoe UI", 7.8F);
            Fractal.Location = new Point(959, 393);
            Fractal.Name = "Fractal";
            Fractal.Size = new Size(46, 17);
            Fractal.TabIndex = 35;
            Fractal.Text = "Fractal";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 7.8F);
            label13.Location = new Point(493, 393);
            label13.Name = "label13";
            label13.Size = new Size(39, 17);
            label13.TabIndex = 34;
            label13.Text = "Value";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 7.8F);
            label14.Location = new Point(27, 393);
            label14.Name = "label14";
            label14.Size = new Size(41, 17);
            label14.TabIndex = 33;
            label14.Text = "White";
            // 
            // pbPerlinNoise
            // 
            pbPerlinNoise.BorderStyle = BorderStyle.FixedSingle;
            pbPerlinNoise.Location = new Point(959, 437);
            pbPerlinNoise.Name = "pbPerlinNoise";
            pbPerlinNoise.Size = new Size(378, 364);
            pbPerlinNoise.SizeMode = PictureBoxSizeMode.Zoom;
            pbPerlinNoise.TabIndex = 32;
            pbPerlinNoise.TabStop = false;
            pbPerlinNoise.Tag = 5;
            pbPerlinNoise.Click += pbThumbnail_Click;
            // 
            // pbMarbleNoise
            // 
            pbMarbleNoise.BorderStyle = BorderStyle.FixedSingle;
            pbMarbleNoise.Location = new Point(493, 437);
            pbMarbleNoise.Name = "pbMarbleNoise";
            pbMarbleNoise.Size = new Size(378, 364);
            pbMarbleNoise.SizeMode = PictureBoxSizeMode.Zoom;
            pbMarbleNoise.TabIndex = 31;
            pbMarbleNoise.TabStop = false;
            pbMarbleNoise.Tag = 4;
            pbMarbleNoise.Click += pbThumbnail_Click;
            // 
            // pbTurbNoise
            // 
            pbTurbNoise.BorderStyle = BorderStyle.FixedSingle;
            pbTurbNoise.Location = new Point(27, 437);
            pbTurbNoise.Name = "pbTurbNoise";
            pbTurbNoise.Size = new Size(378, 364);
            pbTurbNoise.SizeMode = PictureBoxSizeMode.Zoom;
            pbTurbNoise.TabIndex = 30;
            pbTurbNoise.TabStop = false;
            pbTurbNoise.Tag = 3;
            pbTurbNoise.Click += pbThumbnail_Click;
            // 
            // pbFractalNoise
            // 
            pbFractalNoise.BorderStyle = BorderStyle.FixedSingle;
            pbFractalNoise.Location = new Point(959, 24);
            pbFractalNoise.Name = "pbFractalNoise";
            pbFractalNoise.Size = new Size(378, 364);
            pbFractalNoise.SizeMode = PictureBoxSizeMode.Zoom;
            pbFractalNoise.TabIndex = 29;
            pbFractalNoise.TabStop = false;
            pbFractalNoise.Tag = 2;
            pbFractalNoise.Click += pbThumbnail_Click;
            // 
            // pbValueNoise
            // 
            pbValueNoise.BorderStyle = BorderStyle.FixedSingle;
            pbValueNoise.Location = new Point(493, 24);
            pbValueNoise.Name = "pbValueNoise";
            pbValueNoise.Size = new Size(378, 364);
            pbValueNoise.SizeMode = PictureBoxSizeMode.Zoom;
            pbValueNoise.TabIndex = 28;
            pbValueNoise.TabStop = false;
            pbValueNoise.Tag = 1;
            pbValueNoise.Click += pbThumbnail_Click;
            // 
            // pbWhiteNoise
            // 
            pbWhiteNoise.BorderStyle = BorderStyle.FixedSingle;
            pbWhiteNoise.Location = new Point(27, 24);
            pbWhiteNoise.Name = "pbWhiteNoise";
            pbWhiteNoise.Size = new Size(378, 364);
            pbWhiteNoise.SizeMode = PictureBoxSizeMode.Zoom;
            pbWhiteNoise.TabIndex = 27;
            pbWhiteNoise.TabStop = false;
            pbWhiteNoise.Tag = 0;
            pbWhiteNoise.Click += pbThumbnail_Click;
            // 
            // linkLabel2
            // 
            linkLabel2.AutoSize = true;
            linkLabel2.Location = new Point(1234, 68);
            linkLabel2.Name = "linkLabel2";
            linkLabel2.Size = new Size(103, 20);
            linkLabel2.TabIndex = 40;
            linkLabel2.TabStop = true;
            linkLabel2.Text = "Tutorial part 2";
            linkLabel2.LinkClicked += linkLabel2_LinkClicked;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(1234, 22);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(103, 20);
            linkLabel1.TabIndex = 39;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Tutorial part 1";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(243, 31);
            label8.Name = "label8";
            label8.Size = new Size(42, 20);
            label8.TabIndex = 51;
            label8.Text = "Seed";
            // 
            // tbGlobalRandomSeed
            // 
            tbGlobalRandomSeed.Location = new Point(291, 28);
            tbGlobalRandomSeed.Name = "tbGlobalRandomSeed";
            tbGlobalRandomSeed.Size = new Size(72, 27);
            tbGlobalRandomSeed.TabIndex = 50;
            tbGlobalRandomSeed.Text = "2016";
            tbGlobalRandomSeed.TextAlign = HorizontalAlignment.Right;
            tbGlobalRandomSeed.Validating += tbGlobalRandomSeed_Validating;
            tbGlobalRandomSeed.Validated += tbGlobalRandomSeed_Validated;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(111, 31);
            label6.Name = "label6";
            label6.Size = new Size(44, 20);
            label6.TabIndex = 49;
            label6.Text = "Scale";
            // 
            // tbPerlinScale
            // 
            tbPerlinScale.Location = new Point(161, 26);
            tbPerlinScale.Name = "tbPerlinScale";
            tbPerlinScale.Size = new Size(72, 27);
            tbPerlinScale.TabIndex = 48;
            tbPerlinScale.Text = "64";
            tbPerlinScale.TextAlign = HorizontalAlignment.Right;
            tbPerlinScale.Validating += tbPerlinScale_Validating;
            tbPerlinScale.Validated += tbPerlinScale_Validated;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(267, 61);
            label7.Name = "label7";
            label7.Size = new Size(58, 20);
            label7.TabIndex = 47;
            label7.Text = "nLayers";
            // 
            // udFSnLayers
            // 
            udFSnLayers.Location = new Point(331, 59);
            udFSnLayers.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            udFSnLayers.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            udFSnLayers.Name = "udFSnLayers";
            udFSnLayers.Size = new Size(114, 27);
            udFSnLayers.TabIndex = 6;
            udFSnLayers.Value = new decimal(new int[] { 5, 0, 0, 0 });
            udFSnLayers.ValueChanged += udFSnLayers_ValueChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(251, 29);
            label5.Name = "label5";
            label5.Size = new Size(74, 20);
            label5.TabIndex = 44;
            label5.Text = "AmpMult.";
            // 
            // tbFSAmplitudeMultiplier
            // 
            tbFSAmplitudeMultiplier.Location = new Point(331, 26);
            tbFSAmplitudeMultiplier.Name = "tbFSAmplitudeMultiplier";
            tbFSAmplitudeMultiplier.Size = new Size(114, 27);
            tbFSAmplitudeMultiplier.TabIndex = 43;
            tbFSAmplitudeMultiplier.Text = "0.35";
            tbFSAmplitudeMultiplier.TextAlign = HorizontalAlignment.Right;
            tbFSAmplitudeMultiplier.Validating += tbFSAmplitudeMultiplier_Validating;
            tbFSAmplitudeMultiplier.Validated += tbFSAmplitudeMultiplier_Validated;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(45, 61);
            label4.Name = "label4";
            label4.Size = new Size(71, 20);
            label4.TabIndex = 42;
            label4.Text = "FreqMult.";
            // 
            // tbFSFrequencyMultiplier
            // 
            tbFSFrequencyMultiplier.Location = new Point(119, 59);
            tbFSFrequencyMultiplier.Name = "tbFSFrequencyMultiplier";
            tbFSFrequencyMultiplier.Size = new Size(114, 27);
            tbFSFrequencyMultiplier.TabIndex = 41;
            tbFSFrequencyMultiplier.Text = "1.8";
            tbFSFrequencyMultiplier.TextAlign = HorizontalAlignment.Right;
            tbFSFrequencyMultiplier.Validating += tbFSFrequencyMultiplier_Validating;
            tbFSFrequencyMultiplier.Validated += tbFSFrequencyMultiplier_Validated;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(72, 29);
            label3.Name = "label3";
            label3.Size = new Size(41, 20);
            label3.TabIndex = 10;
            label3.Text = "Freq.";
            // 
            // tbFSFrequency
            // 
            tbFSFrequency.Location = new Point(119, 26);
            tbFSFrequency.Name = "tbFSFrequency";
            tbFSFrequency.Size = new Size(114, 27);
            tbFSFrequency.TabIndex = 9;
            tbFSFrequency.Text = "0.02";
            tbFSFrequency.TextAlign = HorizontalAlignment.Right;
            tbFSFrequency.Validating += tbFSFrequency_Validating;
            tbFSFrequency.Validated += tbFSFrequency_Validated;
            // 
            // folderBrowserDialog1
            // 
            folderBrowserDialog1.ShowNewFolderButton = false;
            // 
            // tbImageWidth
            // 
            tbImageWidth.Location = new Point(106, 28);
            tbImageWidth.Name = "tbImageWidth";
            tbImageWidth.Size = new Size(89, 27);
            tbImageWidth.TabIndex = 4;
            tbImageWidth.Text = "1024";
            tbImageWidth.TextAlign = HorizontalAlignment.Right;
            tbImageWidth.Validating += tbImageWidth_Validating;
            tbImageWidth.Validated += tbImageWidth_Validated;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(51, 31);
            label1.Name = "label1";
            label1.Size = new Size(49, 20);
            label1.TabIndex = 5;
            label1.Text = "Width";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(51, 64);
            label2.Name = "label2";
            label2.Size = new Size(54, 20);
            label2.TabIndex = 8;
            label2.Text = "Height";
            // 
            // tbImageHeight
            // 
            tbImageHeight.Location = new Point(106, 61);
            tbImageHeight.Name = "tbImageHeight";
            tbImageHeight.Size = new Size(89, 27);
            tbImageHeight.TabIndex = 7;
            tbImageHeight.Text = "1024";
            tbImageHeight.TextAlign = HorizontalAlignment.Right;
            tbImageHeight.Validating += tbImageHeight_Validating;
            tbImageHeight.Validated += tbImageHeight_Validated;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(tbFSAmplitudeMultiplier);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(tbFSFrequency);
            groupBox2.Controls.Add(tbFSFrequencyMultiplier);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(udFSnLayers);
            groupBox2.Location = new Point(445, 9);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(461, 94);
            groupBox2.TabIndex = 52;
            groupBox2.TabStop = false;
            groupBox2.Text = "Value noise";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(tbPerlinScale);
            groupBox3.Controls.Add(label6);
            groupBox3.Location = new Point(953, 9);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(250, 94);
            groupBox3.TabIndex = 53;
            groupBox3.TabStop = false;
            groupBox3.Text = "Perlin";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(tbImageHeight);
            groupBox4.Controls.Add(tbImageWidth);
            groupBox4.Controls.Add(label1);
            groupBox4.Controls.Add(label8);
            groupBox4.Controls.Add(tbGlobalRandomSeed);
            groupBox4.Controls.Add(label2);
            groupBox4.Location = new Point(12, 9);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(384, 94);
            groupBox4.TabIndex = 54;
            groupBox4.TabStop = false;
            groupBox4.Text = "Global";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1375, 952);
            Controls.Add(linkLabel1);
            Controls.Add(linkLabel2);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Scratchapixel value noise demo";
            FormClosing += MainForm_FormClosing;
            Load += Mainform_Load;
            Click += MainForm_Click;
            DoubleClick += MainForm_DoubleClick;
            KeyDown += MainForm_KeyDown;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbPerlinNoise).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbMarbleNoise).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbTurbNoise).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbFractalNoise).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbValueNoise).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbWhiteNoise).EndInit();
            ((System.ComponentModel.ISupportInitialize)udFSnLayers).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private GroupBox groupBox1;
        private PictureBox pbPerlinNoise;
        private PictureBox pbMarbleNoise;
        private PictureBox pbTurbNoise;
        private PictureBox pbFractalNoise;
        private PictureBox pbValueNoise;
        private PictureBox pbWhiteNoise;
        private Label label9;
        private Label label10;
        private Label Turb;
        private Label Fractal;
        private Label label13;
        private Label label14;
        private LinkLabel linkLabel1;
        private LinkLabel linkLabel2;
        private FolderBrowserDialog folderBrowserDialog1;
        private TextBox tbImageWidth;
        private Label label1;
        private NumericUpDown udFSnLayers;
        private Label label7;
        private Label label5;
        private TextBox tbFSAmplitudeMultiplier;
        private Label label4;
        private TextBox tbFSFrequencyMultiplier;
        private Label label3;
        private TextBox tbFSFrequency;
        private Label label2;
        private TextBox tbImageHeight;
        private Label label6;
        private TextBox tbPerlinScale;
        private Label label8;
        private TextBox tbGlobalRandomSeed;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
    }
}