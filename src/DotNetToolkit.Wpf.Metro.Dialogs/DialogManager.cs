namespace DotNetToolkit.Wpf.Metro.Dialogs
{
    using MahApps.Metro.SimpleChildWindow;
    using System;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;

    public static class DialogManager
    {
        /// <summary>
        /// Creates a child window MessageDialog inside of the current window dialog container.
        /// </summary>
        /// <param name="window">The window.</param>
        /// <param name="title">The title of the MessageDialog.</param>
        /// <param name="message">The message contained within the MessageDialog.</param>
        /// <param name="style">The type of buttons to use.</param>
        /// <param name="settings">Optional Settings that override the global metro dialog settings.</param>
        /// <param name="overlayFillBehavior">The overlay fill behavior.</param>
        /// <returns>A task promising the result of which button was pressed.</returns>
        public static Task<MessageDialogResult> ShowChildWindowMessageAsync(this Window window, string title, string message, MessageDialogStyle style = MessageDialogStyle.Affirmative, ChildWindowDialogSettings settings = null, ChildWindowManager.OverlayFillBehavior overlayFillBehavior = ChildWindowManager.OverlayFillBehavior.WindowContent)
        {
            var dialog = new MessageDialog(settings)
            {
                Title = title,
                Message = message,
                ButtonStyle = style
            };

            window.ShowChildWindowAsync<Task<MessageDialogResult>>(dialog, overlayFillBehavior);

            return dialog.WaitForButtonPressAsync().ContinueWith(y =>
            {
                dialog.Dispatcher.Invoke(() => dialog.Close(y));

                return y;
            }).Unwrap();
        }

        /// <summary>
        /// Creates a child window MessageDialog on the given container.
        /// </summary>
        /// <param name="window">The window.</param>
        /// <param name="title">The title of the MessageDialog.</param>
        /// <param name="message">The message contained within the MessageDialog.</param>
        /// <param name="container">The container.</param>
        /// <param name="style">The type of buttons to use.</param>
        /// <param name="settings">Optional Settings that override the global metro dialog settings.</param>
        /// <returns>A task promising the result of which button was pressed.</returns>
        public static Task<MessageDialogResult> ShowChildWindowMessageAsync(this Window window, string title, string message, Panel container, MessageDialogStyle style = MessageDialogStyle.Affirmative, ChildWindowDialogSettings settings = null)
        {
            var dialog = new MessageDialog(settings)
            {
                Title = title,
                Message = message,
                ButtonStyle = style
            };

            window.ShowChildWindowAsync<Task<MessageDialogResult>>(dialog, container);

            return dialog.WaitForButtonPressAsync().ContinueWith(y =>
            {
                dialog.Dispatcher.Invoke(() => dialog.Close(y));

                return y;
            }).Unwrap();
        }

        /// <summary>
        /// Creates a child window InputDialog inside of the current window dialog container.
        /// </summary>
        /// <param name="window">The window.</param>
        /// <param name="title">The title of the MessageDialog.</param>
        /// <param name="message">The message contained within the MessageDialog.</param>
        /// <param name="settings">Optional Settings that override the global metro dialog settings.</param>
        /// <param name="overlayFillBehavior">The overlay fill behavior.</param>
        /// <returns>The text that was entered or null (Nothing in Visual Basic) if the user cancelled the operation.</returns>
        public static Task<string> ShowChildWindowInputAsync(this Window window, string title, string message, ChildWindowDialogSettings settings = null, ChildWindowManager.OverlayFillBehavior overlayFillBehavior = ChildWindowManager.OverlayFillBehavior.WindowContent)
        {
            var dialog = new InputDialog(settings)
            {
                Title = title,
                Message = message,
                Input = settings?.DefaultText
            };

            window.ShowChildWindowAsync<string>(dialog, overlayFillBehavior);

            return dialog.WaitForButtonPressAsync().ContinueWith(y =>
            {
                dialog.Dispatcher.Invoke(() => dialog.Close(y));

                return y;
            }).Unwrap();
        }

        /// <summary>
        /// Creates a child window InputDialog on the given container.
        /// </summary>
        /// <param name="window">The window.</param>
        /// <param name="title">The title of the MessageDialog.</param>
        /// <param name="message">The message contained within the MessageDialog.</param>
        /// <param name="container">The container.</param>
        /// <param name="settings">Optional Settings that override the global metro dialog settings.</param>
        /// <returns>The text that was entered or null (Nothing in Visual Basic) if the user cancelled the operation.</returns>
        public static Task<string> ShowChildWindowInputAsync(this Window window, string title, string message, Panel container, ChildWindowDialogSettings settings = null)
        {
            var dialog = new InputDialog(settings)
            {
                Title = title,
                Message = message,
                Input = settings?.DefaultText
            };

            window.ShowChildWindowAsync<string>(dialog, container);

            return dialog.WaitForButtonPressAsync().ContinueWith(y =>
            {
                dialog.Dispatcher.Invoke(() => dialog.Close(y));

                return y;
            }).Unwrap();
        }

        /// <summary>
        /// Creates a child window ProgressDialog inside of the current window dialog container.
        /// </summary>
        /// <param name="window">The MetroWindow</param>
        /// <param name="title">The title of the ProgressDialog.</param>
        /// <param name="message">The message within the ProgressDialog.</param>
        /// <param name="isCancelable">Determines if the cancel button is visible.</param>
        /// <param name="settings">Optional Settings that override the global metro dialog settings.</param>
        /// <param name="overlayFillBehavior">The overlay fill behavior.</param>
        /// <returns>A task promising the instance of ProgressDialogController for this operation.</returns>
        public static Task<ProgressDialogController> ShowChildWindowProgressAsync(this Window window, string title, string message, bool isCancelable = false, ChildWindowDialogSettings settings = null, ChildWindowManager.OverlayFillBehavior overlayFillBehavior = ChildWindowManager.OverlayFillBehavior.WindowContent)
        {
            var dialog = new ProgressDialog(settings)
            {
                Title = title,
                Message = message,
                IsCancelable = isCancelable
            };

            window.ShowChildWindowAsync(dialog, overlayFillBehavior);

            return dialog.WaitForIsOpenChangedAsync().ContinueWith(_ => new ProgressDialogController(dialog));
        }

        /// <summary>
        /// Creates a child window ProgressDialog on the given container.
        /// </summary>
        /// <param name="window">The MetroWindow</param>
        /// <param name="title">The title of the ProgressDialog.</param>
        /// <param name="message">The message within the ProgressDialog.</param>
        /// <param name="isCancelable">Determines if the cancel button is visible.</param>
        /// <param name="container">The container.</param>
        /// <param name="settings">Optional Settings that override the global metro dialog settings.</param>
        /// <returns>A task promising the instance of ProgressDialogController for this operation.</returns>
        public static Task<ProgressDialogController> ShowChildWindowProgressAsync(this Window window, string title, string message, bool isCancelable, Panel container, ChildWindowDialogSettings settings = null)
        {
            var dialog = new ProgressDialog(settings)
            {
                Title = title,
                Message = message,
                IsCancelable = isCancelable
            };

            window.ShowChildWindowAsync(dialog, container);

            return dialog.WaitForIsOpenChangedAsync().ContinueWith(_ => new ProgressDialogController(dialog));
        }

        /// <summary>
        /// Creates a child window CustomDialog inside of the current window dialog container.
        /// </summary>
        /// <param name="window">The window.</param>
        /// <param name="title">The title of the CustomDialog.</param>
        /// <param name="content">The data contained within the CustomDialog.</param>
        /// <param name="buttonPressCallbackAsync">A callback function to be executed when a button is pressed asynchronous.</param>
        /// <param name="style">The type of buttons to use.</param>
        /// <param name="settings">Optional Settings that override the global metro dialog settings.</param>
        /// <param name="overlayFillBehavior">The overlay fill behavior.</param>
        /// <returns>A task promising the result of which button was pressed.</returns>
        public static Task<MessageDialogResult> ShowChildWindowCustomAsync(this Window window, string title, object content, Func<MessageDialogResult, Task<bool>> buttonPressCallbackAsync = null, MessageDialogStyle style = MessageDialogStyle.Affirmative, ChildWindowDialogSettings settings = null, ChildWindowManager.OverlayFillBehavior overlayFillBehavior = ChildWindowManager.OverlayFillBehavior.WindowContent)
        {
            var dialog = new CustomDialog(settings, buttonPressCallbackAsync)
            {
                Title = title,
                DialogContent = content,
                ButtonStyle = style
            };

            window.ShowChildWindowAsync<Task<MessageDialogResult>>(dialog, overlayFillBehavior);

            return dialog.WaitForButtonPressAsync().ContinueWith(y =>
            {
                dialog.Dispatcher.Invoke(() => dialog.Close(y));

                return y;
            }).Unwrap();
        }

        /// <summary>
        /// Creates a child window CustomDialog on the given container.
        /// </summary>
        /// <param name="window">The window.</param>
        /// <param name="title">The title of the CustomDialog.</param>
        /// <param name="content">The data contained within the CustomDialog.</param>
        /// <param name="container">The container.</param>
        /// <param name="buttonPressCallbackAsync">A callback function to be executed when a button is pressed asynchronous.</param>
        /// <param name="style">The type of buttons to use.</param>
        /// <param name="settings">Optional Settings that override the global metro dialog settings.</param>
        /// <returns>A task promising the result of which button was pressed.</returns>
        public static Task<MessageDialogResult> ShowChildWindowCustomAsync(this Window window, string title, object content, Panel container, Func<MessageDialogResult, Task<bool>> buttonPressCallbackAsync = null, MessageDialogStyle style = MessageDialogStyle.Affirmative, ChildWindowDialogSettings settings = null)
        {
            var dialog = new CustomDialog(settings, buttonPressCallbackAsync)
            {
                Title = title,
                DialogContent = content,
                ButtonStyle = style
            };

            window.ShowChildWindowAsync<Task<MessageDialogResult>>(dialog, container);

            return dialog.WaitForButtonPressAsync().ContinueWith(y =>
            {
                dialog.Dispatcher.Invoke(() => dialog.Close(y));

                return y;
            }).Unwrap();
        }
    }
}
