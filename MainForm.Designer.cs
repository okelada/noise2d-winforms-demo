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
            button6 = new Button();
            button5 = new Button();
            button4 = new Button();
            button3 = new Button();
            button2 = new Button();
            btSaveToPNG_0 = new Button();
            panel2 = new Panel();
            btSaveToCSV_5 = new Button();
            btSaveToCSV_4 = new Button();
            btSaveToCSV_3 = new Button();
            btSaveToCSV_2 = new Button();
            btSaveToCSV_1 = new Button();
            btSaveToCSV_0 = new Button();
            panel1 = new Panel();
            label9 = new Label();
            label10 = new Label();
            Turb = new Label();
            Fractal = new Label();
            label13 = new Label();
            label14 = new Label();
            pbPerlinNoise = new PictureBox();
            pbMarbleNoise = new PictureBox();
            pbTurbNoise = new PictureBox();
            pbFractalValue = new PictureBox();
            pbValueNoise = new PictureBox();
            pbFractalPerlin = new PictureBox();
            btMakeSeamless = new Button();
            linkLabel2 = new LinkLabel();
            linkLabel1 = new LinkLabel();
            label8 = new Label();
            label7 = new Label();
            udFSnLayers = new NumericUpDown();
            label5 = new Label();
            tbFSAmplitudeMultiplier = new TextBox();
            label4 = new Label();
            tbFSFrequencyMultiplier = new TextBox();
            label3 = new Label();
            tbFSFrequency = new TextBox();
            tbImageWidth = new TextBox();
            label1 = new Label();
            label2 = new Label();
            tbImageHeight = new TextBox();
            groupBox2 = new GroupBox();
            groupBox4 = new GroupBox();
            tbSeamlessOvlp = new TextBox();
            label11 = new Label();
            cbTableSize = new ComboBox();
            label6 = new Label();
            udSeed = new NumericUpDown();
            labelStatus = new Label();
            btSetDefaults = new Button();
            saveFileDialog1 = new SaveFileDialog();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbPerlinNoise).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbMarbleNoise).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbTurbNoise).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbFractalValue).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbValueNoise).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbFractalPerlin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)udFSnLayers).BeginInit();
            groupBox2.SuspendLayout();
            groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)udSeed).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(button6);
            groupBox1.Controls.Add(button5);
            groupBox1.Controls.Add(button4);
            groupBox1.Controls.Add(button3);
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(btSaveToPNG_0);
            groupBox1.Controls.Add(panel2);
            groupBox1.Controls.Add(btSaveToCSV_5);
            groupBox1.Controls.Add(btSaveToCSV_4);
            groupBox1.Controls.Add(btSaveToCSV_3);
            groupBox1.Controls.Add(btSaveToCSV_2);
            groupBox1.Controls.Add(btSaveToCSV_1);
            groupBox1.Controls.Add(btSaveToCSV_0);
            groupBox1.Controls.Add(panel1);
            groupBox1.Controls.Add(label9);
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(Turb);
            groupBox1.Controls.Add(Fractal);
            groupBox1.Controls.Add(label13);
            groupBox1.Controls.Add(label14);
            groupBox1.Controls.Add(pbPerlinNoise);
            groupBox1.Controls.Add(pbMarbleNoise);
            groupBox1.Controls.Add(pbTurbNoise);
            groupBox1.Controls.Add(pbFractalValue);
            groupBox1.Controls.Add(pbValueNoise);
            groupBox1.Controls.Add(pbFractalPerlin);
            groupBox1.Location = new Point(12, 109);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1355, 840);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Outputs";
            // 
            // button6
            // 
            button6.Location = new Point(1127, 807);
            button6.Name = "button6";
            button6.Size = new Size(99, 27);
            button6.TabIndex = 68;
            button6.Tag = "5";
            button6.Text = "Save to .png";
            button6.UseVisualStyleBackColor = true;
            button6.Click += btSaveToPNG_Click;
            // 
            // button5
            // 
            button5.Location = new Point(661, 807);
            button5.Name = "button5";
            button5.Size = new Size(99, 27);
            button5.TabIndex = 67;
            button5.Tag = "4";
            button5.Text = "Save to .png";
            button5.UseVisualStyleBackColor = true;
            button5.Click += btSaveToPNG_Click;
            // 
            // button4
            // 
            button4.Location = new Point(201, 807);
            button4.Name = "button4";
            button4.Size = new Size(99, 27);
            button4.TabIndex = 66;
            button4.Tag = "3";
            button4.Text = "Save to .png";
            button4.UseVisualStyleBackColor = true;
            button4.Click += btSaveToPNG_Click;
            // 
            // button3
            // 
            button3.Location = new Point(1127, 396);
            button3.Name = "button3";
            button3.Size = new Size(99, 27);
            button3.TabIndex = 65;
            button3.Tag = "2";
            button3.Text = "Save to .png";
            button3.UseVisualStyleBackColor = true;
            button3.Click += btSaveToPNG_Click;
            // 
            // button2
            // 
            button2.Location = new Point(661, 396);
            button2.Name = "button2";
            button2.Size = new Size(99, 27);
            button2.TabIndex = 64;
            button2.Tag = "1";
            button2.Text = "Save to .png";
            button2.UseVisualStyleBackColor = true;
            button2.Click += btSaveToPNG_Click;
            // 
            // btSaveToPNG_0
            // 
            btSaveToPNG_0.Location = new Point(201, 396);
            btSaveToPNG_0.Name = "btSaveToPNG_0";
            btSaveToPNG_0.Size = new Size(99, 27);
            btSaveToPNG_0.TabIndex = 63;
            btSaveToPNG_0.Tag = "0";
            btSaveToPNG_0.Text = "Save to .png";
            btSaveToPNG_0.UseVisualStyleBackColor = true;
            btSaveToPNG_0.Click += btSaveToPNG_Click;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ControlDarkDark;
            panel2.Location = new Point(443, 23);
            panel2.Name = "panel2";
            panel2.Size = new Size(5, 803);
            panel2.TabIndex = 40;
            // 
            // btSaveToCSV_5
            // 
            btSaveToCSV_5.Location = new Point(1232, 807);
            btSaveToCSV_5.Name = "btSaveToCSV_5";
            btSaveToCSV_5.Size = new Size(99, 27);
            btSaveToCSV_5.TabIndex = 61;
            btSaveToCSV_5.Tag = "5";
            btSaveToCSV_5.Text = "Save to .csv";
            btSaveToCSV_5.UseVisualStyleBackColor = true;
            btSaveToCSV_5.Click += btSaveToCSV_Click;
            // 
            // btSaveToCSV_4
            // 
            btSaveToCSV_4.Location = new Point(766, 807);
            btSaveToCSV_4.Name = "btSaveToCSV_4";
            btSaveToCSV_4.Size = new Size(99, 27);
            btSaveToCSV_4.TabIndex = 60;
            btSaveToCSV_4.Tag = "4";
            btSaveToCSV_4.Text = "Save to .csv";
            btSaveToCSV_4.UseVisualStyleBackColor = true;
            btSaveToCSV_4.Click += btSaveToCSV_Click;
            // 
            // btSaveToCSV_3
            // 
            btSaveToCSV_3.Location = new Point(306, 807);
            btSaveToCSV_3.Name = "btSaveToCSV_3";
            btSaveToCSV_3.Size = new Size(99, 27);
            btSaveToCSV_3.TabIndex = 59;
            btSaveToCSV_3.Tag = "3";
            btSaveToCSV_3.Text = "Save to .csv";
            btSaveToCSV_3.UseVisualStyleBackColor = true;
            btSaveToCSV_3.Click += btSaveToCSV_Click;
            // 
            // btSaveToCSV_2
            // 
            btSaveToCSV_2.Location = new Point(1232, 396);
            btSaveToCSV_2.Name = "btSaveToCSV_2";
            btSaveToCSV_2.Size = new Size(99, 27);
            btSaveToCSV_2.TabIndex = 58;
            btSaveToCSV_2.Tag = "2";
            btSaveToCSV_2.Text = "Save to .csv";
            btSaveToCSV_2.UseVisualStyleBackColor = true;
            btSaveToCSV_2.Click += btSaveToCSV_Click;
            // 
            // btSaveToCSV_1
            // 
            btSaveToCSV_1.Location = new Point(766, 396);
            btSaveToCSV_1.Name = "btSaveToCSV_1";
            btSaveToCSV_1.Size = new Size(99, 27);
            btSaveToCSV_1.TabIndex = 57;
            btSaveToCSV_1.Tag = "1";
            btSaveToCSV_1.Text = "Save to .csv";
            btSaveToCSV_1.UseVisualStyleBackColor = true;
            btSaveToCSV_1.Click += btSaveToCSV_Click;
            // 
            // btSaveToCSV_0
            // 
            btSaveToCSV_0.Location = new Point(306, 396);
            btSaveToCSV_0.Name = "btSaveToCSV_0";
            btSaveToCSV_0.Size = new Size(99, 27);
            btSaveToCSV_0.TabIndex = 56;
            btSaveToCSV_0.Tag = "0";
            btSaveToCSV_0.Text = "Save to .csv";
            btSaveToCSV_0.UseVisualStyleBackColor = true;
            btSaveToCSV_0.Click += btSaveToCSV_Click;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ControlDarkDark;
            panel1.Location = new Point(907, 23);
            panel1.Name = "panel1";
            panel1.Size = new Size(5, 803);
            panel1.TabIndex = 39;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 7.8F);
            label9.Location = new Point(953, 395);
            label9.Name = "label9";
            label9.Size = new Size(80, 17);
            label9.TabIndex = 38;
            label9.Text = "Perlin (base)";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 7.8F);
            label10.Location = new Point(487, 806);
            label10.Name = "label10";
            label10.Size = new Size(133, 17);
            label10.TabIndex = 37;
            label10.Text = "Fractal sum  / Marble";
            // 
            // Turb
            // 
            Turb.AutoSize = true;
            Turb.Font = new Font("Segoe UI", 7.8F);
            Turb.Location = new Point(487, 395);
            Turb.Name = "Turb";
            Turb.Size = new Size(151, 17);
            Turb.TabIndex = 36;
            Turb.Text = "Fractal sum / Turbulence";
            // 
            // Fractal
            // 
            Fractal.AutoSize = true;
            Fractal.Font = new Font("Segoe UI", 7.8F);
            Fractal.Location = new Point(27, 806);
            Fractal.Name = "Fractal";
            Fractal.Size = new Size(118, 17);
            Fractal.TabIndex = 35;
            Fractal.Text = "Fractal sum / Value";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 7.8F);
            label13.Location = new Point(27, 395);
            label13.Name = "label13";
            label13.Size = new Size(79, 17);
            label13.TabIndex = 34;
            label13.Text = "Value (base)";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 7.8F);
            label14.Location = new Point(953, 806);
            label14.Name = "label14";
            label14.Size = new Size(119, 17);
            label14.TabIndex = 33;
            label14.Text = "Fractal sum / Perlin";
            // 
            // pbPerlinNoise
            // 
            pbPerlinNoise.BorderStyle = BorderStyle.FixedSingle;
            pbPerlinNoise.Location = new Point(953, 26);
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
            pbMarbleNoise.Location = new Point(487, 437);
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
            pbTurbNoise.Location = new Point(487, 26);
            pbTurbNoise.Name = "pbTurbNoise";
            pbTurbNoise.Size = new Size(378, 364);
            pbTurbNoise.SizeMode = PictureBoxSizeMode.Zoom;
            pbTurbNoise.TabIndex = 30;
            pbTurbNoise.TabStop = false;
            pbTurbNoise.Tag = 3;
            pbTurbNoise.Click += pbThumbnail_Click;
            // 
            // pbFractalValue
            // 
            pbFractalValue.BorderStyle = BorderStyle.FixedSingle;
            pbFractalValue.Location = new Point(27, 437);
            pbFractalValue.Name = "pbFractalValue";
            pbFractalValue.Size = new Size(378, 364);
            pbFractalValue.SizeMode = PictureBoxSizeMode.Zoom;
            pbFractalValue.TabIndex = 29;
            pbFractalValue.TabStop = false;
            pbFractalValue.Tag = 2;
            pbFractalValue.Click += pbThumbnail_Click;
            // 
            // pbValueNoise
            // 
            pbValueNoise.BorderStyle = BorderStyle.FixedSingle;
            pbValueNoise.Location = new Point(27, 26);
            pbValueNoise.Name = "pbValueNoise";
            pbValueNoise.Size = new Size(378, 364);
            pbValueNoise.SizeMode = PictureBoxSizeMode.Zoom;
            pbValueNoise.TabIndex = 28;
            pbValueNoise.TabStop = false;
            pbValueNoise.Tag = 1;
            pbValueNoise.Click += pbThumbnail_Click;
            // 
            // pbFractalPerlin
            // 
            pbFractalPerlin.BorderStyle = BorderStyle.FixedSingle;
            pbFractalPerlin.Location = new Point(953, 437);
            pbFractalPerlin.Name = "pbFractalPerlin";
            pbFractalPerlin.Size = new Size(378, 364);
            pbFractalPerlin.SizeMode = PictureBoxSizeMode.Zoom;
            pbFractalPerlin.TabIndex = 27;
            pbFractalPerlin.TabStop = false;
            pbFractalPerlin.Tag = 0;
            pbFractalPerlin.Click += pbThumbnail_Click;
            // 
            // btMakeSeamless
            // 
            btMakeSeamless.Location = new Point(919, 74);
            btMakeSeamless.Name = "btMakeSeamless";
            btMakeSeamless.Size = new Size(131, 27);
            btMakeSeamless.TabIndex = 62;
            btMakeSeamless.Tag = "5";
            btMakeSeamless.Text = "Make seamless";
            btMakeSeamless.UseVisualStyleBackColor = true;
            btMakeSeamless.Click += btToggleSeamless_Click;
            // 
            // linkLabel2
            // 
            linkLabel2.AutoSize = true;
            linkLabel2.Location = new Point(1263, 35);
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
            linkLabel1.Location = new Point(1263, 9);
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
            label8.Location = new Point(178, 29);
            label8.Name = "label8";
            label8.Size = new Size(42, 20);
            label8.TabIndex = 51;
            label8.Text = "Seed";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(243, 61);
            label7.Name = "label7";
            label7.Size = new Size(58, 20);
            label7.TabIndex = 47;
            label7.Text = "nLayers";
            // 
            // udFSnLayers
            // 
            udFSnLayers.Location = new Point(307, 59);
            udFSnLayers.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            udFSnLayers.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            udFSnLayers.Name = "udFSnLayers";
            udFSnLayers.Size = new Size(60, 27);
            udFSnLayers.TabIndex = 6;
            udFSnLayers.TextAlign = HorizontalAlignment.Right;
            udFSnLayers.Value = new decimal(new int[] { 4, 0, 0, 0 });
            udFSnLayers.ValueChanged += udFSnLayers_ValueChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(232, 28);
            label5.Name = "label5";
            label5.Size = new Size(69, 20);
            label5.TabIndex = 44;
            label5.Text = "fBm gain";
            // 
            // tbFSAmplitudeMultiplier
            // 
            tbFSAmplitudeMultiplier.Location = new Point(307, 26);
            tbFSAmplitudeMultiplier.Name = "tbFSAmplitudeMultiplier";
            tbFSAmplitudeMultiplier.Size = new Size(60, 27);
            tbFSAmplitudeMultiplier.TabIndex = 43;
            tbFSAmplitudeMultiplier.Text = "0.5";
            tbFSAmplitudeMultiplier.TextAlign = HorizontalAlignment.Right;
            tbFSAmplitudeMultiplier.Validating += tbFSAmplitudeMultiplier_Validating;
            tbFSAmplitudeMultiplier.Validated += tbFSAmplitudeMultiplier_Validated;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(14, 61);
            label4.Name = "label4";
            label4.Size = new Size(104, 20);
            label4.TabIndex = 42;
            label4.Text = "fBm lacunarity";
            // 
            // tbFSFrequencyMultiplier
            // 
            tbFSFrequencyMultiplier.Location = new Point(124, 59);
            tbFSFrequencyMultiplier.Name = "tbFSFrequencyMultiplier";
            tbFSFrequencyMultiplier.Size = new Size(60, 27);
            tbFSFrequencyMultiplier.TabIndex = 41;
            tbFSFrequencyMultiplier.Text = "2.0";
            tbFSFrequencyMultiplier.TextAlign = HorizontalAlignment.Right;
            tbFSFrequencyMultiplier.Validating += tbFSFrequencyMultiplier_Validating;
            tbFSFrequencyMultiplier.Validated += tbFSFrequencyMultiplier_Validated;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(27, 29);
            label3.Name = "label3";
            label3.Size = new Size(91, 20);
            label3.TabIndex = 10;
            label3.Text = "Spatial Freq.";
            // 
            // tbFSFrequency
            // 
            tbFSFrequency.Location = new Point(124, 26);
            tbFSFrequency.Name = "tbFSFrequency";
            tbFSFrequency.Size = new Size(60, 27);
            tbFSFrequency.TabIndex = 9;
            tbFSFrequency.Text = "0.02";
            tbFSFrequency.TextAlign = HorizontalAlignment.Right;
            tbFSFrequency.Validating += tbFSFrequency_Validating;
            tbFSFrequency.Validated += tbFSFrequency_Validated;
            // 
            // tbImageWidth
            // 
            tbImageWidth.Location = new Point(66, 26);
            tbImageWidth.Name = "tbImageWidth";
            tbImageWidth.Size = new Size(60, 27);
            tbImageWidth.TabIndex = 4;
            tbImageWidth.Text = "1024";
            tbImageWidth.TextAlign = HorizontalAlignment.Right;
            tbImageWidth.Validating += tbImageWidth_Validating;
            tbImageWidth.Validated += tbImageWidth_Validated;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(11, 29);
            label1.Name = "label1";
            label1.Size = new Size(49, 20);
            label1.TabIndex = 5;
            label1.Text = "Width";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(11, 61);
            label2.Name = "label2";
            label2.Size = new Size(54, 20);
            label2.TabIndex = 8;
            label2.Text = "Height";
            // 
            // tbImageHeight
            // 
            tbImageHeight.Location = new Point(66, 59);
            tbImageHeight.Name = "tbImageHeight";
            tbImageHeight.Size = new Size(60, 27);
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
            groupBox2.Location = new Point(524, 9);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(383, 94);
            groupBox2.TabIndex = 52;
            groupBox2.TabStop = false;
            groupBox2.Text = "Fractal sum";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(tbSeamlessOvlp);
            groupBox4.Controls.Add(label11);
            groupBox4.Controls.Add(cbTableSize);
            groupBox4.Controls.Add(label6);
            groupBox4.Controls.Add(udSeed);
            groupBox4.Controls.Add(tbImageHeight);
            groupBox4.Controls.Add(tbImageWidth);
            groupBox4.Controls.Add(label1);
            groupBox4.Controls.Add(label8);
            groupBox4.Controls.Add(label2);
            groupBox4.Location = new Point(12, 9);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(506, 94);
            groupBox4.TabIndex = 54;
            groupBox4.TabStop = false;
            groupBox4.Text = "Global";
            // 
            // tbSeamlessOvlp
            // 
            tbSeamlessOvlp.Location = new Point(436, 26);
            tbSeamlessOvlp.Name = "tbSeamlessOvlp";
            tbSeamlessOvlp.Size = new Size(60, 27);
            tbSeamlessOvlp.TabIndex = 54;
            tbSeamlessOvlp.Text = "32";
            tbSeamlessOvlp.TextAlign = HorizontalAlignment.Right;
            tbSeamlessOvlp.Validating += tbSeamlessOvlp_Validating;
            tbSeamlessOvlp.Validated += tbSeamlessOvlp_Validated;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(327, 28);
            label11.Name = "label11";
            label11.Size = new Size(103, 20);
            label11.TabIndex = 55;
            label11.Text = "Seamless ovlp";
            // 
            // cbTableSize
            // 
            cbTableSize.DropDownStyle = ComboBoxStyle.DropDownList;
            cbTableSize.FormattingEnabled = true;
            cbTableSize.Items.AddRange(new object[] { "2", "4", "8", "16", "32", "64", "128", "256", "512", "1024", "2048" });
            cbTableSize.Location = new Point(226, 59);
            cbTableSize.Name = "cbTableSize";
            cbTableSize.Size = new Size(60, 28);
            cbTableSize.TabIndex = 53;
            cbTableSize.SelectedIndexChanged += cbTableSize_SelectedIndexChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(147, 61);
            label6.Name = "label6";
            label6.Size = new Size(73, 20);
            label6.TabIndex = 52;
            label6.Text = "Table size";
            // 
            // udSeed
            // 
            udSeed.Location = new Point(226, 26);
            udSeed.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            udSeed.Name = "udSeed";
            udSeed.Size = new Size(60, 27);
            udSeed.TabIndex = 48;
            udSeed.TextAlign = HorizontalAlignment.Right;
            udSeed.Value = new decimal(new int[] { 2016, 0, 0, 0 });
            udSeed.ValueChanged += udSeed_ValueChanged;
            // 
            // labelStatus
            // 
            labelStatus.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            labelStatus.Location = new Point(1198, 63);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(168, 40);
            labelStatus.TabIndex = 50;
            labelStatus.Text = "Idle";
            labelStatus.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btSetDefaults
            // 
            btSetDefaults.Location = new Point(919, 26);
            btSetDefaults.Name = "btSetDefaults";
            btSetDefaults.Size = new Size(107, 29);
            btSetDefaults.TabIndex = 55;
            btSetDefaults.Text = "Set defaults";
            btSetDefaults.UseVisualStyleBackColor = true;
            btSetDefaults.Click += btSetDefaults_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1379, 957);
            Controls.Add(btSetDefaults);
            Controls.Add(labelStatus);
            Controls.Add(linkLabel1);
            Controls.Add(linkLabel2);
            Controls.Add(groupBox4);
            Controls.Add(btMakeSeamless);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Scratchapixel's value noise 2D demo";
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
            ((System.ComponentModel.ISupportInitialize)pbFractalValue).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbValueNoise).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbFractalPerlin).EndInit();
            ((System.ComponentModel.ISupportInitialize)udFSnLayers).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)udSeed).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private GroupBox groupBox1;
        private PictureBox pbPerlinNoise;
        private PictureBox pbMarbleNoise;
        private PictureBox pbTurbNoise;
        private PictureBox pbFractalValue;
        private PictureBox pbValueNoise;
        private PictureBox pbFractalPerlin;
        private Label label9;
        private Label label10;
        private Label Turb;
        private Label Fractal;
        private Label label13;
        private Label label14;
        private LinkLabel linkLabel1;
        private LinkLabel linkLabel2;
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
        private Label label8;
        private GroupBox groupBox2;
        private GroupBox groupBox4;
        private Label labelStatus;
        private Panel panel1;
        private NumericUpDown udSeed;
        private ComboBox cbTableSize;
        private Label label6;
        private Button btSaveToCSV_0;
        private Button btSetDefaults;
        private Button btSaveToCSV_5;
        private Button btSaveToCSV_4;
        private Button btSaveToCSV_3;
        private Button btSaveToCSV_2;
        private Button btSaveToCSV_1;
        private Panel panel2;
        private SaveFileDialog saveFileDialog1;
        private Button btMakeSeamless;
        private TextBox tbSeamlessOvlp;
        private Label label11;
        private Button btSaveToPNG_0;
        private Button button3;
        private Button button2;
        private Button button6;
        private Button button5;
        private Button button4;
    }
}