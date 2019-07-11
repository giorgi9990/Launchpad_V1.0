using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;
using NAudio.CoreAudioApi;


namespace WindowsFormsApp3
{
    public partial class Launchpad : Form
    {
        WasapiOut wasapiOut;
        AudioFileReader audioFileReader;
        private bool mouseDown;
        private Point lastLocation;
        public IAudioService AudioService { get; }
        Dictionary<string, ButtonsInfo> ButoonColor = null;

        public Launchpad(IAudioService audioService)
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.TransparencyKey = Color.FromKnownColor(KnownColor.InactiveBorder);
            InitializeComponent();
            AudioService = audioService;
            ButoonColor = new Dictionary<string, ButtonsInfo>()
            {
                {"D1",new ButtonsInfo((PictureBox)this.Controls["_1Button"], Properties.Resources.YellowButton, "01.wav")},
                {"D2",new ButtonsInfo((PictureBox)this.Controls["_2Button"], Properties.Resources.YellowButton, "02.wav")},
                {"D3",new ButtonsInfo((PictureBox)this.Controls["_3Button"], Properties.Resources.YellowButton, "03.wav")},
                {"D4",new ButtonsInfo((PictureBox)this.Controls["_4Button"], Properties.Resources.YellowButton, "04.wav")},
                {"D5",new ButtonsInfo((PictureBox)this.Controls["_5Button"], Properties.Resources.YellowButton, "04.wav")},
                {"D6",new ButtonsInfo((PictureBox)this.Controls["_6Button"], Properties.Resources.YellowButton, "03.wav")},
                {"Q",new ButtonsInfo((PictureBox)this.Controls["qButton"], Properties.Resources.YellowButton, "04.wav")},
                {"W",new ButtonsInfo((PictureBox)this.Controls["wButton"], Properties.Resources.YellowButton, "04.wav")},
                {"E",new ButtonsInfo((PictureBox)this.Controls["eButton"], Properties.Resources.YellowButton, "04.wav")},
                {"R",new ButtonsInfo((PictureBox)this.Controls["rButton"], Properties.Resources.YellowButton, "04.wav")},
                {"T",new ButtonsInfo((PictureBox)this.Controls["tButton"], Properties.Resources.YellowButton, "04.wav")},
                {"Y",new ButtonsInfo((PictureBox)this.Controls["yButton"], Properties.Resources.YellowButton, "04.wav")},
                {"A",new ButtonsInfo((PictureBox)this.Controls["aButton"], Properties.Resources.YellowButton, "04.wav")},
                {"S",new ButtonsInfo((PictureBox)this.Controls["sButton"], Properties.Resources.YellowButton, "04.wav")},
                {"D",new ButtonsInfo((PictureBox)this.Controls["dButton"], Properties.Resources.YellowButton, "04.wav")},
                {"F",new ButtonsInfo((PictureBox)this.Controls["fButton"], Properties.Resources.YellowButton, "04.wav")},
                {"G",new ButtonsInfo((PictureBox)this.Controls["gButton"], Properties.Resources.YellowButton, "04.wav")},
                {"H",new ButtonsInfo((PictureBox)this.Controls["hButton"], Properties.Resources.YellowButton, "04.wav")},
                {"Z",new ButtonsInfo((PictureBox)this.Controls["zButton"], Properties.Resources.YellowButton, "04.wav")},
                {"X",new ButtonsInfo((PictureBox)this.Controls["xButton"], Properties.Resources.YellowButton, "04.wav")},
                {"C",new ButtonsInfo((PictureBox)this.Controls["cButton"], Properties.Resources.YellowButton, "04.wav")},
                {"V",new ButtonsInfo((PictureBox)this.Controls["vButton"], Properties.Resources.YellowButton, "04.wav")},
                {"B",new ButtonsInfo((PictureBox)this.Controls["bButton"], Properties.Resources.YellowButton, "04.wav")},
                {"N",new ButtonsInfo((PictureBox)this.Controls["nButton"], Properties.Resources.YellowButton, "04.wav")},
                {"D7",new ButtonsInfo((PictureBox)this.Controls["_7Button"], Properties.Resources.YellowButton, "04.wav")},
                {"D8",new ButtonsInfo((PictureBox)this.Controls["_8Button"], Properties.Resources.YellowButton, "04.wav")},
                {"D9",new ButtonsInfo((PictureBox)this.Controls["_9Button"], Properties.Resources.YellowButton, "04.wav")},
                {"D0",new ButtonsInfo((PictureBox)this.Controls["_0Button"], Properties.Resources.YellowButton, "04.wav")},
                {"OemMinus",new ButtonsInfo((PictureBox)this.Controls["minusButton"], Properties.Resources.YellowButton, "04.wav")},
                {"Oemplus",new ButtonsInfo((PictureBox)this.Controls["equalsButton"], Properties.Resources.YellowButton, "04.wav")},
                {"U",new ButtonsInfo((PictureBox)this.Controls["uButton"], Properties.Resources.YellowButton, "04.wav")},
                {"I",new ButtonsInfo((PictureBox)this.Controls["iButton"], Properties.Resources.YellowButton, "04.wav")},
                {"O",new ButtonsInfo((PictureBox)this.Controls["oButton"], Properties.Resources.YellowButton, "04.wav")},
                {"P",new ButtonsInfo((PictureBox)this.Controls["pButton"], Properties.Resources.YellowButton, "04.wav")},
                {"OemOpenBrackets",new ButtonsInfo((PictureBox)this.Controls["leftBracketButton"], Properties.Resources.YellowButton, "04.wav")},
                {"Oem6",new ButtonsInfo((PictureBox)this.Controls["rightBracketButton"], Properties.Resources.YellowButton, "04.wav")},

            };
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

      
        private void Launchpad_KeyDown(object sender, KeyEventArgs e)
        {
            if (ButoonColor.Keys.Contains(e.KeyCode.ToString()))
            {
                ButoonColor[e.KeyCode.ToString()].PictureBox.Image = ButoonColor[e.KeyCode.ToString()].NewImage;
            }
        }
        private void Launchpad_KeyUp(object sender, KeyEventArgs e)
        {
            if (ButoonColor.Keys.Contains(e.KeyCode.ToString()))
            {
                ButoonColor[e.KeyCode.ToString()].PictureBox.Image = Properties.Resources.WhiteButton;
            }
        }


        private void pictureBox42_Click(object sender, EventArgs e)
        {
            this.Location = new Point(this.Location.X - 200, this.Location.Y);
            Loop loop = new Loop(new AudioService());
            loop.Show();
            loop.Location = new Point(this.Location.X + 810, this.Location.Y);
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
