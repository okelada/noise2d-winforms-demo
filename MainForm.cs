//#define USE_DEBUG_SEAMLESS


using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Noise2D
{
    public partial class MainForm : Form
    {
        ViewerForm viewer = new ViewerForm();
        System.Windows.Forms.Timer viewerFormTimer = new System.Windows.Forms.Timer();

        int baseWidth;
        int baseHeight;
        float frequency;
        float fBm_lacunarity;
        float fBm_gain;
        int numLayers;
        int seed;
        uint tableSize;
        int seamlessOvlp;

        bool bMakeSeamless;
        bool bNeedsRedraw;

        float[]? noiseBufferValue;
        float[]? noiseBufferFractal;
        float[]? noiseBufferTurb;
        float[]? noiseBufferMarble;
        float[]? noiseBufferPerlin;
        float[]? noiseBufferFractalPerlin;

        float[]? testPatternBuffer;

        Bitmap? OutputImageFractalPerlin = null;
        Bitmap? OutputImageValue = null;
        Bitmap? OutputImageFractal = null;
        Bitmap? OutputImageTurb = null;
        Bitmap? OutputImageMarble = null;
        Bitmap? OutputImagePerlin = null;

        public MainForm()
        {
            InitializeComponent();

            foreach (Control control in Controls)
                control.MouseClick += RedirectMouseClick;

            groupBoxOutputs.Click += groupBox1_Click;

            labelStatus.Text = "Idle";
            labelStatus.ForeColor = Color.Green;
        }

        private void RedirectMouseClick(object sender, MouseEventArgs e)
        {
            Control control = (Control)sender;
            Point screenPoint = control.PointToScreen(new Point(e.X, e.Y));
            Point formPoint = PointToClient(screenPoint);
            MouseEventArgs args = new MouseEventArgs(e.Button, e.Clicks,
                formPoint.X, formPoint.Y, e.Delta);
            OnMouseClick(args);
        }

        private void SetDefaults()
        {
            baseWidth = 1024;
            baseHeight = 768;

            frequency = .01f;
            fBm_lacunarity = 2.0f;
            fBm_gain = 0.5f;

            numLayers = 4;
            seed = 2016;
            tableSize = 256;
            seamlessOvlp = 32;

            tbImageWidth.Text = baseWidth.ToString();
            tbImageHeight.Text = baseHeight.ToString();
            tbFSFrequency.Text = frequency.ToString();
            tbFSFrequencyMultiplier.Text = fBm_lacunarity.ToString();
            tbFSAmplitudeMultiplier.Text = fBm_gain.ToString();
            udFSnLayers.Value = numLayers;
            udSeed.Value = seed;
            cbTableSize.Text = tableSize.ToString();
            tbSeamlessOvlp.Text = seamlessOvlp.ToString();

            cbOvlpInterpolator.SelectedIndex = 0;

            bMakeSeamless = true;
            bNeedsRedraw = true;
#if USE_DEBUG_SEAMLESS
            testPatternBuffer = GetTestPattern(false);
#endif
        }


        private void Mainform_Load(object sender, EventArgs e)
        {
            SetDefaults();

            ResetAll();
            viewerFormTimer.Tick += ViewerFormTimer_Tick;
            viewerFormTimer.Interval = 250;
            viewerFormTimer.Start();
            //valueNoise1D.TestModulo();
        }


        private void EnableControls(bool bDisable)
        {
            btMakeSeamless.Enabled = bDisable;
            btSetDefaults.Enabled = bDisable;
            groupBoxfBm.Enabled = bDisable;
            groupBoxGlobal.Enabled = bDisable;
            groupBoxOutputs.Enabled = bDisable;
        }



        private int getActualWidth()
        {
            int actualWidth = (baseWidth / 2) * 2;

            if (bMakeSeamless)
                actualWidth += 2 * seamlessOvlp;
            return actualWidth;
        }

        private int getActualHeight()
        {
            int actualHeight = (baseHeight/ 2) * 2;

            if (bMakeSeamless)
                actualHeight += 2 * seamlessOvlp;
            return actualHeight;
        }



        private void ViewerFormTimer_Tick(object? sender, EventArgs e)
        {

            if (bNeedsRedraw) 
                viewer.viewerRequested = false;

            if (!viewer.viewerRequested && viewer.Visible)
            {
                bool bMouseInViewer = viewer.ClientRectangle.Contains(viewer.PointToClient(Cursor.Position));

                if (!viewer.viewerRequested)// && !bMouseInViewer)
                {
                    viewer.Hide();
                    //  viewerFormTimer.Stop();
                    viewerFormTimer.Interval = 3000;
                    //  Debug.Write("THIDE");
                }
            }
            else if (viewer.viewerRequested && !viewer.Visible)
            {
                viewer.StartPosition = FormStartPosition.Manual;
                viewer.Location = new Point(this.Location.X + (this.Width - viewer.Width) / 2, this.Location.Y + (this.Height - viewer.Height) / 2);

                viewer.Show(this);
                viewerFormTimer.Interval = 500;
                //Debug.Write("TSHOW");
            }

            if (bNeedsRedraw)
            {
                EnableControls(false);
                bNeedsRedraw = false;

                Debug.Write("Redrawing...");
                labelStatus.Text = "Processing...";
                labelStatus.ForeColor = Color.Red;
                ResetImages();


                SeamlessOverlap.OvlpInterpDelegate seamlessOvlpInterpolator = SeamlessOverlap.OvlpInterp1;

                if (cbOvlpInterpolator.SelectedIndex == 1)
                    seamlessOvlpInterpolator = SeamlessOverlap.OvlpInterp3;
                else if (cbOvlpInterpolator.SelectedIndex == 2)
                    seamlessOvlpInterpolator = SeamlessOverlap.OvlpInterp5;


                int actualWidth = getActualWidth();
                int actualHeight = getActualHeight();

                try
                {
                    Parallel.Invoke(
                    () =>
                    {
#if USE_DEBUG_SEAMLESS
                        OutputImageValue = RenderBWBuffer(testPatternBuffer, width, height);
#else
                        ValueNoise valueNoise = new ValueNoise(actualWidth, actualHeight, frequency, seed, tableSize);
                        noiseBufferValue = valueNoise.GetNoiseBuffer();

                        if (bMakeSeamless)
                        {
                            SeamlessOverlap seamless = new SeamlessOverlap(actualWidth, actualHeight, seamlessOvlp, seamlessOvlpInterpolator);
                            noiseBufferValue = seamless.GetSeamlessBuffer(noiseBufferValue, out int outImageWidth, out int outImageHeight);
                            OutputImageValue = RenderBWBuffer(noiseBufferValue, outImageWidth, outImageHeight);
                        }
                        else
                            OutputImageValue = RenderBWBuffer(noiseBufferValue, actualWidth, actualHeight);
#endif
                    },
                    () =>
                    {
                        ValueNoise valueNoise = new ValueNoise(actualWidth, actualHeight, frequency, seed, tableSize);
                        noiseBufferFractal = valueNoise.GetFractalNoiseBuffer(fBm_lacunarity, fBm_gain, numLayers);
                        if (bMakeSeamless)
                        {
                            SeamlessOverlap seamless = new SeamlessOverlap(actualWidth, actualHeight, seamlessOvlp, seamlessOvlpInterpolator);
                            noiseBufferFractal = seamless.GetSeamlessBuffer(noiseBufferFractal, out int outImageWidth, out int outImageHeight);
                            OutputImageFractal = RenderBWBuffer(noiseBufferFractal, outImageWidth, outImageHeight);
                        }
                        else
                            OutputImageFractal = RenderBWBuffer(noiseBufferFractal, actualWidth, actualHeight);


                    },
                    () =>
                    {
#if USE_DEBUG_SEAMLESS
                        SeamlessOverlap seamless = new SeamlessOverlap(width, height, seamlessOvlp);
                        float[] seamlessBufferUpper = seamless.GetSeamlessBufferUpper(testPatternBuffer, out int outImageWidthU, out int outImageHeightU);

                        seamlessBufferUpper = BaseNoise.NormalizeBuffer(seamlessBufferUpper);
                        OutputImageTurb = RenderBWBuffer(seamlessBufferUpper, outImageWidthU, outImageHeightU);
                        pbTurbNoise.Image = OutputImageTurb;

#else

                        ValueNoise valueNoise = new ValueNoise(actualWidth, actualHeight, frequency, seed, tableSize);
                        noiseBufferTurb = valueNoise.GetTurbulenceNoiseBuffer(fBm_lacunarity, fBm_gain, numLayers);
                        if (bMakeSeamless)
                        {
                            SeamlessOverlap seamless = new SeamlessOverlap(actualWidth, actualHeight, seamlessOvlp, seamlessOvlpInterpolator);
                            noiseBufferTurb = seamless.GetSeamlessBuffer(noiseBufferTurb, out int outImageWidth, out int outImageHeight);
                            OutputImageTurb = RenderBWBuffer(noiseBufferTurb, outImageWidth, outImageHeight);
                        }
                        else
                            OutputImageTurb = RenderBWBuffer(noiseBufferTurb, actualWidth, actualHeight);
#endif
                    },
                    () =>
                    {

#if USE_DEBUG_SEAMLESS

                        SeamlessOverlap seamless = new SeamlessOverlap(width, height, seamlessOvlp);
                        float[] seamlessBufferLower = seamless.GetSeamlessBufferLower(testPatternBuffer, out int outImageWidthL, out int outImageHeightL);
                        seamlessBufferLower = BaseNoise.NormalizeBuffer(seamlessBufferLower);
                        OutputImageMarble = RenderBWBuffer(seamlessBufferLower, outImageWidthL, outImageHeightL);
                        pbMarbleNoise.Image = OutputImageMarble;
#else



                        ValueNoise valueNoise = new ValueNoise(actualWidth, actualHeight, frequency, seed, tableSize);
                        noiseBufferMarble = valueNoise.GetMarbleNoiseBuffer(fBm_lacunarity, fBm_gain, numLayers);
                        if (bMakeSeamless)
                        {
                            SeamlessOverlap seamless = new SeamlessOverlap(actualWidth, actualHeight, seamlessOvlp, seamlessOvlpInterpolator);
                            noiseBufferMarble = seamless.GetSeamlessBuffer(noiseBufferMarble, out int outImageWidth, out int outImageHeight);
                            OutputImageMarble = RenderBWBuffer(noiseBufferMarble, outImageWidth, outImageHeight);
                        }
                        else
                            OutputImageMarble = RenderBWBuffer(noiseBufferMarble, actualWidth, actualHeight);
#endif
                    },
                    () =>
                    {
                        PerlinNoise perlinNoise = new PerlinNoise(actualWidth, actualHeight, frequency, seed, tableSize);
                        noiseBufferPerlin = perlinNoise.GetNoiseBuffer();
                        if (bMakeSeamless)
                        {
                            SeamlessOverlap seamless = new SeamlessOverlap(actualWidth, actualHeight, seamlessOvlp, seamlessOvlpInterpolator);
                            noiseBufferPerlin = seamless.GetSeamlessBuffer(noiseBufferPerlin, out int outImageWidth, out int outImageHeight);
                            OutputImagePerlin = RenderBWBuffer(noiseBufferPerlin, outImageWidth, outImageHeight);
                        }
                        else
                            OutputImagePerlin = RenderBWBuffer(noiseBufferPerlin, actualWidth, actualHeight);
                    },
                    () =>
                    {
#if USE_DEBUG_SEAMLESS 
                        SeamlessOverlap seamless = new SeamlessOverlap(width, height, seamlessOvlp);
                        float[] seamlessBuffer = seamless.GetSeamlessBuffer(testPatternBuffer, out int outImageWidth, out int outImageHeight);
                        seamlessBuffer = BaseNoise.NormalizeBuffer(seamlessBuffer);
                        OutputImageFractalPerlin = RenderBWBuffer(seamlessBuffer, outImageWidth, outImageHeight);
#else
                        PerlinNoise perlinNoise = new PerlinNoise(actualWidth, actualHeight, frequency, seed, tableSize);
                        noiseBufferFractalPerlin = perlinNoise.GetFractalNoiseBuffer(fBm_lacunarity, fBm_gain, numLayers);
                        //BaseNoise.ShowMinMax(noiseBufferFractalPerlin, out float minValue, out float maxValue);

                        if (bMakeSeamless)
                        {
                            SeamlessOverlap seamless = new SeamlessOverlap(actualWidth, actualHeight, seamlessOvlp, seamlessOvlpInterpolator);
                            noiseBufferFractalPerlin = seamless.GetSeamlessBuffer(noiseBufferFractalPerlin, out int outImageWidth, out int outImageHeight);
                            OutputImageFractalPerlin = RenderBWBuffer(noiseBufferFractalPerlin, outImageWidth, outImageHeight);
                            //BaseNoise.ShowMinMax(noiseBufferFractalPerlin, out minValue, out maxValue);
                        }
                        else
                            OutputImageFractalPerlin = RenderBWBuffer(noiseBufferFractalPerlin, actualWidth, actualHeight);
#endif
                    }
                    );
                }
                catch (AggregateException ex)
                {
                    Console.WriteLine("An action has thrown an exception. THIS WAS UNEXPECTED.\n{0}", ex.InnerException.ToString());
                }

                pbFractalPerlin.Image = OutputImageFractalPerlin;
                pbValueNoise.Image = OutputImageValue;
                pbFractalValue.Image = OutputImageFractal;
                pbTurbNoise.Image = OutputImageTurb;
                pbMarbleNoise.Image = OutputImageMarble;
                pbPerlinNoise.Image = OutputImagePerlin;

                Debug.WriteLine("Done.");
                labelStatus.Text = "Idle";
                labelStatus.ForeColor = Color.Green;
                EnableControls(true);
            }
        }


        private void ResetImages()
        {
            pbFractalPerlin.Image = Properties.Resources.noimage;
            pbValueNoise.Image = Properties.Resources.noimage;
            pbFractalValue.Image = Properties.Resources.noimage;
            pbTurbNoise.Image = Properties.Resources.noimage;
            pbMarbleNoise.Image = Properties.Resources.noimage;
            pbPerlinNoise.Image = Properties.Resources.noimage;
        }


        private void ResetAll()
        {
            ResetImages();

            noiseBufferValue = null;
            noiseBufferFractal = null;
            noiseBufferTurb = null;
            noiseBufferMarble = null;
            noiseBufferPerlin = null;
            noiseBufferFractalPerlin = null;
            //lvLog.Items.Clear();
        }



        private Bitmap? RenderBWBuffer(float[] noiseBuffer, int width, int height)
        {
            Bitmap? OutputImage = null;
            if (noiseBuffer == null)
                return null;

            try
            {
                int nPixels = height * width;

                OutputImage = new Bitmap(width, height, PixelFormat.Format32bppArgb);

                // Lock the bitmap's bits.  
                Rectangle rect = new Rectangle(0, 0, OutputImage.Width, OutputImage.Height);

                System.Drawing.Imaging.BitmapData bmpData =
                OutputImage.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

                // Get the address of the first line.
                IntPtr ptrBW = bmpData.Scan0;
                // Declare an array to hold the bytes of the bitmap.
                int nBytesLen = Math.Abs(bmpData.Stride) * bmpData.Height;
                byte[] bwValues = new byte[nBytesLen];

                // Copy the BW values into the array.
                System.Runtime.InteropServices.Marshal.Copy(ptrBW, bwValues, 0, nBytesLen);
                for (int j = 0; j < height; j++)
                {
                    for (int i = 0; i < width; i++)
                    {
                        int srcPos = j * width + i;
                        int bmpPos = Math.Abs(bmpData.Stride) * j + 4 * i;

                        byte bwValue = (byte)(noiseBuffer[srcPos] * 255.0f);//BW
                        bwValues[bmpPos + 0] = bwValue;//B
                        bwValues[bmpPos + 1] = bwValue;//G
                        bwValues[bmpPos + 2] = bwValue;//R
                        bwValues[bmpPos + 3] = 255;//A
                    }
                }
                // Copy the RGB values back to the bitmap
                System.Runtime.InteropServices.Marshal.Copy(bwValues, 0, ptrBW, nBytesLen);

                // Unlock the bits.
                OutputImage.UnlockBits(bmpData);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }

            return OutputImage;
        }

        private void ShowViewer(object sender, EventArgs e)
        {
            Bitmap? hoverImage = null;

            if (sender == null)
                return;

            int senderTag = (int)((PictureBox)sender).Tag;
            hoverImage = (Bitmap)((PictureBox)sender).Image;

            if (hoverImage != null)
            {
                if (hoverImage.Width < viewer.ClientSize.Width || hoverImage.Height < viewer.ClientSize.Height)
                    viewer.ClientSize = new Size(hoverImage.Width, hoverImage.Height + 16);
                viewer.pbViewer.Image = hoverImage;
                viewer.viewerRequested = true;
            }
        }

        private void groupBox1_Click(object sender, EventArgs e)
        {
            viewer.viewerRequested = false;
        }

        private void MainForm_Click(object sender, EventArgs e)
        {
            viewer.viewerRequested = false;
        }

        private void MainForm_DoubleClick(object sender, EventArgs e)
        {
            viewer.viewerRequested = false;
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            viewer.viewerRequested = false;
        }

        private void pbThumbnail_Click(object sender, EventArgs e)
        {
            viewerFormTimer.Interval = 100;
            ShowViewer(sender, e);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            viewerFormTimer.Stop();
            viewer.Close();
            e.Cancel = false;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "https://www.scratchapixel.com/lessons/procedural-generation-virtual-worlds/procedural-patterns-noise-part-1/introduction.html",
                UseShellExecute = true
            };
            Process.Start(psi);

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "https://www.scratchapixel.com/lessons/procedural-generation-virtual-worlds/perlin-noise-part-2/perlin-noise.html",
                UseShellExecute = true
            };
            Process.Start(psi);

        }


        private void tbFSAmplitudeMultiplier_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = !float.TryParse(tbFSAmplitudeMultiplier.Text, out float parsed) || parsed <= 0.0f;
        }
        private void tbFSFrequency_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = !float.TryParse(tbFSFrequency.Text, out float parsed) || parsed <= 0.0f;
        }

        private void tbFSFrequencyMultiplier_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = !float.TryParse(tbFSFrequencyMultiplier.Text, out float parsed) || parsed <= 0.0f;
        }

        private void tbImageWidth_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = !int.TryParse(tbImageWidth.Text, out int parsed) || parsed <= 0;
        }

        private void tbSeamlessOvlp_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = !int.TryParse(tbSeamlessOvlp.Text, out int parsed) || parsed <= 0;
        }
        private void tbImageHeight_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = !int.TryParse(tbImageHeight.Text, out int parsed) || parsed <= 0;
        }

        private void tbFSFrequency_Validated(object sender, EventArgs e)
        {
            float _frequency = float.Parse(tbFSFrequency.Text);
            if (_frequency != frequency)
            {
                frequency = _frequency;
                bNeedsRedraw = true;
            }
        }
        private void tbFSFrequencyMultiplier_Validated(object sender, EventArgs e)
        {
            float _frequencyMult = float.Parse(tbFSFrequencyMultiplier.Text);
            if (_frequencyMult != fBm_lacunarity)
            {
                fBm_lacunarity = _frequencyMult;
                bNeedsRedraw = true;
            }
        }

        private void tbFSAmplitudeMultiplier_Validated(object sender, EventArgs e)
        {
            float _amplitudeMult = float.Parse(tbFSAmplitudeMultiplier.Text);
            if (_amplitudeMult != fBm_gain)
            {
                fBm_gain = _amplitudeMult;
                bNeedsRedraw = true;
            }
        }
        private void udFSnLayers_ValueChanged(object sender, EventArgs e)
        {
            int _numLayers = (int)udFSnLayers.Value;
            if (_numLayers != numLayers)
            {
                numLayers = _numLayers;
                bNeedsRedraw = true;
            }
        }

        private void udSeed_ValueChanged(object sender, EventArgs e)
        {
            int _seed = (int)udSeed.Value;
            if (_seed != seed)
            {
                seed = _seed;
                bNeedsRedraw = true;
            }
        }

        private void tbImageWidth_Validated(object sender, EventArgs e)
        {
            int _width = int.Parse(tbImageWidth.Text);
            if (_width != baseWidth)
            {
                baseWidth = _width;
                bNeedsRedraw = true;
            }
        }
        private void tbImageHeight_Validated(object sender, EventArgs e)
        {
            int _height = int.Parse(tbImageHeight.Text);
            if (_height != baseHeight)
            {
                baseHeight = _height;
                bNeedsRedraw = true;
            }
        }


        private void tbSeamlessOvlp_Validated(object sender, EventArgs e)
        {
            int _seamlessOvlp = int.Parse(tbSeamlessOvlp.Text);
            if (_seamlessOvlp != seamlessOvlp)
            {
                seamlessOvlp = _seamlessOvlp;
                bNeedsRedraw = true;
            }
        }



        private void cbTableSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            uint _tableSize = uint.Parse(cbTableSize.Text);
            if (_tableSize != tableSize)
            {
                tableSize = _tableSize;
                bNeedsRedraw = true;
            }
        }

        private void btSaveToCSV_Click(object sender, EventArgs e)
        {
            CultureInfo cultureUS = new CultureInfo("en-US");
            float[]? bufferToExport = null;
            string tag = (string)((Control)sender).Tag;
            Bitmap? imgToExport = null;

            switch (int.Parse(tag))
            {
                case 0:
                    bufferToExport = noiseBufferValue; 
                    imgToExport = OutputImageValue;
                    break;
                case 1:
                    bufferToExport = noiseBufferTurb; 
                    imgToExport = OutputImageTurb;
                    break;
                case 2:
                    bufferToExport = noiseBufferPerlin; 
                    imgToExport = OutputImagePerlin;
                    break;
                case 3:
                    bufferToExport = noiseBufferFractal;  
                    imgToExport = OutputImageFractal;
                    break;
                case 4:  
                    bufferToExport = noiseBufferMarble; 
                    imgToExport = OutputImageMarble;
                    break;
                case 5:
                    bufferToExport = noiseBufferFractalPerlin;  
                    imgToExport = OutputImageFractalPerlin;
                    break;
            }

            try
            {
                saveFileDialog1.Filter = "Csv files (*.csv)|*.csv";
                saveFileDialog1.RestoreDirectory = true;

                if(imgToExport != null && bufferToExport != null && DialogResult.OK == saveFileDialog1.ShowDialog())
                {
                    FileStream fs = new FileStream(saveFileDialog1.FileName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                    StreamWriter sw = new StreamWriter(fs);

                    int _width = imgToExport.Width;
                    int _height = imgToExport.Height;

                    string matlab_cheatcode = "";

                    sw.WriteLine($"#y=linspace(0,{_height},{_height});");
                    matlab_cheatcode += $"y=linspace(0,{_height},{_height});\r\n";

                    sw.WriteLine($"#x=linspace(0,{_width},{_width});");
                    matlab_cheatcode += $"x=linspace(0,{_width},{_width});\r\n";

                    sw.WriteLine($"#[XX,YY]=meshgrid(x,y);");
                    matlab_cheatcode += $"[XX,YY]=meshgrid(x,y);\r\n";

                    sw.WriteLine($"#surf(XX,YY,{Path.GetFileNameWithoutExtension(saveFileDialog1.FileName)});");
                    matlab_cheatcode += $"surf(XX,YY,{Path.GetFileNameWithoutExtension(saveFileDialog1.FileName)});";


                    for (int idxHeight = 0; idxHeight < _height; idxHeight++)
                    {
                        string line = "";
                        for (int idxWidth = 0; idxWidth < _width; idxWidth++)
                        {
                            line += bufferToExport[idxHeight * _width + idxWidth].ToString("F7", cultureUS) + ",";
                        }
                        line = line.Trim([',']);
                        sw.WriteLine(line);
                    }
                    sw.Close();
                    fs.Close();

                    Clipboard.SetText(matlab_cheatcode);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }
        }



        private void btSetDefaults_Click(object sender, EventArgs e)
        {
            SetDefaults();
        }


        private void btToggleSeamless_Click(object sender, EventArgs e)
        {
            bMakeSeamless = !bMakeSeamless;
            bNeedsRedraw = true;

            if (bMakeSeamless)
                btMakeSeamless.Text = "Disable seamless";
            else
                btMakeSeamless.Text = "Enable seamless";
        }


        float[] GetTestPattern(bool bBullseye = true)
        {
            int outImageWidth = getActualWidth();
            int outImageHeight = getActualHeight(); 

            int diagonalLength = ((outImageWidth + outImageHeight) / 10 / 2) * 2;

            float[] testPatternBuffer = new float[outImageWidth * outImageHeight];

            for (int j = 0; j < outImageHeight; ++j)
            {
                int blackLength = j % diagonalLength + 1;
                int whiteLength = diagonalLength - blackLength;
                for (int i = 0; i < outImageWidth; ++i)
                {
                    int patpos = i % diagonalLength;
                    testPatternBuffer[j * outImageWidth + i] = patpos < blackLength ? 0.0f : 1.0f;
                }
            }

            if (bBullseye)
            {
                for (int j = -diagonalLength / 2; j <= diagonalLength / 2; ++j)
                {
                    for (int i = -diagonalLength / 2; i <= diagonalLength / 2; ++i)
                    {
                        float color = 0.0f;

                        if ((i + 0.5f) * (j + 0.5f) > 0.0f)
                            color = 1.0f;

                        testPatternBuffer[(j + outImageHeight / 2) * outImageWidth + (i + outImageWidth / 2)] = color;
                    }
                }
            }


            int cj = outImageHeight / 2 - 1;
            int ci = outImageWidth / 2 - 1;

            testPatternBuffer[(cj) * outImageWidth + (ci)] = 0.0f;
            testPatternBuffer[(cj + 1) * outImageWidth + (ci + 1)] = 0.0f;
            testPatternBuffer[(cj + 1) * outImageWidth + (ci)] = 1.0f;
            testPatternBuffer[(cj) * outImageWidth + (ci + 1)] = 1.0f;

            return testPatternBuffer;
        }

        private void btSaveToPNG_Click(object sender, EventArgs e)
        {
            Bitmap? imgToExport = null;
            string tag = (string)((Control)sender).Tag;

            switch (int.Parse(tag))
            {
                case 0:
                    imgToExport = OutputImageValue;
                    break;
                case 1:
                    imgToExport = OutputImageTurb;
                    break;
                case 2:
                    imgToExport = OutputImagePerlin;
                    break;
                case 3:
                    imgToExport = OutputImageFractal;
                    break;
                case 4:
                    imgToExport = OutputImageMarble;
                    break;
                case 5:
                    imgToExport = OutputImageFractalPerlin;
                    break;
            }

            try
            {
                saveFileDialog1.Filter = "PNG files (*.png)|*.png";
                saveFileDialog1.RestoreDirectory = true;

                if (imgToExport != null && DialogResult.OK == saveFileDialog1.ShowDialog())
                {
                    imgToExport.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Png);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }
        }

        private void cbOvlpInterpolator_SelectedIndexChanged(object sender, EventArgs e)
        {
            bNeedsRedraw = true;
        }
    }
}

