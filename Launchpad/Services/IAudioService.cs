using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Launchpad
{
   public interface IAudioService
    {
        void PlaySound(string file);
        void RecordSounds(Timer timer, string name, int number, PictureBox loopPicutrebox);
        void StopRecordSound(Timer timer, string name);
        void SetTimeInterval(Timer timer, int interval);
    }
}
