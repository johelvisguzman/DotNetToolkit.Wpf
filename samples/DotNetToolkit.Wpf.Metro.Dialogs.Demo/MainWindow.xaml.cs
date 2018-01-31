namespace DotNetToolkit.Wpf.Metro.Dialogs.Demo
{
    using System.Threading.Tasks;
    using System.Windows;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void ProgressTest_OnClick(object sender, RoutedEventArgs e)
        {
            var mySettings = new ChildWindowDialogSettings
            {
                AllowMove = true,
                NegativeButtonText = "Close now"
            };

            var controller = await this.ShowChildWindowProgressAsync("Please wait...", "We are baking some cupcakes!", true, mySettings);

            controller.SetIndeterminate();

            double i = 0.0;
            while (i < 6.0)
            {
                double val = (i / 100.0) * 20.0;
                controller.SetProgress(val);
                controller.SetMessage("Baking cupcake: " + i + "...");

                if (controller.IsCanceled)
                    break; //canceled progressdialog auto closes.

                i += 1.0;

                await Task.Delay(2000);
            }

            await controller.CloseAsync();

            if (controller.IsCanceled)
            {
                await this.ShowChildWindowMessageAsync("No cupcakes!", "You stopped baking!", MessageDialogStyle.Affirmative, mySettings);
            }
            else
            {
                await this.ShowChildWindowMessageAsync("Cupcakes!", "Your cupcakes are finished! Enjoy!", MessageDialogStyle.Affirmative, mySettings);
            }
        }

        private async void MessageTest_OnClick(object sender, RoutedEventArgs e)
        {
            var mySettings = new ChildWindowDialogSettings
            {
                AllowMove = true,
                AffirmativeButtonText = "Hi",
                NegativeButtonText = "Go away!",
                FirstAuxiliaryButtonText = "Cancel"
            };

            await this.ShowChildWindowMessageAsync("Hello!", "Welcome to the world of metro!", MessageDialogStyle.AffirmativeAndNegativeAndSingleAuxiliary, mySettings);
        }

        private async void InputTest_OnClick(object sender, RoutedEventArgs e)
        {
            var mySettings = new ChildWindowDialogSettings
            {
                AllowMove = true
            };

            var result = await this.ShowChildWindowInputAsync("Hello!", "What is your name?", mySettings);

            if (string.IsNullOrEmpty(result)) //user pressed cancel
                return;

            await this.ShowChildWindowMessageAsync("Hello", "Hello " + result + "!", MessageDialogStyle.Affirmative, mySettings);
        }
    }
}
