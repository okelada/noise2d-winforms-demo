using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
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



        private string GetFormTitle()
        {
            if (pbViewer.Image != null)
            {
                if (pbViewer.SizeMode != PictureBoxSizeMode.StretchImage)
                    return "Zoom 100% - right click to fit";
                else
                    return "Zoom " + (100 * pbViewer.Width) / pbViewer.Image.Width + "% - right click to 1:1";
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
            else
            {
                if (pbViewer.SizeMode != PictureBoxSizeMode.StretchImage)
                {
                    if (pbViewer.Image != null)
                    {
                        pbViewer.SizeMode = PictureBoxSizeMode.StretchImage;
                        pbViewer.Dock = DockStyle.Fill;
                        Text = GetFormTitle();
                    }
                }
                else
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
            pbViewer.SizeMode = PictureBoxSizeMode.StretchImage;
            pbViewer.Dock = DockStyle.Fill;
            Text = GetFormTitle();
        }
    }
}
