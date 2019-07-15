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
using System.IO;

namespace Launchpad
{
    public partial class Launchpad : Form
    {
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
            string loc = @"../../Assets/wav/";
            ButoonColor = new Dictionary<string, ButtonsInfo>()
            {
                 {"D1",new ButtonsInfo((PictureBox)this.Controls["_1Button"], Properties.Resources.YellowButton,Properties.Resources.WhiteButton,$"{loc}01.wav")},
                {"D2",new ButtonsInfo((PictureBox)this.Controls["_2Button"], Properties.Resources.YellowButton,Properties.Resources.WhiteButton,$"{loc}02.wav")},
                {"D3",new ButtonsInfo((PictureBox)this.Controls["_3Button"], Properties.Resources.YellowButton,Properties.Resources.WhiteButton,$"{loc}03.wav")},
                {"D4",new ButtonsInfo((PictureBox)this.Controls["_4Button"], Properties.Resources.BlueButton,Properties.Resources.WhiteButton,$"{loc}04.wav")},
                {"D5",new ButtonsInfo((PictureBox)this.Controls["_5Button"], Properties.Resources.BlueButton,Properties.Resources.WhiteButton,$"{loc}05.wav")},
                {"D6",new ButtonsInfo((PictureBox)this.Controls["_6Button"], Properties.Resources.BlueButton, Properties.Resources.WhiteButton, $"{loc}06.wav")},
                {"Q",new ButtonsInfo((PictureBox)this.Controls["qButton"], Properties.Resources.YellowButton,Properties.Resources.WhiteButton, $"{loc}07.wav")},
                {"W",new ButtonsInfo((PictureBox)this.Controls["wButton"], Properties.Resources.YellowButton,Properties.Resources.WhiteButton, $"{loc}08.wav")},
                {"E",new ButtonsInfo((PictureBox)this.Controls["eButton"], Properties.Resources.YellowButton,Properties.Resources.WhiteButton, $"{loc}09.wav")},
                {"R",new ButtonsInfo((PictureBox)this.Controls["rButton"], Properties.Resources.BlueButton,Properties.Resources.WhiteButton, $"{loc}10.wav")},
                {"T",new ButtonsInfo((PictureBox)this.Controls["tButton"], Properties.Resources.BlueButton,Properties.Resources.WhiteButton, $"{loc}11.wav")},
                {"Y",new ButtonsInfo((PictureBox)this.Controls["yButton"], Properties.Resources.BlueButton,Properties.Resources.WhiteButton, $"{loc}12.wav")},
                {"A",new ButtonsInfo((PictureBox)this.Controls["aButton"], Properties.Resources.YellowButton,Properties.Resources.WhiteButton, $"{loc}13.wav")},
                {"S",new ButtonsInfo((PictureBox)this.Controls["sButton"], Properties.Resources.YellowButton,Properties.Resources.WhiteButton, $"{loc}14.wav")},
                {"D",new ButtonsInfo((PictureBox)this.Controls["dButton"], Properties.Resources.left_up_yellow,Properties.Resources.left_up_white, $"{loc}15.wav")},
                {"F",new ButtonsInfo((PictureBox)this.Controls["fButton"], Properties.Resources.right_up_blue,Properties.Resources.right_up_white, $"{loc}16.wav")},
                {"G",new ButtonsInfo((PictureBox)this.Controls["gButton"], Properties.Resources.BlueButton,Properties.Resources.WhiteButton, $"{loc}17.wav")},
                {"H",new ButtonsInfo((PictureBox)this.Controls["hButton"], Properties.Resources.BlueButton,Properties.Resources.WhiteButton, $"{loc}18.wav")},
                {"Z",new ButtonsInfo((PictureBox)this.Controls["zButton"], Properties.Resources.GreenButton,Properties.Resources.WhiteButton, $"{loc}19.wav")},
                {"X",new ButtonsInfo((PictureBox)this.Controls["xButton"], Properties.Resources.GreenButton,Properties.Resources.WhiteButton, $"{loc}20.wav")},
                {"C",new ButtonsInfo((PictureBox)this.Controls["cButton"], Properties.Resources.left_down_green,Properties.Resources.left_down_white, $"{loc}21.wav")},
                {"V",new ButtonsInfo((PictureBox)this.Controls["vButton"], Properties.Resources.right_down_red,Properties.Resources.right_down_white, $"{loc}22.wav")},
                {"B",new ButtonsInfo((PictureBox)this.Controls["bButton"], Properties.Resources.RedButton,Properties.Resources.WhiteButton, $"{loc}23.wav")},
                {"N",new ButtonsInfo((PictureBox)this.Controls["nButton"], Properties.Resources.RedButton,Properties.Resources.WhiteButton, $"{loc}24.wav")},
                {"D7",new ButtonsInfo((PictureBox)this.Controls["_7Button"], Properties.Resources.GreenButton,Properties.Resources.WhiteButton, $"{loc}25.wav")},
                {"D8",new ButtonsInfo((PictureBox)this.Controls["_8Button"], Properties.Resources.GreenButton,Properties.Resources.WhiteButton, $"{loc}26.wav")},
                {"D9",new ButtonsInfo((PictureBox)this.Controls["_9Button"], Properties.Resources.GreenButton,Properties.Resources.WhiteButton, $"{loc}27.wav")},
                {"D0",new ButtonsInfo((PictureBox)this.Controls["_0Button"], Properties.Resources.RedButton,Properties.Resources.WhiteButton, $"{loc}28.wav")},
                {"OemMinus",new ButtonsInfo((PictureBox)this.Controls["minusButton"], Properties.Resources.RedButton,Properties.Resources.WhiteButton, $"{loc}29.wav")},
                {"Oemplus",new ButtonsInfo((PictureBox)this.Controls["equalsButton"], Properties.Resources.RedButton,Properties.Resources.WhiteButton, $"{loc}30.wav")},
                {"U",new ButtonsInfo((PictureBox)this.Controls["uButton"], Properties.Resources.GreenButton,Properties.Resources.WhiteButton, $"{loc}31.wav")},
                {"I",new ButtonsInfo((PictureBox)this.Controls["iButton"], Properties.Resources.GreenButton,Properties.Resources.WhiteButton, $"{loc}32.wav")},
                {"O",new ButtonsInfo((PictureBox)this.Controls["oButton"], Properties.Resources.GreenButton,Properties.Resources.WhiteButton, $"{loc}33.wav")},
                {"P",new ButtonsInfo((PictureBox)this.Controls["pButton"], Properties.Resources.RedButton,Properties.Resources.WhiteButton, $"{loc}34.wav")},
                {"OemOpenBrackets",new ButtonsInfo((PictureBox)this.Controls["leftBracketButton"], Properties.Resources.RedButton,Properties.Resources.WhiteButton, $"{loc}35.wav")},
                {"Oem6",new ButtonsInfo((PictureBox)this.Controls["rightBracketButton"], Properties.Resources.RedButton,Properties.Resources.WhiteButton, $"{loc}36.wav")},

            };
        }

        private void Launchpad_KeyDown(object sender, KeyEventArgs e)
        {
            if (ButoonColor.Keys.Contains(e.KeyCode.ToString()))
            {
                ButoonColor[e.KeyCode.ToString()].PictureBox.Image = ButoonColor[e.KeyCode.ToString()].NewImage;
                AudioService.PlaySound(ButoonColor[e.KeyCode.ToString()].AudioName);
            }
        }
        private void Launchpad_KeyUp(object sender, KeyEventArgs e)
        {
            if (ButoonColor.Keys.Contains(e.KeyCode.ToString()))
            {
                ButoonColor[e.KeyCode.ToString()].PictureBox.Image = ButoonColor[e.KeyCode.ToString()].OldImage;
            }
        }

        #region FormButtons
        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void menuButton_Click(object sender, EventArgs e)
        {
            this.Location = new Point(this.Location.X - 200, this.Location.Y);
            Loop loop = new Loop(new AudioService());
            loop.Show();
            loop.Location = new Point(this.Location.X + 810, this.Location.Y);
        }

        private void minimizeButton_Click(object sender, EventArgs e)
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
        #endregion

        private void ChangeMusic_Event(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "wav files (*.wav)|*.wav";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ButoonColor.Values.FirstOrDefault(x => x.PictureBox.Name == (sender as PictureBox).Name).AudioName= dialog.FileName;
            }
        }
    }
}
