using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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
            foreach (Alarm alarm in _alarms)
            {
                if (!alarm.IsEnabled)
                    continue;
                if (alarm.timestring == currentTime)
                {
                    TriggerAlarm(alarm);
                }
            }
        }

        private void TriggerAlarm(Alarm alarm)
        {
            alarm.IsEnabled = false;

            AlarmPopup popup = new AlarmPopup(alarm);

            popup.ShowDialog();

            RefreshAlarmCards();
        }

        private void RefreshAlarmCards()
        {
            ActiveAlarmsPanel.Children.Clear();

            foreach (Alarm alarm in _alarms)
            {
                CreateAlarmCard(alarm);
            }

            AlarmCountText.Text = _alarms.Count.ToString();
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

        private string? _selectedRingtonePath;

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

        private void CreateAlarmCard(Alarm alarm)
        {
            Border card = new Border
            {
                CornerRadius = new CornerRadius(18),
                Background = new SolidColorBrush(
                    Color.FromArgb(45, 255, 255, 255)
                ),
                BorderBrush = new SolidColorBrush(
                    Color.FromArgb(80, 255, 105, 180)
                ),
                BorderThickness = new Thickness(1),
                Padding = new Thickness(16),
                Margin = new Thickness(0, 0, 0, 12)
            };

            Grid cardGrid = new Grid();

            cardGrid.ColumnDefinitions.Add(
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
            );

            cardGrid.ColumnDefinitions.Add(
                new ColumnDefinition { Width = GridLength.Auto }
            );

            // =========================
            // LEFT SIDE
            // =========================

            StackPanel information = new StackPanel();

            TextBlock timeText = new TextBlock
            {
                Text = alarm.timestring,
                FontSize = 30,
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.White
            };

            string ringtoneName = "No ringtone";

            if (!string.IsNullOrEmpty(alarm.RingtonePath))
            {
                ringtoneName =
                    System.IO.Path.GetFileName(alarm.RingtonePath);
            }

            TextBlock ringtoneText = new TextBlock
            {
                Text = "🔔  " + ringtoneName,
                FontSize = 12,
                Foreground = new SolidColorBrush(
                    Color.FromArgb(190, 255, 255, 255)
                ),
                Margin = new Thickness(0, 4, 0, 0),
                TextTrimming = TextTrimming.CharacterEllipsis,
                MaxWidth = 230
            };

            information.Children.Add(timeText);
            information.Children.Add(ringtoneText);

            Grid.SetColumn(information, 0);

            // =========================
            // RIGHT SIDE BUTTONS
            // =========================

            StackPanel buttons = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                VerticalAlignment = VerticalAlignment.Center
            };

            Button toggleButton = new Button
            {
                Content = alarm.IsEnabled ? "ON" : "OFF",
                Width = 55,
                Height = 35,
                Margin = new Thickness(5, 0, 5, 0),
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.White,
                Background = alarm.IsEnabled
                    ? new SolidColorBrush(
                        Color.FromArgb(130, 50, 180, 100))
                    : new SolidColorBrush(
                        Color.FromArgb(100, 100, 100, 100)),
                BorderBrush = Brushes.Transparent,
                Cursor = Cursors.Hand
            };

            toggleButton.Click += (s, e) =>
            {
                alarm.IsEnabled = !alarm.IsEnabled;

                RefreshAlarmCards();
            };


            Button removeButton = new Button
            {
                Content = "✕",
                Width = 35,
                Height = 35,
                Margin = new Thickness(5, 0, 0, 0),
                FontSize = 14,
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.White,
                Background = new SolidColorBrush(
                    Color.FromArgb(100, 180, 40, 80)
                ),
                BorderBrush = new SolidColorBrush(
                    Color.FromArgb(150, 255, 100, 140)
                ),
                BorderThickness = new Thickness(1),
                Cursor = Cursors.Hand
            };

            removeButton.Click += (s, e) =>
            {
                _alarms.Remove(alarm);

                RefreshAlarmCards();
            };


            buttons.Children.Add(toggleButton);
            buttons.Children.Add(removeButton);

            Grid.SetColumn(buttons, 1);

            cardGrid.Children.Add(information);
            cardGrid.Children.Add(buttons);

            card.Child = cardGrid;

            ActiveAlarmsPanel.Children.Add(card);
        }

        private void AddAlarmButton_Click(object sender, RoutedEventArgs e)
        {
            Alarm newAlarm = new Alarm
            {
                Hours = DateTime.Now.Hour,
                Minutes = DateTime.Now.Minute,
                Seconds = DateTime.Now.Second,
                RingtonePath = _selectedRingtonePath ?? ""
            };

            _alarms.Add(newAlarm);

            RefreshAlarmCards();
        }

        //previously named "tensec", but, in reality, it adds 5 seconds.
        //Reason is that 10 had too much waiting time. By going with 5, it seems more manageable.
        private void Addalarm_tensec_click(object sender, RoutedEventArgs e)
        {
            DateTime go_off = DateTime.Now.AddSeconds(5);
            Alarm newAlarm = new Alarm
            {
                Hours = DateTime.Now.Hour,
                Minutes = DateTime.Now.Minute,
                Seconds = go_off.Second,
                RingtonePath = _selectedRingtonePath ?? ""
            };

            _alarms.Add(newAlarm);
            RefreshAlarmCards();
        }
    }
}