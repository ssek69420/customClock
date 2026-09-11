using System;
using System.Windows;
using System.Windows.Media;

namespace project
{
    public partial class AlarmPopup : Window
    {
        private readonly MediaPlayer _mediaPlayer;

        public AlarmPopup(Alarm alarm)
        {
            InitializeComponent();

            TimeText.Text = alarm.timestring;

            if (!string.IsNullOrEmpty(alarm.RingtonePath))
            {
                RingtoneText.Text = System.IO.Path.GetFileName(
                    alarm.RingtonePath
                );
            }
            else
            {
                RingtoneText.Text = "No ringtone";
            }

            _mediaPlayer = new MediaPlayer();

            if (!string.IsNullOrEmpty(alarm.RingtonePath))
            {
                try
                {
                    _mediaPlayer.Open(
                        new Uri(alarm.RingtonePath)
                    );

                    _mediaPlayer.MediaEnded += (s, e) =>
                    {
                        _mediaPlayer.Position = TimeSpan.Zero;
                        _mediaPlayer.Play();
                    };

                    _mediaPlayer.Play();
                }
                catch
                {
                    RingtoneText.Text = "Could not play ringtone";
                }
            }
        }

        private void DismissButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            _mediaPlayer.Stop();
            Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            _mediaPlayer.Stop();
            _mediaPlayer.Close();

            base.OnClosed(e);
        }
    }
}