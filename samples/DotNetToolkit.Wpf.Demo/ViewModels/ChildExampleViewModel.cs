namespace DotNetToolkit.Wpf.Demo.ViewModels
{
    public class ChildExampleViewModel : DotNetToolkit.Wpf.Mvvm.ViewModelBase
    {
        private string _testMessageOne;
        public string TestMessageOne
        {
            get { return _testMessageOne; }
            set { Set(ref _testMessageOne, value); }
        }

        public ChildExampleViewModel()
        {
            TestMessageOne = "Hello from the Child View!!";
        }
    }
}
