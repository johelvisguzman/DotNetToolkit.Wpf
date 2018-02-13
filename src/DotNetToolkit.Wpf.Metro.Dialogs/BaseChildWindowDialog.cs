namespace DotNetToolkit.Wpf.Metro.Dialogs
{
    using MahApps.Metro.SimpleChildWindow;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public abstract class BaseChildWindowDialog : ChildWindow
    {
        public static readonly DependencyProperty DialogMessageFontSizeProperty = DependencyProperty.Register("DialogMessageFontSize", typeof(double), typeof(BaseChildWindowDialog), new PropertyMetadata(15D));

        /// <summary>
        /// Gets or sets the size of the dialog message font.
        /// </summary>
        /// <value>
        /// The size of the dialog message font.
        /// </value>
        public double DialogMessageFontSize
        {
            get { return (double)GetValue(DialogMessageFontSizeProperty); }
            set { SetValue(DialogMessageFontSizeProperty, value); }
        }

        public ChildWindowDialogSettings DialogSettings { get; private set; }

        protected BaseChildWindowDialog(ChildWindowDialogSettings settings)
        {
            DialogSettings = settings ?? new ChildWindowDialogSettings();

            TitleCharacterCasing = DialogSettings.TitleCharacterCasing;
            ShowTitleBar = DialogSettings.ShowTitleBar;
            ShowCloseButton = DialogSettings.ShowCloseButton;
            TitleFontFamily = DialogSettings.TitleFontFamily;
            AllowMove = DialogSettings.AllowMove;
            CloseByEscape = DialogSettings.CloseByEscape;
            CloseOnOverlay = DialogSettings.CloseOnOverlay;
            
            if (DialogSettings.TitleBarBackground != null)
            {
                TitleBarBackground = DialogSettings.TitleBarBackground;
            }

            if (!double.IsNaN(DialogSettings.DialogMessageFontSize))
            {
                DialogMessageFontSize = DialogSettings.DialogMessageFontSize;
            }

            if (!double.IsNaN(DialogSettings.ChildWindowWidth))
            {
                ChildWindowWidth = DialogSettings.ChildWindowWidth;
            }

            if (!double.IsNaN(DialogSettings.ChildWindowHeight))
            {
                ChildWindowHeight = DialogSettings.ChildWindowHeight;
            }

            Initialize();
        }

        protected BaseChildWindowDialog()
        {
            DialogSettings = new ChildWindowDialogSettings();

            Initialize();
        }

        private void Initialize()
        {
            Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri("pack://application:,,,/DotNetToolkit.Wpf.Metro.Dialogs;component/Themes/Generic.xaml") });

            Loaded += (sender, args) => { OnLoaded(); };
        }

        /// <summary>
        /// This is called in the loaded event.
        /// </summary>
        protected virtual void OnLoaded()
        {
            // nothing here
        }

        /// <summary>
        /// Waits for the dialog is open flag to change asynchronous.
        /// </summary>
        public Task WaitForIsOpenChangedAsync()
        {
            var tcs = new TaskCompletionSource<object>();

            RoutedEventHandler handler = null;

            handler = (sender, e) =>
            {
                IsOpenChanged -= handler;

                tcs.TrySetResult(null);
            };

            IsOpenChanged += handler;

            return tcs.Task;
        }
    }

    /// <summary>
    /// A class that represents the settings used by the ChildWindow Dialogs.
    /// </summary>
    public class ChildWindowDialogSettings
    {
        public ChildWindowDialogSettings()
        {
            ShowTitleBar = true;
            TitleCharacterCasing = CharacterCasing.Upper;
            AffirmativeButtonText = "OK";
            NegativeButtonText = "Cancel";
            DefaultText = "";
            DefaultButtonFocus = MessageDialogResult.Negative;
            CancellationToken = CancellationToken.None;
            DialogMessageFontSize = Double.NaN;
            ChildWindowWidth = Double.NaN;
            ChildWindowHeight = Double.NaN;
        }

        /// <summary>
        /// Gets or sets the width of the child window.
        /// </summary>
        public double ChildWindowWidth { get; set; }

        /// <summary>
        /// Gets or sets the height of the child window.
        /// </summary>
        public double ChildWindowHeight { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the child window can be moved inside the overlay container.
        /// </summary>
        public bool AllowMove { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the child window is modal.
        /// </summary>
        public bool IsModal { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the child window can be closed by clicking the overlay container.
        /// </summary>
        public bool CloseOnOverlay { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the child window can be closed by the Escape key.
        /// </summary>
        public bool CloseByEscape { get; set; }

        /// <summary>
        /// Gets or sets whether the title bar is visible or not.
        /// </summary>
        public bool ShowTitleBar { get; set; }

        /// <summary>
        /// Gets or sets the character casing of the title.
        /// </summary>
        public CharacterCasing TitleCharacterCasing { get; set; }

        /// <summary> 
        /// The FontFamily property specifies the font family of the title.
        /// </summary>
        public FontFamily TitleFontFamily { get; set; }

        /// <summary>
        /// Gets or sets the title background.
        /// </summary>
        public Brush TitleBarBackground { get; set; }

        /// <summary>
        /// Gets or sets if the close button is visible.
        /// </summary>
        public bool ShowCloseButton { get; set; }

        /// <summary>
        /// Gets or sets which button should be focused by default
        /// </summary>
        public MessageDialogResult DefaultButtonFocus { get; set; }

        /// <summary>
        /// Gets/sets the text used for the Affirmative button. For example: "OK" or "Yes".
        /// </summary>
        public string AffirmativeButtonText { get; set; }

        /// <summary>
        /// Gets/sets the text used for the Negative button. For example: "Cancel" or "No".
        /// </summary>
        public string NegativeButtonText { get; set; }

        public string FirstAuxiliaryButtonText { get; set; }

        public string SecondAuxiliaryButtonText { get; set; }

        /// <summary>
        /// Gets/sets the default text( just the inputdialog needed)
        /// </summary>
        public string DefaultText { get; set; }

        /// <summary>
        /// Gets/sets the token to cancel the dialog.
        /// </summary>
        public CancellationToken CancellationToken { get; set; }

        /// <summary>
        /// Gets or sets the size of the dialog message font.
        /// </summary>
        /// <value>
        /// The size of the dialog message font.
        /// </value>
        public double DialogMessageFontSize { get; set; }
    }
}
