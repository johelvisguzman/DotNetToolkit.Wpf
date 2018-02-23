namespace DotNetToolkit.Wpf.Metro.Demo.Views
{
    using System;
    using System.Threading;
    using System.Windows;

    /// <summary>
    /// Interaction logic for BusyIndicatorExampleView.xaml
    /// </summary>
    public partial class BusyIndicatorExampleView
    {
        public BusyIndicatorExampleView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int busySeconds = (int)BusySeconds.SelectedItem;
            int delayMilliseconds = (int)DelayMilliseconds.SelectedItem;
            SampleIndicator.DisplayAfter = TimeSpan.FromMilliseconds(delayMilliseconds);

            // Simulate a long-running task by sleeping on a worker thread
            DataContext = true;
            ThreadPool.QueueUserWorkItem((state) =>
            {
                Thread.Sleep(busySeconds * 1000);
                Dispatcher.BeginInvoke(new Func<object>(() => DataContext = false));
            });
        }
    }
}
