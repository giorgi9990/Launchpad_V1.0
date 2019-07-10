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


        public Launchpad()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.TransparencyKey = Color.FromKnownColor(KnownColor.InactiveBorder);
           
            InitializeComponent();
        }

        
        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            #region Numbers 1 2 3 4 5 6
            if (e.KeyChar == '1')
            {
                _1Button.Image = Properties.Resources.YellowButton;
                PlaySound("01.wav");
            }

            if (e.KeyChar == '2')
            {
                _2Button.Image = Properties.Resources.YellowButton;
                PlaySound("02.wav");
            }

            if (e.KeyChar == '3')
            {
                _3Button.Image = Properties.Resources.YellowButton;
                PlaySound("03.wav");
            }

            if (e.KeyChar == '4')
            {
                _4Button.Image = Properties.Resources.BlueButton;
                PlaySound("04.wav");
            }

            if (e.KeyChar == '5')
            {
                _5Button.Image = Properties.Resources.BlueButton;
                PlaySound("05.wav");
            }

            if (e.KeyChar == '6')
            {
                _6Button.Image = Properties.Resources.BlueButton;
                PlaySound("06.wav");
            }
            #endregion

            #region q w e r t y
            if (e.KeyChar == 'q' || e.KeyChar == 'Q')
            {
                qButton.Image = Properties.Resources.YellowButton;
                PlaySound("q.wav");
            }

            if (e.KeyChar == 'w' || e.KeyChar == 'W')
            {
                wButton.Image = Properties.Resources.YellowButton;
                PlaySound("w.wav");
            }

            if (e.KeyChar == 'e' || e.KeyChar == 'E')
            {
                eButton.Image = Properties.Resources.YellowButton;
                PlaySound("e.wav");
            }

            if (e.KeyChar == 'r' || e.KeyChar == 'R')
            {
                rButton.Image = Properties.Resources.BlueButton;
                PlaySound("r.wav");
            }

            if (e.KeyChar == 't' || e.KeyChar == 'T')
            {
                tButton.Image = Properties.Resources.BlueButton;
                PlaySound("t.wav");
            }

            if (e.KeyChar == 'y' || e.KeyChar == 'Y')
            {
                yButton.Image = Properties.Resources.BlueButton;
                PlaySound("y.wav");
            } 
            #endregion

            if (e.KeyChar == 'f' || e.KeyChar == 'F')
            {
                fButton.Image = Properties.Resources.right_up_blue;
                
            }

            if (e.KeyChar == 'd' || e.KeyChar == 'D')
            {
                dButton.Image = Properties.Resources.left_up_yellow;
            }

            if (e.KeyChar == 'c' || e.KeyChar == 'C')
            {
                cButton.Image = Properties.Resources.left_down_green;
            }

            if (e.KeyChar == 'v' || e.KeyChar == 'V')
            {
                vButton.Image = Properties.Resources.right_down_red;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            #region Numbers 1 2 3 4 5 6
            if (e.KeyCode == Keys.D1)
            {
                _1Button.Image = Properties.Resources.WhiteButton;
            }

            if (e.KeyCode == Keys.D2)
            {
                _2Button.Image = Properties.Resources.WhiteButton;
            }

            if (e.KeyCode == Keys.D3)
            {
                _3Button.Image = Properties.Resources.WhiteButton;
            }

            if (e.KeyCode == Keys.D4)
            {
                _4Button.Image = Properties.Resources.WhiteButton;
            }

            if (e.KeyCode == Keys.D5)
            {
                _5Button.Image = Properties.Resources.WhiteButton;
            }

            if (e.KeyCode == Keys.D6)
            {
                _6Button.Image = Properties.Resources.WhiteButton;
            }
            #endregion

            if (e.KeyCode.ToString() == "q" || e.KeyCode.ToString() == "Q")
            {
                qButton.Image = Properties.Resources.WhiteButton;
            }

            if (e.KeyCode.ToString() == "w" || e.KeyCode.ToString() == "W")
            {
                wButton.Image = Properties.Resources.WhiteButton;
            }

            if (e.KeyCode.ToString() == "e" || e.KeyCode.ToString() == "E")
            {
                eButton.Image = Properties.Resources.WhiteButton;
            }

            if (e.KeyCode.ToString() == "r" || e.KeyCode.ToString() == "R")
            {
                rButton.Image = Properties.Resources.WhiteButton;
            }

            if (e.KeyCode.ToString() == "t" || e.KeyCode.ToString() == "T")
            {
                tButton.Image = Properties.Resources.WhiteButton;
            }

            if (e.KeyCode.ToString() == "y" || e.KeyCode.ToString() == "Y")
            {
                yButton.Image = Properties.Resources.WhiteButton;
            }



            if (e.KeyCode.ToString() == "f" || e.KeyCode.ToString() == "F")
            {
                fButton.Image = Properties.Resources.right_up_white;
            }

            if (e.KeyCode.ToString() == "d" || e.KeyCode.ToString() == "D")
            {
                dButton.Image = Properties.Resources.left_up_white;
            }

            if (e.KeyCode.ToString() == "c" || e.KeyCode.ToString() == "C")
            {
                cButton.Image = Properties.Resources.left_down_white;
            }

            if (e.KeyCode.ToString() == "v" || e.KeyCode.ToString() == "V")
            {
                vButton.Image = Properties.Resources.right_down_white;
            }
        }

        private void PlaySound(string file)
        {
            AudioClientShareMode shareMode = AudioClientShareMode.Shared;
            wasapiOut = new WasapiOut(shareMode, useEventSync: true, 50);
            //wasapiOut.Volume = 1;
            try
            {
                audioFileReader = new AudioFileReader(file);
            }
            catch (Exception)
            {
                
            }
            //audioFileReader.Volume = 1;
            wasapiOut.Init(audioFileReader);
            wasapiOut.Play();
        }

        private void pictureBox42_Click(object sender, EventArgs e)
        {
            this.Location = new Point(this.Location.X - 200, this.Location.Y);
            Loop loop = new Loop();
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
