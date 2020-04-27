namespace DotNetToolkit.Wpf.Commands
{
    using Common;
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using System.Windows.Input;

    /// <summary>
    /// An asynchronous command whose sole purpose is to relay its functionality to other objects by invoking delegates. The default return value for the CanExecute method is 'true'.
    /// This class does not allow you to accept command parameters in the Execute and CanExecute callback methods.
    /// </summary>
    public class RelayCommandAsync : IAsyncCommand
    {
        #region Fields

        private bool _isExecuting;
        private readonly Func<Task> _execute;
        private readonly Predicate<object> _canExecute;
        private readonly IErrorHandler _errorHandler;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="errorHandler">The error handler for the asynchronous command.</param>
        /// <exception cref="ArgumentNullException">execute</exception>
        public RelayCommandAsync(Func<Task> execute, Predicate<object> canExecute = null, IErrorHandler errorHandler = null)
        {
            _execute = execute;
            _canExecute = canExecute;
            _errorHandler = errorHandler;
        }

        #endregion

        #region ICommand Members

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        /// <returns>
        /// true if this command can be executed; otherwise, false.
        /// </returns>
        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return !_isExecuting && (_canExecute == null ? true : _canExecute(parameter));
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        void ICommand.Execute(object parameter)
        {
            ExecuteAsync(parameter).FireAndForgetSafeAsync(_errorHandler);
        }

        #endregion

        #region Implementations of IAsyncCommand

        /// <summary>
        /// Asynchronously defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to null.</param>
        public async Task ExecuteAsync(object parameter)
        {
            if (CanExecute(parameter) && _execute != null)
            {
                try
                {
                    _isExecuting = true;
                    await _execute();
                }
                finally
                {
                    _isExecuting = false;
                }
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Raises the <see cref="CanExecuteChanged"/> event to indicate that the return value of the <see cref="CanExecute"/>
        /// method has changed.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}
