namespace DotNetToolkit.Wpf.FluentVValidation.Demo.Infrastructure
{
    using FluentValidation;
    using Mvvm;
    using System;
    using System.Threading.Tasks;

    public abstract class FluentExplicitValidatableViewModelBase<TValidator> : ViewModelBase where TValidator : IValidator
    {
        private readonly IValidator _validator;

        protected FluentExplicitValidatableViewModelBase()
        {
            _validator = Activator.CreateInstance<TValidator>();
        }

        public async Task<bool> ValidateAsync()
        {
            ClearErrors();

            var result = await _validator.ValidateAsync(this);
            if (result == null)
                return false;

            foreach (var error in result.Errors)
            {
                SetError(error.PropertyName, error.ErrorMessage);
            }

            return result.IsValid;
        }
    }
}
