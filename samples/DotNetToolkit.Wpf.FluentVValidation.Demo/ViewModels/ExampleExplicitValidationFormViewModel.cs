namespace DotNetToolkit.Wpf.FluentVValidation.Demo.ViewModels
{
    using Commands;
    using FluentValidation;
    using Infrastructure;

    public class ExampleExplicitValidationFormViewModel : FluentExplicitValidatableViewModelBase<ExampleExplicitValidationFormViewModelValidator>
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private int? _age;
        public int? Age
        {
            get { return _age; }
            set { SetProperty(ref _age, value); }
        }

        private string _notes;
        public string Notes
        {
            get { return _notes; }
            set { SetProperty(ref _notes, value); }
        }

        public RelayCommand SubmitCommand { get; private set; }

        public ExampleExplicitValidationFormViewModel()
        {
            SubmitCommand = new RelayCommand(OnSubmit);
        }

        private async void OnSubmit()
        {
            if (await ValidateAsync())
            {
                // Form is valid and ready to be saved
            }
        }
    }

    public class ExampleExplicitValidationFormViewModelValidator : AbstractValidator<ExampleExplicitValidationFormViewModel>
    {
        public ExampleExplicitValidationFormViewModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("The name field is required.");

            RuleFor(x => x.Age)
                .NotEmpty()
                .WithMessage("The age field is required.")
                .GreaterThan(10)
                .WithMessage("The age value must be greater than 10.");
        }
    }
}
