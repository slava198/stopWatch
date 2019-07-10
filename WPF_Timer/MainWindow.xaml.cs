using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace WPF_Timer
{

    public partial class MainWindow : Window
    {
        private const string defaultTime = "00:00:00.000";
        private DispatcherTimer timer;
        private DateTime startTime;
        private TimeSpan currentTime;
        private int lapIndex;


        public MainWindow()
        {
            InitializeComponent();
        }


        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            tbTime.Text = defaultTime;
            lapIndex = 1;
            startTime = DateTime.Now;
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            timer.Tick += Timer_Tick;
            progressBar.IsIndeterminate = true;
            timer.Start();
            tbLaps.Clear();
            btnStart.IsEnabled = false;
            btnLap.IsEnabled = true;
            btnStop.IsEnabled = true;
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            currentTime = DateTime.Now - startTime;
            tbTime.Text = $"{currentTime.Hours:00}:{currentTime.Minutes:00}:{currentTime.Seconds:00}.{currentTime.Milliseconds:000}";//$"{time/3600:00}:{time/60:00}:{time%60:00}:000";
        }


        private void BtnLap_Click(object sender, RoutedEventArgs e)
        {
            tbLaps.Text += $"Lap {lapIndex++:000} time: {tbTime.Text}{Environment.NewLine}";
            tbLaps.ScrollToEnd();
        }


        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            if (btnStop.Content.ToString() == "Stop")
            {
                timer.Stop();
                tbLaps.Text += $" FINISH  time: {tbTime.Text}{Environment.NewLine}";
                tbLaps.ScrollToEnd();
                progressBar.IsIndeterminate = false;
                btnStart.IsEnabled = false;
                btnLap.IsEnabled = false;
                btnStop.Content = "Clear";
            }
            else
            {
                tbTime.Text = defaultTime;
                tbLaps.Clear();
                btnStart.IsEnabled = true;
                btnLap.IsEnabled = false;
                btnStop.IsEnabled = false;
                btnStop.Content = "Stop";
            }
        }


        private void TrayIconCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            if (mainWindow.IsVisible == false)
            {
                mainWindow.Show();
                mainWindow.ShowInTaskbar = true;
            }
            else
            {
                mainWindow.Hide();
                mainWindow.ShowInTaskbar = false;
            }

        }
    }



    public class WindowCommands
    {
        static WindowCommands()
        {
            HideShowWindowCommand = new RoutedCommand("HideShowWindowCommand", typeof(MainWindow));
        }
        public static RoutedCommand HideShowWindowCommand { get; set; }
    }

}
