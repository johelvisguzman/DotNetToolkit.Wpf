namespace DotNetToolkit.Wpf.Commands
{
    using System.Threading.Tasks;
    using System.Windows.Input;

    /// <summary>
    /// Defines an asynchronous command.
    /// </summary>
    public interface IAsyncCommand : ICommand
    {
        /// <summary>
        /// Asynchronously defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to null.</param>
        Task ExecuteAsync(object parameter);
    }
}
