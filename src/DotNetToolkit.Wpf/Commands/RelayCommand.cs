namespace DotNetToolkit.Wpf.Commands
{
    using System;
    using System.Diagnostics;
    using System.Windows.Input;

    /// <summary>
    /// A command whose sole purpose is to relay its functionality to other objects by invoking delegates. The default return value for the CanExecute method is 'true'.
    /// This class does not allow you to accept command parameters in the Execute and CanExecute callback methods.
    /// </summary>
    // http://msdn.microsoft.com/en-us/magazine/dd419663.aspx#id0090030
    public class RelayCommand : ICommand
    {
        #region Fields

        private readonly Action _execute;
        private readonly Predicate<object> _canExecute;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <exception cref="ArgumentNullException">execute</exception>
        public RelayCommand(Action execute, Predicate<object> canExecute = null)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));

            _execute = execute;
            _canExecute = canExecute;
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
            return _canExecute == null ? true : _canExecute(parameter);
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public void Execute(object parameter)
        {
            if (CanExecute(parameter) && _execute != null)
            {
                _execute();
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
