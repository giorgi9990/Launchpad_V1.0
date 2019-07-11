using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
   public interface IAudioService
    {
        void PlaySound(string file);
        void RecordSounds(Timer timer, string name);
        void StopRecordSound(Timer timer);
        void SetTimeInterval(Timer timer, int interval);
    }
}
