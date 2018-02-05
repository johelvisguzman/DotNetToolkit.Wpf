namespace DotNetToolkit.Wpf.FluentVValidation.Demo.ViewModels
{
    using FluentValidation;
    using Infrastructure;

    public class ExampleImplicitValidationFormViewModel : FluentImplicitValidatableViewModelBase<ExampleImplicitValidationFormViewModelValidator>
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { Set(ref _name, value); }
        }

        private int? _age;
        public int? Age
        {
            get { return _age; }
            set { Set(ref _age, value); }
        }

        private string _notes;
        public string Notes
        {
            get { return _notes; }
            set { Set(ref _notes, value); }
        }
    }

    public class ExampleImplicitValidationFormViewModelValidator : AbstractValidator<ExampleImplicitValidationFormViewModel>
    {
        public ExampleImplicitValidationFormViewModelValidator()
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
