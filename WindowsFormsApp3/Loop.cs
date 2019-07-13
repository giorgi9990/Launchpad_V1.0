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
using System.Data;
namespace WindowsFormsApp3
{
    public partial class Loop : Form
    {
        private bool mouseDown;
        private Point lastLocation;

        public IAudioService AudioService { get; }

        public Loop(IAudioService audioService)
        {
            InitializeComponent();
            AudioService = audioService;
        }

        #region WindowMoveFunction
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
        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void pictureBox38_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }

        #endregion

        bool secondclick = false;

        private void LoopGif1_Click(object sender, EventArgs e)
        {
            if (!secondclick)
            {
                loopGif1.Enabled = true;

                AudioService.RecordSounds(firstloopTimer,"RecordSound/Track1.wav");
                secondclick = true;
            }
            else
            {
                AudioService.StopRecordSound(firstloopTimer);
            }
        }

        private void LoopRecordClick(object sender, EventArgs e)
        {
            switch ((sender as PictureBox).Name)
            {
                case "loopGif2":
                    AudioService.RecordSounds(secondloopTimer, "RecordSound/Track2.wav");
                    break;
                case "loopGif3":
                    AudioService.RecordSounds(thirdloopTimer, "RecordSound/Track3.wav");
                    break;
                case "loopGif4":
                    AudioService.RecordSounds(fourthloopTimer, "RecordSound/Track4.wav");
                    break;
                case "loopGif5":
                    AudioService.RecordSounds(fifthloopTimer, "RecordSound/Track5.wav");
                    break;
                case "loopGif6":
                    AudioService.RecordSounds(sixthloopTimer, "RecordSound/Track6.wav");
                    break;
                default:
                    break;
            }
        }

        #region Timer_Tick
        private void FirstloopTimer_Tick(object sender, EventArgs e)
        {
            AudioService.PlaySound("RecordSound/Track1.wav");
        }
        private void SecondloopTimer_Tick(object sender, EventArgs e)
        {
            AudioService.PlaySound("RecordSound/Track2.wav");
        }

        private void ThirdloopTimer_Tick(object sender, EventArgs e)
        {
            AudioService.PlaySound("RecordSound/Track3.wav");
        }

        private void FourthloopTimer_Tick(object sender, EventArgs e)
        {
            AudioService.PlaySound("RecordSound/Track4.wav");
        }

        private void FifthloopTimer_Tick(object sender, EventArgs e)
        {
            AudioService.PlaySound("RecordSound/Track5.wav");
        }

        private void SixthloopTimer_Tick(object sender, EventArgs e)
        {
            AudioService.PlaySound("RecordSound/Track6.wav");
        }









        #endregion

       
    }
}
