namespace DotNetToolkit.Wpf
{
    using System;

    public interface IErrorHandler
    {
        void HandleError(Exception ex);
    }
}
