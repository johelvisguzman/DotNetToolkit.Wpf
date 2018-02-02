namespace DotNetToolkit.Wpf.ComponentModel
{
    using System;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// A base class for objects of which the properties can be invalidated.
    /// </summary>
    public abstract class ValidatableObject : ObservableObject, INotifyDataErrorInfo
    {
        private readonly ConcurrentDictionary<string, List<string>> _errors = new ConcurrentDictionary<string, List<string>>();

        /// <summary>
        /// Occurs when the validation errors have changed for a property or for the entire entity.
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        /// <summary>
        /// Gets a value that indicates whether the entity has validation errors.
        /// </summary>
        public bool HasErrors
        {
            get { return _errors.Any(x => x.Value != null && x.Value.Count > 0); }
        }

        /// <summary>
        /// Raises the errors changed event.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        protected void RaiseErrorsChanged([CallerMemberName] string propertyName = null)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Sets the validation error for the specified property.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="errorMessage">The error message.</param>
        protected void SetError(string propertyName, string errorMessage)
        {
            if (!_errors.ContainsKey(propertyName))
            {
                _errors.TryAdd(propertyName, new List<string>
                {
                    errorMessage
                });
            }

            RaiseErrorsChanged(propertyName);
        }

        /// <summary>
        /// Sets the validation error for the property whose name matches <paramref name="propertyExpression" />.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="propertyExpression">An expression identifying the property to set the validation error for.</param>
        /// <param name="errorMessage">The error message.</param>
        protected void SetError<T>(Expression<Func<T>> propertyExpression, string errorMessage)
        {
            SetError(GetPropertyName(propertyExpression), errorMessage);
        }

        /// <summary>
        /// Gets the validation errors for a specified property or for the entire entity.
        /// </summary>
        /// <param name="propertyName">The name of the property to retrieve validation errors for; or null or <see cref="F:System.String.Empty" />, to retrieve entity-level errors.</param>
        /// <returns>
        /// The validation errors for the property or entity.
        /// </returns>
        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName) || !_errors.ContainsKey(propertyName))
            {
                return null;
            }

            return _errors[propertyName];
        }

        /// <summary>
        /// Gets the validation errors for the property whose name matches <paramref name="propertyExpression" /> or for the entire entity.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="propertyExpression">An expression identifying the property to get the validation error for.</param>
        /// <returns>
        /// The validation errors for the property or entity.
        /// </returns>
        public IEnumerable GetErrors<T>(Expression<Func<T>> propertyExpression)
        {
            return GetErrors(GetPropertyName(propertyExpression));
        }

        /// <summary>
        /// Clears the validation error for the specified property.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        protected void ClearError(string propertyName)
        {
            if (_errors.ContainsKey(propertyName))
            {
                _errors.TryRemove(propertyName, out List < string > errors);
            }

            RaiseErrorsChanged(propertyName);
        }

        /// <summary>
        /// Clears the validation error for the property whose name matches <paramref name="propertyExpression" />.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="propertyExpression">An expression identifying the property to clear the validation error for.</param>
        protected void ClearError<T>(Expression<Func<T>> propertyExpression)
        {
            ClearError(GetPropertyName(propertyExpression));
        }

        /// <summary>
        /// Clears all the validation errors.
        /// </summary>
        protected void ClearAllErrors()
        {
            _errors.Select(error => error.Key).ToList().ForEach(ClearError);
        }
    }
}