using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3.Services
{
   public interface IAudioService
    {
        void PlaySound(string file);
        void RecordSound(string name, Timer timer);
        void StopRecordSoundO(Timer timer);
        void SetTimeInterval(Timer timer, int interval);
    }
}
