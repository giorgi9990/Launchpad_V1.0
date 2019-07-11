using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public class AudioService : IAudioService
    {
        private WasapiOut wasapiOut;
        private AudioFileReader audioFileReader;
        private WaveIn waveSource;
        private WaveFileWriter waveFile;
        public int firstrecordDuration { get; set; } = 8000;
        private Timer RecordTimer;
        public void PlaySound(string file)
        {
            
            AudioClientShareMode shareMode = AudioClientShareMode.Shared;
            wasapiOut = new WasapiOut(shareMode, 0);
            wasapiOut.Volume = 0.5f;
            audioFileReader = new AudioFileReader(file);
            audioFileReader.Volume = 0.5f;
            wasapiOut.Init(audioFileReader);
            wasapiOut.Play();
        }

        public void RecordSounds(Timer timer, string name)
        {
            RecordTimer = new Timer();
            RecordTimer.Interval = firstrecordDuration;
            SetTimeInterval(timer, 1);
            RecordTimer.Tick += delegate (object sender2, EventArgs e2)
            {
                RecordSoundDelegat(new object(), new EventArgs(), timer);
            };
            waveSource = new WaveIn();
            waveSource.WaveFormat = new WaveFormat();
            waveSource.DataAvailable += new EventHandler<WaveInEventArgs>(waveSource_DataAvailable);
            waveSource.RecordingStopped += new EventHandler<StoppedEventArgs>(waveSource_RecordingStopped);
            waveFile = new WaveFileWriter(name, waveSource.WaveFormat);
            waveSource.StartRecording();

            RecordTimer.Start();
        }
       
        public void SetTimeInterval(Timer timer, int interval)
        {
            timer.Interval = firstrecordDuration * interval;
        }

        public void StopRecordSound(Timer timer)
        {
            if (firstrecordDuration == 8000)
            {
                firstrecordDuration = int.Parse(waveFile.TotalTime.TotalMilliseconds.ToString());
                timer.Interval = firstrecordDuration;
            }

            waveSource.StopRecording();
            waveFile.Close();
            RecordTimer.Stop();

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
        private void RecordSoundDelegat(object sender, EventArgs e, Timer timer)
        {
                StopRecordSound(timer);
           
            RecordTimer.Stop();
        }
    }
}
