namespace DotNetToolkit.Wpf.Mvvm
{
    using System.ComponentModel;

    /// <summary>
    /// A base interface for all view models in the MVVM pattern.
    /// </summary>
    public interface IViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is initialized.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is initialized; otherwise, <c>false</c>.
        /// </value>
        bool IsInitialized { get; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        string DisplayName { get; set; }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        void Initialize();
    }
}
