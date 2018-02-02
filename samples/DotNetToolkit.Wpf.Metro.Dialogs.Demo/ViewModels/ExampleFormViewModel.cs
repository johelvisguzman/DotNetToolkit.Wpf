namespace DotNetToolkit.Wpf.Metro.Dialogs.Demo.ViewModels
{
    using Annotations;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class ExampleFormViewModel : IDataErrorInfo, INotifyPropertyChanged
    {
        private bool _isValid;
        public bool IsValid
        {
            get { return _isValid; }
            private set
            {
                _isValid = value;
                OnPropertyChanged();
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private string _notes;
        public string Notes
        {
            get { return _notes; }
            set
            {
                _notes = value;
                OnPropertyChanged();
            }
        }

        #region Implementation of IDataErrorInfo

        public string this[string columnName]
        {
            get
            {
                if (columnName.Equals(nameof(Name)))
                {
                    if (string.IsNullOrEmpty(Name))
                    {
                        IsValid = false;
                        return "The name is a required field.";
                    }
                }

                IsValid = true;
                return null;
            }
        }

        public string Error { get { throw new NotImplementedException(); } }

        #endregion

        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
