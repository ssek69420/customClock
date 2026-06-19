using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace project
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private readonly DispatcherTimer _timer;
        public MainPage()
        {
            InitializeComponent();
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;

            UpdateClock();
            Change_label();
            _timer.Start();
        }
        //shows the current time
        private void UpdateClock()
        {
            ClockText.Text = DateTime.Now.ToString("HH:mm:ss");
        }
        private void Timer_Tick(object? sender, EventArgs e)
        {
            UpdateClock();
        }

        private void Change_label()
        {
            if(DateTime.Now > DateTime.Today.AddHours(12))
            {
                Label.Text = "what's up dude";
            }
            else
            {
                Label.Text = "*yawn*...";
            }
            if(DateTime.Now > DateTime.Today.AddHours(18))
            {
                Label.Text = "time to get a lil sleep, don't you think?";
                if (DateTime.Now > DateTime.Today.AddHours(23))
                {
                    Label.Text = "dude... GO TO SLEEP!";
                }
            }
        }

        private void openAlarms_clhandler(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new OpenAlarms());
        }
   }
}