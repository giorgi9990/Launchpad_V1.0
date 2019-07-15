using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Launchpad
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

        enum MouseClickCounter
        {
            firstclick,
            secondclick,
            thridclick,
        }
        MouseClickCounter mouseClickCounter = MouseClickCounter.firstclick;
        private void LoopGif1_Click(object sender, EventArgs e)
        {
            PictureBox LoopPicturebox = (sender as PictureBox);
            if (mouseClickCounter == MouseClickCounter.firstclick)
            {
                LoopPicturebox.Image = Properties.Resources.YellowLooperButton;
                AudioService.RecordSounds(firstloopTimer, "../../Assets/RecordSound/Track1.wav", 1, loopGif1);
                mouseClickCounter = MouseClickCounter.secondclick;
            }
            else if (mouseClickCounter == MouseClickCounter.secondclick)
            {
                AudioService.StopRecordSound(firstloopTimer, "../../Assets/RecordSound/Track1.wav");
                LoopPicturebox.Image = Properties.Resources.BlueLooperButton;
                mouseClickCounter = MouseClickCounter.thridclick;
            }
            else if (mouseClickCounter == MouseClickCounter.thridclick)
            {
                LoopPicturebox.Image = Properties.Resources.RedLooperButton;
                firstloopTimer.Stop();
                loopGif1.Enabled = false;
            }
        }
        bool secondLoopsecondClick = false, thirdLoopsecondClick = false, fourthloopsecondClick = false, fifthloopsecondClick = false, sixthloopsecondClick = false;
            
        private void LoopRecordClick(object sender, EventArgs e)
        {
            PictureBox LoopPicturebox = (sender as PictureBox);
            switch (LoopPicturebox.Name)
            {
                case "loopGif2":
                    SwichFunction(secondLoopsecondClick, LoopPicturebox,secondloopTimer, "../../Assets/RecordSound/Track2.wav", int.Parse(loopGif2Label.Text));
                    secondLoopsecondClick = true;
                    break;
                case "loopGif3":
                    SwichFunction(thirdLoopsecondClick, LoopPicturebox, thirdloopTimer, "../../Assets/RecordSound/Track3.wav", int.Parse(loopGif3Label.Text));
                    thirdLoopsecondClick = true;
                    break;
                case "loopGif4":
                    SwichFunction(fourthloopsecondClick, LoopPicturebox, fourthloopTimer, "../../Assets/RecordSound/Track4.wav", int.Parse(loopGif4Label.Text));
                    fourthloopsecondClick = true;
                    break;
                case "loopGif5":
                    SwichFunction(fifthloopsecondClick, LoopPicturebox, fifthloopTimer, "../../Assets/RecordSound/Track5.wav", int.Parse(loopGif5Label.Text));
                    fifthloopsecondClick = true;
                    break;
                case "loopGif6":
                    SwichFunction(sixthloopsecondClick, LoopPicturebox, sixthloopTimer, "../../Assets/RecordSound/Track6.wav", int.Parse(loopGif6Label.Text));
                    sixthloopsecondClick = true;
                    break;
                default:
                    break;
            }
        }

        #region Timer_Tick
        private void FirstloopTimer_Tick(object sender, EventArgs e)
        {
          AudioService.PlaySound("../../Assets/RecordSound/Track1.wav");
        }
      
        private void SecondloopTimer_Tick(object sender, EventArgs e)
        {
            AudioService.PlaySound("../../Assets/RecordSound/Track2.wav");
            EnableButton();
        }

        private void ThirdloopTimer_Tick(object sender, EventArgs e)
        {
            AudioService.PlaySound("../../Assets/RecordSound/Track3.wav");
            EnableButton();
        }

        private void FourthloopTimer_Tick(object sender, EventArgs e)
        {
            AudioService.PlaySound("../../Assets/RecordSound/Track4.wav");
            EnableButton();
        }

        private void FifthloopTimer_Tick(object sender, EventArgs e)
        {
            AudioService.PlaySound("../../Assets/RecordSound/Track5.wav");
            EnableButton();
        }

        private void SixthloopTimer_Tick(object sender, EventArgs e)
        {
            AudioService.PlaySound("../../Assets/RecordSound/Track6.wav");
            EnableButton();
        }
        #endregion

        #region HelperFunction
        private void MinusLabel(object sender, EventArgs e)
        {
            Label LctionLabel = (sender as Label);
            Label LoopLabel = (Label)this.Controls[LctionLabel.Tag.ToString()];

            if (int.Parse(LoopLabel.Text) > 1)
            {
                LoopLabel.Text = (int.Parse(LoopLabel.Text) - 1).ToString();
            }
        }

        private void PlusLabel(object sender, EventArgs e)
        {
            Label LctionLabel = (sender as Label);
            Label LoopLabel = (Label)this.Controls[LctionLabel.Tag.ToString()];

            if (int.Parse(LoopLabel.Text) < 4)
            {
                LoopLabel.Text = (int.Parse(LoopLabel.Text) + 1).ToString();
            }
        }
        private void DisableButton()
        {
            loopGif1.Enabled = false;
            loopGif2.Enabled = false;
            loopGif3.Enabled = false;
            loopGif4.Enabled = false;
            loopGif5.Enabled = false;
            loopGif6.Enabled = false;
        }
        private void EnableButton()
        {
            loopGif1.Enabled = loopGif1.Image == Properties.Resources.RedLooperButton ? false : true;
            loopGif2.Enabled = loopGif2.Image == Properties.Resources.RedLooperButton ? false : true;
            loopGif3.Enabled = loopGif3.Image == Properties.Resources.RedLooperButton ? false : true;
            loopGif4.Enabled = loopGif4.Image == Properties.Resources.RedLooperButton ? false : true;
            loopGif5.Enabled = loopGif5.Image == Properties.Resources.RedLooperButton ? false : true;
            loopGif6.Enabled = loopGif6.Image == Properties.Resources.RedLooperButton ? false : true;
        }

        public void SwichFunction(bool Secondclick, PictureBox LoopPicturebox, Timer timer, string name, int interval)
        {
            if (!Secondclick)
            {
                AudioService.RecordSounds(timer, name, interval, LoopPicturebox);
                DisableButton();
                LoopPicturebox.Image = Properties.Resources.YellowLooperButton;
            }
            else
            {
                LoopPicturebox.Image = Properties.Resources.RedLooperButton;
                timer.Stop();
                LoopPicturebox.Enabled = false;
            }
        } 
        #endregion

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
    }
}
