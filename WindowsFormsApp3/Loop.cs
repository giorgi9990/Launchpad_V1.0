using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace WindowsFormsApp3
{
    public partial class Loop : Form
    {
        private bool mouseDown;
        private Point lastLocation;
        private FrameDimension dimension;
        private int frameCount;
        private int indexToPaint;
        private Timer timer = new Timer();

        public Loop()
        {
            InitializeComponent();

            dimension = new FrameDimension(this.loopGif1.Image.FrameDimensionsList[0]);
            frameCount = this.loopGif1.Image.GetFrameCount(dimension);
            this.loopGif1.Paint += new PaintEventHandler(loopGif_Paint);
            timer.Interval = 25;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            indexToPaint++;
            if (indexToPaint >= frameCount)
            {
                indexToPaint = 0;
            }
        }


        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void loopGif_Paint(object sender, PaintEventArgs e)
        {
            this.loopGif1.Image.SelectActiveFrame(dimension, indexToPaint);
            e.Graphics.DrawImage(this.loopGif1.Image, Point.Empty);
        }

        private void pictureBox38_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }

        private void Loop_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Loop_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void Loop_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
    }
}
