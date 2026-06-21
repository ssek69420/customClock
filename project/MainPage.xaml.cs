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
            Random random = new Random();
            if(DateTime.Now > DateTime.Today.AddHours(12))
            {
                string[] phrasesTwelve = {"what's up dude", "hey there!", "good to see you!", "what are you doing?"};
                int rand_i = random.Next(phrasesTwelve.Length);
                Label.Text = phrasesTwelve[rand_i];
            }
            else
            {
                Label.Text = "*yawn*...";
            }
            if(DateTime.Now > DateTime.Today.AddHours(18))
            {
                string[] phrasesEighteen = { "time to get a lil sleep, don't you think?", "it's getting late, maybe you should rest?", "you look tired, maybe you should sleep?" };
                int rand_i = random.Next(phrasesEighteen.Length);
                Label.Text = phrasesEighteen[rand_i];
                if (DateTime.Now > DateTime.Today.AddHours(23))
                {
                    string[] phrasesTwentyThree = { "dude... GO TO SLEEP!", "it's really late, you should sleep!", "you look like a zombie, go to sleep!" };
                    int rand_i_t = random.Next(phrasesTwentyThree.Length);
                    Label.Text = phrasesTwentyThree[rand_i_t];
                }
            }
        }

        private void openAlarms_clhandler(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new OpenAlarms());
        }
   }
}