using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace project
{
    /// <summary>
    /// Interaction logic for OpenAlarms.xaml
    /// </summary>
    public partial class OpenAlarms : Page
    {
        public OpenAlarms()
        {
            InitializeComponent();
        }
        private bool _isUpdating = false;

        private void TimeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_isUpdating)
                return;

            _isUpdating = true;

            TextBox textBox = (TextBox)sender;

            // Keep only digits
            string digits = new string(textBox.Text.Where(char.IsDigit).ToArray());

            // Maximum HHMMSS
            if (digits.Length > 6)
                digits = digits.Substring(0, 6);

            // Pad with leading zeros
            digits = digits.PadLeft(6, '0');

            // Format as HH:MM:SS
            string formatted =
                $"{digits.Substring(0, 2)}:" +
                $"{digits.Substring(2, 2)}:" +
                $"{digits.Substring(4, 2)}";

            textBox.Text = formatted;

            // Put cursor at the end
            textBox.CaretIndex = textBox.Text.Length;

            _isUpdating = false;
        }

        private void AddAlarmButton_Click(object sender, RoutedEventArgs e)
        {
            string alarmTime = TimeTextBox.Text;

            if (string.IsNullOrWhiteSpace(alarmTime))
                return;

            TextBlock alarm = new TextBlock
            {
                Text = alarmTime,
                FontSize = 20,
                Foreground = Brushes.White,
                Margin = new Thickness(0, 0, 0, 10)
            };

            ActiveAlarmsPanel.Children.Add(alarm);

            AlarmCountText.Text = ActiveAlarmsPanel.Children.Count.ToString();
        }
    }
}
