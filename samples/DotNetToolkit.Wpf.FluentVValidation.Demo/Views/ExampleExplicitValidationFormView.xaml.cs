namespace DotNetToolkit.Wpf.FluentVValidation.Demo.Views
{
    using ViewModels;

    /// <summary>
    /// Interaction logic for ExampleExplicitValidationFormView.xaml
    /// </summary>
    public partial class ExampleExplicitValidationFormView
    {
        public ExampleExplicitValidationFormView()
        {
            InitializeComponent();

            DataContext = new ExampleExplicitValidationFormViewModel();
        }
    }
}
