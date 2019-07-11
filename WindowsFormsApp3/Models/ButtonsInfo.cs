using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    class ButtonsInfo
    {
        public ButtonsInfo(PictureBox pictureBoxName, Bitmap newImage, string audioName)
        {
            PictureBox = pictureBoxName;
            NewImage = newImage;
            AudioName = audioName;
        }

        public PictureBox PictureBox { get; set; }
        public Bitmap NewImage { get; set; }
        public string AudioName { get; set; }
    }
}
