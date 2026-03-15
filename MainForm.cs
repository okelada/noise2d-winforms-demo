using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
//using wifitracelistener;
using System.Threading.Tasks;
using System.Globalization;

namespace Noise2D
{
    public partial class MainForm : Form
    {
        ViewerForm viewer = new ViewerForm();
        System.Windows.Forms.Timer viewerFormTimer = new System.Windows.Forms.Timer();
        //wifiTraceListener myTraceListener;

        int width;
        int height;
        float frequency;
        float fBm_lacunarity;
        float fBm_gain;
        int numLayers;
        int seed;
        uint tableSize;

        bool bNeedsRedraw;

        float[]? noiseBufferValue;
        float[]? noiseBufferFractal;
        float[]? noiseBufferTurb;
        float[]? noiseBufferMarble;
        float[]? noiseBufferPerlin;
        float[]? noiseBufferFractalPerlin;


        public MainForm()
        {
            InitializeComponent();
            //myTraceListener = new wifiTraceListener(lvLog, true, "LOG", TraceEventType.Verbose, 600);

            foreach (Control control in Controls)
                control.MouseClick += RedirectMouseClick;

            groupBox1.Click += groupBox1_Click;

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
            width = 1024;
            height = 768;
            frequency = .01f;
            fBm_lacunarity = 2.0f;
            fBm_gain = 0.5f;
            numLayers = 4;
            seed = 2016;
            tableSize = 256;

            tbImageWidth.Text = width.ToString();
            tbImageHeight.Text = height.ToString();
            tbFSFrequency.Text = frequency.ToString();
            tbFSFrequencyMultiplier.Text = fBm_lacunarity.ToString();
            tbFSAmplitudeMultiplier.Text = fBm_gain.ToString();
            udFSnLayers.Value = numLayers;
            udSeed.Value = seed;
            cbTableSize.Text = tableSize.ToString();

            bNeedsRedraw = true;
        }


        private void Mainform_Load(object sender, EventArgs e)
        {
            SetDefaults();

            ResetAll();
            viewerFormTimer.Tick += ViewerFormTimer_Tick;
            viewerFormTimer.Interval = 500;
            viewerFormTimer.Start();

            //valueNoise1D.TestModulo();
        }


        private void EnableControls(bool bDisable)
        {
            tbImageWidth.Enabled = bDisable;
            tbImageHeight.Enabled = bDisable;
            tbFSFrequency.Enabled = bDisable;
            tbFSFrequencyMultiplier.Enabled = bDisable;
            tbFSAmplitudeMultiplier.Enabled = bDisable;
            udFSnLayers.Enabled = bDisable;
            udSeed.Enabled = bDisable;
            cbTableSize.Enabled = bDisable;
        }

        private void ViewerFormTimer_Tick(object? sender, EventArgs e)
        {
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

                Bitmap? OutputImageFractalPerlin = null;
                Bitmap? OutputImageValue = null;
                Bitmap? OutputImageFractal = null;
                Bitmap? OutputImageTurb = null;
                Bitmap? OutputImageMarble = null;
                Bitmap? OutputImagePerlin = null;

                pbFractalPerlin.Image = null;
                pbValueNoise.Image = null;
                pbFractalValue.Image = null;
                pbTurbNoise.Image = null;
                pbMarbleNoise.Image = null;
                pbPerlinNoise.Image = null;


                try
                {
                    Parallel.Invoke(
                    () =>
                    {
                        ValueNoise valueNoise = new ValueNoise(width, height, frequency, seed, tableSize);
                        noiseBufferValue = valueNoise.GetNoiseBuffer();
                        OutputImageValue = RenderBWBuffer(noiseBufferValue, width, height);
                    },
                    () =>
                    {
                        ValueNoise valueNoise = new ValueNoise(width, height, frequency, seed, tableSize);
                        noiseBufferFractal = valueNoise.GetFractalNoiseBuffer(fBm_lacunarity, fBm_gain, numLayers);
                        OutputImageFractal = RenderBWBuffer(noiseBufferFractal, width, height);
                    },
                    () =>
                    {
                        ValueNoise valueNoise = new ValueNoise(width, height, frequency, seed, tableSize);
                        noiseBufferTurb = valueNoise.GetTurbulenceNoiseBuffer(fBm_lacunarity, fBm_gain, numLayers);
                        OutputImageTurb = RenderBWBuffer(noiseBufferTurb, width, height);
                    },
                    () =>
                    {
                        ValueNoise valueNoise = new ValueNoise(width, height, frequency, seed, tableSize);
                        noiseBufferMarble = valueNoise.GetMarbleNoiseBuffer(fBm_lacunarity, fBm_gain, numLayers);
                        OutputImageMarble = RenderBWBuffer(noiseBufferMarble, width, height);
                    },
                    () =>
                    {
                        PerlinNoise perlinNoise = new PerlinNoise(width, height, frequency, seed, tableSize);
                        noiseBufferPerlin = perlinNoise.GetNoiseBuffer();
                        OutputImagePerlin = RenderBWBuffer(noiseBufferPerlin, width, height);
                    },
                    () =>
                    {
                        PerlinNoise perlinNoise = new PerlinNoise(width, height, frequency, seed, tableSize);
                        noiseBufferFractalPerlin = perlinNoise.GetFractalNoiseBuffer(fBm_lacunarity, fBm_gain, numLayers);
                        OutputImageFractalPerlin = RenderBWBuffer(noiseBufferFractalPerlin, width, height);
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

                for (int pos = 0; pos < nPixels; pos++)
                {
                    byte bwValue = (byte)(noiseBuffer[pos] * 255.0f);//BW
                    bwValues[4 * pos + 0] = bwValue;//B
                    bwValues[4 * pos + 1] = bwValue;//G
                    bwValues[4 * pos + 2] = bwValue;//R
                    bwValues[4 * pos + 3] = 255;//A
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
                    viewer.ClientSize = new Size(hoverImage.Width, hoverImage.Height);
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
            if (_width != width)
            {
                width = _width;
                bNeedsRedraw = true;
            }
        }
        private void tbImageHeight_Validated(object sender, EventArgs e)
        {
            int _height = int.Parse(tbImageHeight.Text);
            if (_height != height)
            {
                height = _height;
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

            switch (int.Parse(tag))
            {
                case 0:
                    bufferToExport = noiseBufferValue;
                    break;
                case 1:
                    bufferToExport = noiseBufferTurb;
                    break;
                case 2:
                    bufferToExport = noiseBufferPerlin;
                    break;
                case 3:
                    bufferToExport = noiseBufferFractal;
                    break;
                case 4:
                    bufferToExport = noiseBufferMarble;
                    break;
                case 5:
                    bufferToExport = noiseBufferFractalPerlin;
                    break;
            }

            try
            {
                saveFileDialog1.Filter = "Csv files (*.csv)|*.csv";
                saveFileDialog1.RestoreDirectory = true;

                if (bufferToExport != null && DialogResult.OK == saveFileDialog1.ShowDialog())
                {
                    FileStream fs = new FileStream(saveFileDialog1.FileName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                    StreamWriter sw = new StreamWriter(fs);

                    sw.WriteLine($"#y=linspace(0,{height},{height});");
                    sw.WriteLine($"#x=linspace(0,{width},{width});");
                    sw.WriteLine($"#[XX,YY]=meshgrid(x,y);");
                    sw.WriteLine($"#surf(XX,YY,{Path.GetFileNameWithoutExtension(saveFileDialog1.FileName)});");


                    for (int idxHeight = 0; idxHeight < height; idxHeight++)
                    {
                        string line = "";
                        for (int idxWidth = 0; idxWidth < width; idxWidth++)
                        {
                            line += bufferToExport[idxHeight*width + idxWidth].ToString("F7", cultureUS) + ",";
                        }
                        line = line.Trim([',']);
                        sw.WriteLine(line);
                    }
                    sw.Close();
                    fs.Close();

            
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
    }
}

