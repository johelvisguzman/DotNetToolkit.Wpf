namespace DotNetToolkit.Wpf.FluentVValidation.Demo.Infrastructure
{
    using FluentValidation;
    using FluentValidation.Internal;
    using FluentValidation.Results;
    using Mvvm;
    using System;
    using System.Runtime.CompilerServices;

    public abstract class FluentImplicitValidatableViewModelBase<TValidator> : ViewModelBase where TValidator : IValidator
    {
        private readonly IValidator _validator;

        protected FluentImplicitValidatableViewModelBase()
        {
            _validator = Activator.CreateInstance<TValidator>();
        }

        private ValidationResult Validate(string propertyName)
        {
            if (_validator == null)
                return null;

            var selector = ValidatorOptions.ValidatorSelectors.MemberNameValidatorSelectorFactory(new[] { propertyName });
            var context = new ValidationContext(this, new PropertyChain(), selector);
            var result = _validator.Validate(context);

            return result;
        }

        protected override bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            ClearErrors(propertyName);

            var isSet = base.SetProperty(ref field, newValue, propertyName);
            var result = Validate(propertyName);

            if (result != null)
            {
                foreach (var error in result.Errors)
                {
                    SetError(error.PropertyName, error.ErrorMessage);
                }
            }

            return isSet;
        }
    }
}
