using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Windows.Forms;
using System.Media;
namespace WindowsFormsApp3
{
    public class AudioService : IAudioService
    {
        private WaveFileReader WaveFileReader;
        private DirectSoundOut DirectSoundOut;

        public void PlaySound(string file)
        {
            WaveFileReader = new WaveFileReader(file);
            DirectSoundOut = new DirectSoundOut();
            DirectSoundOut.Init(new WaveChannel32(WaveFileReader));
            DirectSoundOut.Play();
        }

        private WaveIn waveSource;
        private WaveFileWriter waveFile;
        public int firstrecordDuration { get; set; } = 8000;
        private Timer RecordTimer;

        public void RecordSounds(Timer timer, string name, int number, PictureBox LoopPicutrebox)
        {
            RecordTimer = new Timer();
            RecordTimer.Interval = firstrecordDuration * number;
            SetTimeInterval(timer, number);
            RecordTimer.Tick += delegate (object sender2, EventArgs e2)
            {
                RecordSoundDelegat(new object(), new EventArgs(), timer, LoopPicutrebox, name);
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

        public void StopRecordSound(Timer timer ,string name)
        {
            if (firstrecordDuration == 8000)
            {
                firstrecordDuration = int.Parse(waveFile.TotalTime.TotalMilliseconds.ToString());
                timer.Interval = firstrecordDuration;
            }

            waveSource.StopRecording();
            waveFile.Close();
            RecordTimer.Stop();
            PlaySound(name);
            timer.Start();
        }
       
        #region Private Function
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
        private void RecordSoundDelegat(object sender, EventArgs e, Timer timer, PictureBox pictureBox, string name)
        {
            StopRecordSound(timer, name);
            pictureBox.Image = Properties.Resources.BlueLooperButton;
            RecordTimer.Stop();
        } 
        #endregion
    }
}
