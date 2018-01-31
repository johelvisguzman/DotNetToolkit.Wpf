namespace DotNetToolkit.Wpf.Mvvm
{
    using Common;

    /// <summary>
    /// A base class for the ViewModel classes in the MVVM pattern.
    /// </summary>
    public abstract class ViewModelBase : ObservableObject
    {
        #region Fields

        private string _displayName;
        private bool _isInitialized;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelBase" /> class.
        /// </summary>
        protected ViewModelBase()
        {
            DisplayName = GetType().FullName;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public string DisplayName
        {
            get { return _displayName; }
            set { Set(ref _displayName, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is initialized.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is initialized; otherwise, <c>false</c>.
        /// </value>
        public bool IsInitialized
        {
            get { return _isInitialized; }
            protected set { Set(ref _isInitialized, value); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize()
        {
            if (!IsInitialized)
            {
                IsInitialized = true;
                OnInitialize();
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// A protected overridable method for initializing.
        /// </summary>
        protected virtual void OnInitialize() { }

        #endregion
    }
}
