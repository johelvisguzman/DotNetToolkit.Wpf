namespace DotNetToolkit.Wpf.Metro.Dialogs
{
    using System.Threading;
    using System.Windows;
    using System.Windows.Media;

    /// <summary>
	/// An internal control that represents a message dialog. Please use MetroWindow.ShowMessage instead!
	/// </summary>
	public partial class ProgressDialog : BaseChildWindowDialog
    {
        internal ProgressDialog()
            : this(null)
        {
        }

        internal ProgressDialog(ChildWindowDialogSettings settings)
            : base(settings)
        {
            InitializeComponent();
        }

        protected override void OnLoaded()
        {
            NegativeButtonText = DialogSettings.NegativeButtonText;
            SetResourceReference(ProgressBarForegroundProperty, "MahApps.Brushes.Accent");
        }

        public static readonly DependencyProperty ProgressBarForegroundProperty = DependencyProperty.Register("ProgressBarForeground", typeof(Brush), typeof(ProgressDialog), new FrameworkPropertyMetadata(default(Brush), FrameworkPropertyMetadataOptions.AffectsRender));
        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register("Message", typeof(string), typeof(ProgressDialog), new PropertyMetadata(default(string)));
        public static readonly DependencyProperty IsCancelableProperty = DependencyProperty.Register("IsCancelable", typeof(bool), typeof(ProgressDialog), new PropertyMetadata(default(bool), (s, e) => { ((ProgressDialog)s).PART_NegativeButton.Visibility = (bool)e.NewValue ? Visibility.Visible : Visibility.Hidden; }));
        public static readonly DependencyProperty NegativeButtonTextProperty = DependencyProperty.Register("NegativeButtonText", typeof(string), typeof(ProgressDialog), new PropertyMetadata("Cancel"));

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public bool IsCancelable
        {
            get { return (bool)GetValue(IsCancelableProperty); }
            set { SetValue(IsCancelableProperty, value); }
        }

        public string NegativeButtonText
        {
            get { return (string)GetValue(NegativeButtonTextProperty); }
            set { SetValue(NegativeButtonTextProperty, value); }
        }

        public Brush ProgressBarForeground
        {
            get { return (Brush)GetValue(ProgressBarForegroundProperty); }
            set { SetValue(ProgressBarForegroundProperty, value); }
        }

        internal CancellationToken CancellationToken => DialogSettings.CancellationToken;

        internal double Minimum
        {
            get { return PART_ProgressBar.Minimum; }
            set { PART_ProgressBar.Minimum = value; }
        }

        internal double Maximum
        {
            get { return PART_ProgressBar.Maximum; }
            set { PART_ProgressBar.Maximum = value; }
        }

        internal double ProgressValue
        {
            get { return PART_ProgressBar.Value; }
            set
            {
                PART_ProgressBar.IsIndeterminate = false;
                PART_ProgressBar.Value = value;
                PART_ProgressBar.ApplyTemplate();
            }
        }

        internal void SetIndeterminate()
        {
            PART_ProgressBar.IsIndeterminate = true;
        }
    }
}
