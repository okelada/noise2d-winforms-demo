using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Noise2D
{
    public partial class ViewerForm : Form
    {
        private Size OldClientSize;
        public bool viewerRequested = false;
   
        public ViewerForm()
        {
            InitializeComponent();
        }

        private void ViewerForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    viewerRequested = false;
                   // Debug.WriteLine("ESC");
                    break;
            }
        }

        private float GetZoom()
        {
            if (pbViewer.Image != null)
                return (100.0f * pbViewer.Width) / pbViewer.Image.Width;
            else
                return 0f;
        }


        private string GetFormTitle()
        {
            if (pbViewer.Image != null)
            {
                float zoom = GetZoom();

                if (pbViewer.SizeMode != PictureBoxSizeMode.Zoom)
                    return $"{pbViewer.Image.Width}x{pbViewer.Image.Height}   Zoom 100% - right click to fit";
                else
                    return $"{pbViewer.Image.Width}x{pbViewer.Image.Height}   Zoom {zoom}%" + (zoom != 100f?" - right click to 1:1":"");
            }
            else
                return "Viewer";
        }


        private void pbViewer_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                viewerRequested = false;
            }
            else //right
            {
                if (pbViewer.SizeMode != PictureBoxSizeMode.Zoom)
                {
                    if (pbViewer.Image != null)
                    {
                        pbViewer.SizeMode = PictureBoxSizeMode.Zoom;
                        pbViewer.Dock = DockStyle.Fill;
                        Text = GetFormTitle();
                    }
                }
                else
                if(GetZoom()!=100f)
                {
                    pbViewer.SizeMode = PictureBoxSizeMode.AutoSize;
                    pbViewer.Dock = DockStyle.None;
                    Text = GetFormTitle();
                }
            }
        }

        private void ViewerForm_Load(object sender, EventArgs e)
        {
            OldClientSize = ClientSize;
        }

        private void ViewerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            viewerRequested = false;
        }

        private void pbViewer_VisibleChanged(object sender, EventArgs e)
        {
            pbViewer.SizeMode = PictureBoxSizeMode.Zoom;
            pbViewer.Dock = DockStyle.Fill;
            Text = GetFormTitle();
        }

        public void SetImage(Form MainForm, Bitmap theImage)
        {
            if (theImage.Width < ClientSize.Width || theImage.Height < ClientSize.Height)
                ClientSize = new Size(theImage.Width, theImage.Height + 16);
            else
            {
                Size clientSize = new Size(Math.Min(MainForm.ClientSize.Width, theImage.Width),
                    Math.Min(MainForm.ClientSize.Height - 64, theImage.Height + 16));
                ClientSize = clientSize;
            }
            pbViewer.Image = theImage;
            viewerRequested = true;
        }
    }
}
