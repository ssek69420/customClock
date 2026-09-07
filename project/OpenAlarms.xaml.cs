using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace project
{
    public partial class OpenAlarms : Page
    {
        public OpenAlarms()
        {
            InitializeComponent();

            _hours = 0;
            _minutes = 00;
            _seconds = 0;

            UpdateTimeDisplay();
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
            string alarmTime =
                $"{_hours:D2}:{_minutes:D2}:{_seconds:D2}";

            StackPanel alarmContainer = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(0, 0, 0, 10)
            };

            TextBlock alarm = new TextBlock
            {
                Text = alarmTime,
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