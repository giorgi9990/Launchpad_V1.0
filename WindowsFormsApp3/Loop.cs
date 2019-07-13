using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


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

       

        bool secondclick = false;

        private void LoopGif1_Click(object sender, EventArgs e)
        {
            PictureBox LoopPicturebox = (sender as PictureBox);
            if (!secondclick)
            {
                loopGif1.Enabled = true;
                LoopPicturebox.Image = Properties.Resources.YellowLooperButton;

                AudioService.RecordSounds(firstloopTimer,"RecordSound/Track1.wav",1,loopGif1);
                secondclick = true;
            }
            else
            {
                AudioService.StopRecordSound(firstloopTimer, "RecordSound/Track1.wav");
                LoopPicturebox.Image = Properties.Resources.BlueLooperButton;
            }
        }

        private void LoopRecordClick(object sender, EventArgs e)
        {
            PictureBox LoopPicturebox = (sender as PictureBox);
            switch (LoopPicturebox.Name)
            {
                case "loopGif2":
                    AudioService.RecordSounds(secondloopTimer, "RecordSound/Track2.wav", int.Parse(loopGif2Label.Text),loopGif2);
                    LoopPicturebox.Image = Properties.Resources.YellowLooperButton;
                    break;
                case "loopGif3":
                    AudioService.RecordSounds(thirdloopTimer, "RecordSound/Track3.wav", int.Parse(loopGif3Label.Text), loopGif3);
                    LoopPicturebox.Image = Properties.Resources.YellowLooperButton;
                    break;
                case "loopGif4":
                    AudioService.RecordSounds(fourthloopTimer, "RecordSound/Track4.wav", int.Parse(loopGif4Label.Text),loopGif4);
                    LoopPicturebox.Image = Properties.Resources.YellowLooperButton;
                    break;
                case "loopGif5":
                    AudioService.RecordSounds(fifthloopTimer, "RecordSound/Track5.wav", int.Parse(loopGif5Label.Text), loopGif5);
                    LoopPicturebox.Image = Properties.Resources.YellowLooperButton;
                    break;
                case "loopGif6":
                    AudioService.RecordSounds(sixthloopTimer, "RecordSound/Track6.wav", int.Parse(loopGif6Label.Text), loopGif6);
                    LoopPicturebox.Image = Properties.Resources.YellowLooperButton;
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

            if (int.Parse(LoopLabel.Text) <4 )
            {
                LoopLabel.Text = (int.Parse(LoopLabel.Text) + 1).ToString();
            }
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
    }
}
