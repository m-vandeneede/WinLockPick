using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinLockPick
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Helpers.TaskbarHelper.Show();
            this.Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            this.TopMost = true;

            Rectangle rect = new Rectangle(7, 4, 500, 500);
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(rect);
            Region region = new Region(path);

            this.backgroundPictureBox.Image = Helpers.ImageHelper.GetLockscreenWallpaper();
            this.backgroundPictureBox.Size = new Size(Screen.PrimaryScreen.Bounds.Width+2, Screen.PrimaryScreen.Bounds.Height+2);

            this.accountImagePictureBox.Region = region;

            this.accountImagePictureBox.Image = Helpers.ImageHelper.GetAccountImage();
        }

        private void backgroundPictureBox_Click(object sender, EventArgs e)
        {

        }
    }
}
