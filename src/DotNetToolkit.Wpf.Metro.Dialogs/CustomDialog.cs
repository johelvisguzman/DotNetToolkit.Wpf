namespace DotNetToolkit.Wpf.Metro.Dialogs
{
    using ControlzEx;
    using System;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    /// A custom dialog.
    /// </summary>
    public partial class CustomDialog : BaseChildWindowDialog
    {
        internal CustomDialog()
            : this(null)
        {
        }

        internal CustomDialog(ChildWindowDialogSettings settings)
            : base(settings)
        {
            InitializeComponent();
        }

        internal Task<MessageDialogResult> WaitForButtonPressAsync(Func<MessageDialogResult, bool> buttonPressCallback = null)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                Focus();

                var defaultButtonFocus = DialogSettings.DefaultButtonFocus;

                //Ensure it's a valid option
                if (!IsApplicable(defaultButtonFocus))
                {
                    defaultButtonFocus = ButtonStyle == MessageDialogStyle.Affirmative
                        ? MessageDialogResult.Affirmative
                        : MessageDialogResult.Negative;
                }

                //kind of acts like a selective 'IsDefault' mechanism.
                switch (defaultButtonFocus)
                {
                    case MessageDialogResult.Affirmative:
                        PART_AffirmativeButton.SetResourceReference(StyleProperty, "AccentedDialogSquareButton");
                        KeyboardNavigationEx.Focus(PART_AffirmativeButton);
                        break;
                    case MessageDialogResult.Negative:
                        PART_NegativeButton.SetResourceReference(StyleProperty, "AccentedDialogSquareButton");
                        KeyboardNavigationEx.Focus(PART_NegativeButton);
                        break;
                    case MessageDialogResult.FirstAuxiliary:
                        PART_FirstAuxiliaryButton.SetResourceReference(StyleProperty, "AccentedDialogSquareButton");
                        KeyboardNavigationEx.Focus(PART_FirstAuxiliaryButton);
                        break;
                    case MessageDialogResult.SecondAuxiliary:
                        PART_SecondAuxiliaryButton.SetResourceReference(StyleProperty, "AccentedDialogSquareButton");
                        KeyboardNavigationEx.Focus(PART_SecondAuxiliaryButton);
                        break;
                }
            }));

            TaskCompletionSource<MessageDialogResult> tcs = new TaskCompletionSource<MessageDialogResult>();

            RoutedEventHandler negativeHandler = null;
            KeyEventHandler negativeKeyHandler = null;

            RoutedEventHandler affirmativeHandler = null;
            KeyEventHandler affirmativeKeyHandler = null;

            RoutedEventHandler firstAuxHandler = null;
            KeyEventHandler firstAuxKeyHandler = null;

            RoutedEventHandler secondAuxHandler = null;
            KeyEventHandler secondAuxKeyHandler = null;

            KeyEventHandler escapeKeyHandler = null;

            Action cleanUpHandlers = null;

            if (buttonPressCallback == null)
            {
                buttonPressCallback = result => true;
            }

            var cancellationTokenRegistration = DialogSettings.CancellationToken.Register(() =>
            {
                var result = ButtonStyle == MessageDialogStyle.Affirmative
                    ? MessageDialogResult.Affirmative
                    : MessageDialogResult.Negative;

                if (buttonPressCallback(result))
                {
                    cleanUpHandlers?.Invoke();
                    tcs.TrySetResult(result);
                }
            });

            cleanUpHandlers = () =>
            {
                PART_NegativeButton.Click -= negativeHandler;
                PART_AffirmativeButton.Click -= affirmativeHandler;
                PART_FirstAuxiliaryButton.Click -= firstAuxHandler;
                PART_SecondAuxiliaryButton.Click -= secondAuxHandler;

                PART_NegativeButton.KeyDown -= negativeKeyHandler;
                PART_AffirmativeButton.KeyDown -= affirmativeKeyHandler;
                PART_FirstAuxiliaryButton.KeyDown -= firstAuxKeyHandler;
                PART_SecondAuxiliaryButton.KeyDown -= secondAuxKeyHandler;

                KeyDown -= escapeKeyHandler;

                cancellationTokenRegistration.Dispose();
            };

            negativeKeyHandler = (sender, e) =>
            {
                if (e.Key == Key.Enter)
                {
                    if (buttonPressCallback(MessageDialogResult.Negative))
                    {
                        cleanUpHandlers();

                        tcs.TrySetResult(MessageDialogResult.Negative);
                    }
                }
            };

            affirmativeKeyHandler = (sender, e) =>
            {
                if (e.Key == Key.Enter)
                {
                    if (buttonPressCallback(MessageDialogResult.Affirmative))
                    {
                        cleanUpHandlers();

                        tcs.TrySetResult(MessageDialogResult.Affirmative);
                    }
                }
            };

            firstAuxKeyHandler = (sender, e) =>
            {
                if (e.Key == Key.Enter)
                {
                    if (buttonPressCallback(MessageDialogResult.FirstAuxiliary))
                    {
                        cleanUpHandlers();

                        tcs.TrySetResult(MessageDialogResult.FirstAuxiliary);
                    }
                }
            };

            secondAuxKeyHandler = (sender, e) =>
            {
                if (e.Key == Key.Enter)
                {
                    if (buttonPressCallback(MessageDialogResult.SecondAuxiliary))
                    {
                        cleanUpHandlers();

                        tcs.TrySetResult(MessageDialogResult.SecondAuxiliary);
                    }
                }
            };

            negativeHandler = (sender, e) =>
            {
                if (buttonPressCallback(MessageDialogResult.Negative))
                {
                    cleanUpHandlers();

                    tcs.TrySetResult(MessageDialogResult.Negative);

                    e.Handled = true;
                }
            };

            affirmativeHandler = (sender, e) =>
            {
                if (buttonPressCallback(MessageDialogResult.Affirmative))
                {
                    cleanUpHandlers();

                    tcs.TrySetResult(MessageDialogResult.Affirmative);

                    e.Handled = true;
                }
            };

            firstAuxHandler = (sender, e) =>
            {
                if (buttonPressCallback(MessageDialogResult.FirstAuxiliary))
                {
                    cleanUpHandlers();

                    tcs.TrySetResult(MessageDialogResult.FirstAuxiliary);

                    e.Handled = true;
                }
            };

            secondAuxHandler = (sender, e) =>
            {
                if (buttonPressCallback(MessageDialogResult.SecondAuxiliary))
                {
                    cleanUpHandlers();

                    tcs.TrySetResult(MessageDialogResult.SecondAuxiliary);

                    e.Handled = true;
                }
            };

            escapeKeyHandler = (sender, e) =>
            {
                if (e.Key == Key.Escape)
                {
                    var result = ButtonStyle == MessageDialogStyle.Affirmative 
                        ? MessageDialogResult.Affirmative 
                        : MessageDialogResult.Negative;

                    if (buttonPressCallback(result))
                    {
                        cleanUpHandlers();

                        tcs.TrySetResult(result);
                    }
                }
                else if (e.Key == Key.Enter)
                {
                    if (buttonPressCallback(MessageDialogResult.Affirmative))
                    {
                        cleanUpHandlers();

                        tcs.TrySetResult(MessageDialogResult.Affirmative);
                    }
                }
            };

            PART_NegativeButton.KeyDown += negativeKeyHandler;
            PART_AffirmativeButton.KeyDown += affirmativeKeyHandler;
            PART_FirstAuxiliaryButton.KeyDown += firstAuxKeyHandler;
            PART_SecondAuxiliaryButton.KeyDown += secondAuxKeyHandler;

            PART_NegativeButton.Click += negativeHandler;
            PART_AffirmativeButton.Click += affirmativeHandler;
            PART_FirstAuxiliaryButton.Click += firstAuxHandler;
            PART_SecondAuxiliaryButton.Click += secondAuxHandler;

            KeyDown += escapeKeyHandler;

            return tcs.Task;
        }

        public static readonly DependencyProperty DialogContentProperty = DependencyProperty.Register("DialogContent", typeof(object), typeof(CustomDialog), new PropertyMetadata(default(object)));
        public static readonly DependencyProperty AffirmativeButtonTextProperty = DependencyProperty.Register("AffirmativeButtonText", typeof(string), typeof(CustomDialog), new PropertyMetadata("OK"));
        public static readonly DependencyProperty NegativeButtonTextProperty = DependencyProperty.Register("NegativeButtonText", typeof(string), typeof(CustomDialog), new PropertyMetadata("Cancel"));
        public static readonly DependencyProperty FirstAuxiliaryButtonTextProperty = DependencyProperty.Register("FirstAuxiliaryButtonText", typeof(string), typeof(CustomDialog), new PropertyMetadata("Cancel"));
        public static readonly DependencyProperty SecondAuxiliaryButtonTextProperty = DependencyProperty.Register("SecondAuxiliaryButtonText", typeof(string), typeof(CustomDialog), new PropertyMetadata("Cancel"));
        public static readonly DependencyProperty ButtonStyleProperty = DependencyProperty.Register("ButtonStyle", typeof(MessageDialogStyle), typeof(CustomDialog), new PropertyMetadata(MessageDialogStyle.Affirmative, new PropertyChangedCallback((s, e) =>
        {
            CustomDialog md = (CustomDialog)s;

            SetButtonState(md);
        })));

        private static void SetButtonState(CustomDialog md)
        {
            if (md.PART_AffirmativeButton == null)
                return;

            switch (md.ButtonStyle)
            {
                case MessageDialogStyle.Affirmative:
                    {
                        md.PART_AffirmativeButton.Visibility = Visibility.Visible;
                        md.PART_NegativeButton.Visibility = Visibility.Collapsed;
                        md.PART_FirstAuxiliaryButton.Visibility = Visibility.Collapsed;
                        md.PART_SecondAuxiliaryButton.Visibility = Visibility.Collapsed;
                    }
                    break;
                case MessageDialogStyle.AffirmativeAndNegativeAndSingleAuxiliary:
                case MessageDialogStyle.AffirmativeAndNegativeAndDoubleAuxiliary:
                case MessageDialogStyle.AffirmativeAndNegative:
                    {
                        md.PART_AffirmativeButton.Visibility = Visibility.Visible;
                        md.PART_NegativeButton.Visibility = Visibility.Visible;

                        if (md.ButtonStyle == MessageDialogStyle.AffirmativeAndNegativeAndSingleAuxiliary || md.ButtonStyle == MessageDialogStyle.AffirmativeAndNegativeAndDoubleAuxiliary)
                        {
                            md.PART_FirstAuxiliaryButton.Visibility = Visibility.Visible;
                        }

                        if (md.ButtonStyle == MessageDialogStyle.AffirmativeAndNegativeAndDoubleAuxiliary)
                        {
                            md.PART_SecondAuxiliaryButton.Visibility = Visibility.Visible;
                        }
                    }
                    break;
            }

            md.AffirmativeButtonText = md.DialogSettings.AffirmativeButtonText;
            md.NegativeButtonText = md.DialogSettings.NegativeButtonText;
            md.FirstAuxiliaryButtonText = md.DialogSettings.FirstAuxiliaryButtonText;
            md.SecondAuxiliaryButtonText = md.DialogSettings.SecondAuxiliaryButtonText;
        }

        protected override void OnLoaded()
        {
            SetButtonState(this);
        }

        public object DialogContent
        {
            get { return (object)GetValue(DialogContentProperty); }
            set { SetValue(DialogContentProperty, value); }
        }

        public MessageDialogStyle ButtonStyle
        {
            get { return (MessageDialogStyle)GetValue(ButtonStyleProperty); }
            set { SetValue(ButtonStyleProperty, value); }
        }

        public string AffirmativeButtonText
        {
            get { return (string)GetValue(AffirmativeButtonTextProperty); }
            set { SetValue(AffirmativeButtonTextProperty, value); }
        }

        public string NegativeButtonText
        {
            get { return (string)GetValue(NegativeButtonTextProperty); }
            set { SetValue(NegativeButtonTextProperty, value); }
        }

        public string FirstAuxiliaryButtonText
        {
            get { return (string)GetValue(FirstAuxiliaryButtonTextProperty); }
            set { SetValue(FirstAuxiliaryButtonTextProperty, value); }
        }

        public string SecondAuxiliaryButtonText
        {
            get { return (string)GetValue(SecondAuxiliaryButtonTextProperty); }
            set { SetValue(SecondAuxiliaryButtonTextProperty, value); }
        }

        private bool IsApplicable(MessageDialogResult value)
        {
            switch (value)
            {
                case MessageDialogResult.Affirmative:
                    return PART_AffirmativeButton.IsVisible;
                case MessageDialogResult.Negative:
                    return PART_NegativeButton.IsVisible;
                case MessageDialogResult.FirstAuxiliary:
                    return PART_FirstAuxiliaryButton.IsVisible;
                case MessageDialogResult.SecondAuxiliary:
                    return PART_SecondAuxiliaryButton.IsVisible;
            }

            return false;
        }
    }
}
