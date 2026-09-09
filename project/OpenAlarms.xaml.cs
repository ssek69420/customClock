using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;
using System.Windows.Threading;

namespace project
{
    public class Alarm
    {
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }

        public required string RingtonePath { get; set; }
        public bool IsEnabled { get; set; } = true;
        public string timestring =>
        $"{Hours:D2}:{Minutes:D2}:{Seconds:D2}";

    }
    public partial class OpenAlarms : Page
    {
        private void AlarmTimer_Tick(object sender, EventArgs e)
        {
            DateTime _cTime = DateTime.Now;
            string currentTime = _cTime.ToString("HH:mm:ss");
            foreach(Alarm alarm in _alarms){
                if(!alarm.IsEnabled)
                    continue;
                if(alarm.timestring == currentTime){
                    TriggerAlarm(alarm);
                }
            }
        }

        private void TriggerAlarm(Alarm alarm)
        {
            alarm.IsEnabled = false;
            if (!string.IsNullOrEmpty(alarm.RingtonePath))
            {
                _mediaplayer = new MediaPlayer();
                try
                {
                    _mediaplayer.Open(new Uri(alarm.RingtonePath));
                    _mediaplayer.Play();
                }
                catch (Exception)
                {
                    MessageBox.Show(
                        "Ringtone could not be played.",
                        "Alarm",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning
                    );
                }
            }
            MessageBox.Show(
                alarm.timestring,
                "Alarm",
                MessageBoxButton.OK
            );
        }
        public OpenAlarms()
        {
            InitializeComponent();

            _hours = 0;
            _minutes = 00;
            _seconds = 0;

            UpdateTimeDisplay();

            _alarmTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };

            _alarmTimer.Tick += AlarmTimer_Tick;
            _alarmTimer.Start();
        }

        private readonly List<Alarm> _alarms = new List<Alarm>();

        private readonly DispatcherTimer _alarmTimer;

        private MediaPlayer _mediaplayer;

        private string _selectedRingtonePath;

        private void SelectRingtoneButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Audio Files|*.mp3;*.wav;*.wma;*.aac;*.flac"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                _selectedRingtonePath = openFileDialog.FileName;
                RingtoneText.Text = _selectedRingtonePath;
            }
        }
        private int _hours;
        private int _minutes;
        private int _seconds;

        private void UpdateTimeDisplay()
        {
            HourText.Text = _hours.ToString("D2");
            MinuteText.Text = _minutes.ToString("D2");
            SecondText.Text = _seconds.ToString("D2");
        }

        private void HourUp_Click(object sender, RoutedEventArgs e)
        {
            _hours++;

            if (_hours > 23)
                _hours = 0;

            UpdateTimeDisplay();
        }

        private void HourDown_Click(object sender, RoutedEventArgs e)
        {
            _hours--;

            if (_hours < 0)
                _hours = 23;

            UpdateTimeDisplay();
        }

        private void MinuteUp_Click(object sender, RoutedEventArgs e)
        {
            _minutes++;

            if (_minutes > 59)
                _minutes = 0;

            UpdateTimeDisplay();
        }

        private void MinuteDown_Click(object sender, RoutedEventArgs e)
        {
            _minutes--;

            if (_minutes < 0)
                _minutes = 59;

            UpdateTimeDisplay();
        }

        private void SecondUp_Click(object sender, RoutedEventArgs e)
        {
            _seconds++;

            if (_seconds > 59)
                _seconds = 0;

            UpdateTimeDisplay();
        }

        private void SecondDown_Click(object sender, RoutedEventArgs e)
        {
            _seconds--;

            if (_seconds < 0)
                _seconds = 59;

            UpdateTimeDisplay();
        }

        private void AddAlarmButton_Click(object sender, RoutedEventArgs e)
        {
            Alarm newAlarm = new Alarm
            {
                Hours = _hours,
                Minutes = _minutes,
                Seconds = _seconds,
                RingtonePath = _selectedRingtonePath
            };

            _alarms.Add(newAlarm);

            StackPanel alarmContainer = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(0, 0, 0, 10)
            };

            TextBlock alarm = new TextBlock
            {
                Text = newAlarm.ToString(),
                FontSize = 20,
                Foreground = Brushes.White,
                VerticalAlignment = VerticalAlignment.Center,
                Width = 150
            };

            Button removeButton = new Button
            {
                Content = "✕",
                Width = 35,
                Height = 35,
                Background = Brushes.Transparent,
                Foreground = Brushes.Red,
                BorderBrush = Brushes.Red,
                BorderThickness = new Thickness(1),
                Cursor = System.Windows.Input.Cursors.Hand
            };

            removeButton.Click += (s, args) =>
            {
                ActiveAlarmsPanel.Children.Remove(alarmContainer);

                AlarmCountText.Text =
                    ActiveAlarmsPanel.Children.Count.ToString();
            };

            alarmContainer.Children.Add(alarm);
            alarmContainer.Children.Add(removeButton);

            ActiveAlarmsPanel.Children.Add(alarmContainer);

            AlarmCountText.Text =
                ActiveAlarmsPanel.Children.Count.ToString();

        }
    }
}