namespace DotNetToolkit.Wpf.Metro.Demo.Views
{
    using Dialogs;
    using MahApps.Metro.Controls;
    using System;
    using System.Threading.Tasks;
    using System.Windows;
    using ViewModels;

    /// <summary>
    /// Interaction logic for DialogsExampleView.xaml
    /// </summary>
    public partial class DialogsExampleView
    {
        private readonly MetroWindow _mainMetroWindow;

        public DialogsExampleView()
        {
            InitializeComponent();

            _mainMetroWindow = Application.Current.MainWindow as MetroWindow;
        }

        private async void ProgressTest_OnClick(object sender, RoutedEventArgs e)
        {
            var mySettings = new ChildWindowDialogSettings
            {
                AllowMove = true,
                NegativeButtonText = "Close now"
            };

            var controller = await _mainMetroWindow.ShowChildWindowProgressAsync("Please wait...", "We are baking some cupcakes!", true, mySettings);

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
                await _mainMetroWindow.ShowChildWindowMessageAsync("No cupcakes!", "You stopped baking!", MessageDialogStyle.Affirmative, mySettings);
            }
            else
            {
                await _mainMetroWindow.ShowChildWindowMessageAsync("Cupcakes!", "Your cupcakes are finished! Enjoy!", MessageDialogStyle.Affirmative, mySettings);
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

            await _mainMetroWindow.ShowChildWindowMessageAsync("Hello!", "Welcome to the world of metro!", MessageDialogStyle.AffirmativeAndNegativeAndSingleAuxiliary, mySettings);
        }

        private async void InputTest_OnClick(object sender, RoutedEventArgs e)
        {
            var mySettings = new ChildWindowDialogSettings
            {
                AllowMove = true
            };

            var result = await _mainMetroWindow.ShowChildWindowInputAsync("Hello!", "What is your name?", mySettings);

            if (string.IsNullOrEmpty(result)) //user pressed cancel
                return;

            await _mainMetroWindow.ShowChildWindowMessageAsync("Hello", "Hello " + result + "!", MessageDialogStyle.Affirmative, mySettings);
        }

        private async void CustomTest_OnClick(object sender, RoutedEventArgs e)
        {
            var mySettings = new ChildWindowDialogSettings
            {
                AllowMove = true,
                AffirmativeButtonText = "Submit",
                NegativeButtonText = "Close",
                ChildWindowWidth = 400
            };

            var viewModel = new ExampleFormViewModel();
            var view = new ExampleFormView
            {
                DataContext = viewModel
            };

            Func<MessageDialogResult, Task<bool>> callbackAsync = (result) =>
            {
                if (result == MessageDialogResult.Affirmative)
                {
                    if (!viewModel.IsValid)
                        return Task.FromResult(false);
                }

                return Task.FromResult(true);
            };

            await _mainMetroWindow.ShowChildWindowCustomAsync("Enter/Edit Form Details", view, callbackAsync, MessageDialogStyle.AffirmativeAndNegative, mySettings);
        }
    }
}
