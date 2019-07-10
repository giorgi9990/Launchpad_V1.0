using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3.Services
{
    public class AudioService : IAudioService
    {
        private WasapiOut wasapiOut;
        private AudioFileReader audioFileReader;
        private WaveIn waveSource;
        private WaveFileWriter waveFile;
        public int firstrecordDuration { get; set; } = 0;

        public void PlaySound(string file)
        {
            waveSource?.StopRecording();
            waveFile?.Close();
            AudioClientShareMode shareMode = AudioClientShareMode.Shared;
            wasapiOut = new WasapiOut(shareMode, 0);
            wasapiOut.Volume = 1f;
            audioFileReader = new AudioFileReader(file);
            audioFileReader.Volume = 1f;
            wasapiOut.Init(audioFileReader);
            wasapiOut.Play();
        }

        public void RecordSound(string name, Timer timer)
        {
            waveSource = new WaveIn();
            waveSource.WaveFormat = new WaveFormat();
            waveSource.DataAvailable += new EventHandler<WaveInEventArgs>(waveSource_DataAvailable);
            waveSource.RecordingStopped += new EventHandler<StoppedEventArgs>(waveSource_RecordingStopped);
            waveFile = new WaveFileWriter(name, waveSource.WaveFormat);
            waveSource.StartRecording();
            if (firstrecordDuration != 0)
            {
                timer.Start();
            }
        }

        public void SetTimeInterval(Timer timer, int interval)
        {
            timer.Interval = firstrecordDuration * interval;
        }

        public void StopRecordSoundO(Timer timer)
        {
            waveSource.StopRecording();
            waveFile.Close();
            if (firstrecordDuration == 0)
            {
                firstrecordDuration = int.Parse(waveFile.TotalTime.TotalMilliseconds.ToString());
                timer.Interval = firstrecordDuration;
            }

            timer.Start();
        }
        private void waveSource_RecordingStopped(object sender, StoppedEventArgs e)
        {
            if (waveSource != null)
            {
                waveSource.Dispose();
                waveSource = null;
            }

            if (waveFile != null)
            {
                waveFile.Dispose();
                waveFile = null;
            }
        }

        private void waveSource_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (waveFile != null)
            {
                waveFile.Write(e.Buffer, 0, e.BytesRecorded);
                waveFile.Flush();
            }
        }
    }
}
