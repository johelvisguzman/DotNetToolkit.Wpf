namespace DotNetToolkit.Wpf.ComponentModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// A base class for objects of which the properties must be observable.
    /// </summary>
    public class ObservableObject : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when the value of a property has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets the name for the property whose name matches <paramref name="propertyExpression" />.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="propertyExpression">An expression identifying the property that changed</param>
        /// <returns>The name of the property.</returns>
        protected static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            var memberExpr = propertyExpression.Body as MemberExpression;
            if (memberExpr == null)
                return null;

            return memberExpr.Member.Name;
        }

        /// <summary>
        /// Raises <see cref="PropertyChanged" /> for the property whose name matches <paramref name="propertyName" />.
        /// </summary>
        /// <param name="propertyName">The name of the property whose value has changed.</param>
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Raises <see cref="PropertyChanged" /> for the property whose name matches <paramref name="propertyExpression" />.
        /// </summary>
        /// <typeparam name="T">The type of the property that changed.</typeparam>
        /// <param name="propertyExpression">An expression identifying the property that changed.</param>
        protected void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            RaisePropertyChanged(GetPropertyName(propertyExpression));
        }

        /// <summary>
        /// Sets a new value for the specified property.
        /// </summary>
        /// <typeparam name="T">The type of the property that changed.</typeparam>
        /// <param name="field">The field storing the property's value.</param>
        /// <param name="newValue">The property's value after the change occurred.</param>
        /// <param name="propertyName">The name of the property that changed.</param>
        /// <returns>
        /// <c>true</c> if the value of the property has been successful assigned; otherwise, <c>false</c>.
        /// </returns>
        protected bool Set<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
                return false;

            field = newValue;
            RaisePropertyChanged(propertyName);

            return true;
        }

        /// <summary>
        /// Sets a new value for the specified property.
        /// </summary>
        /// <typeparam name="T">The type of the property that changed.</typeparam>
        /// <param name="propertyExpression">An expression identifying the property that changed.</param>
        /// <param name="field">The field storing the property's value.</param>
        /// <param name="newValue">The property's value after the change occurred.</param>
        /// <returns>
        /// <c>true</c> if the value of the property has been successful assigned; otherwise, <c>false</c>.
        /// </returns>
        protected bool Set<T>(Expression<Func<T>> propertyExpression, ref T field, T newValue)
        {
            return Set(ref field, newValue, GetPropertyName(propertyExpression));
        }
    }
}
