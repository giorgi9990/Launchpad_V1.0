using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    class ButtonsInfo
    {
        public ButtonsInfo(PictureBox pictureBoxName, Bitmap newImage, Bitmap oldImage, string audioName)
        {
            PictureBox = pictureBoxName;
            NewImage = newImage;
            OldImage = oldImage;
            AudioName = audioName;
        }

        public PictureBox PictureBox { get; set; }
        public Bitmap NewImage { get; set; }
        public Bitmap OldImage { get; set; }
        public string AudioName { get; set; }
    }
}
