namespace DotNetToolkit.Wpf.Demo.ViewModels
{
    using Mvvm;

    public class MvvmExampleViewModel : ViewModelBase
    {
        private string _testMessageOne;
        public string TestMessageOne
        {
            get { return _testMessageOne; }
            set { SetProperty(ref _testMessageOne, value); }
        }

        private string _testMessageTwo;
        public string TestMessageTwo
        {
            get { return _testMessageTwo; }
            set { SetProperty(ref _testMessageTwo, value); }
        }

        private object _testChildViewOne;
        public object TestChildViewOne
        {
            get { return _testChildViewOne; }
            set { SetProperty(ref _testChildViewOne, value); }
        }

        private object _testChildViewTwo;
        public object TestChildViewTwo
        {
            get { return _testChildViewTwo; }
            set { SetProperty(ref _testChildViewTwo, value); }
        }

        private object _testChildViewModelOne;
        public object TestChildViewModelOne
        {
            get { return _testChildViewModelOne; }
            set { SetProperty(ref _testChildViewModelOne, value); }
        }

        public MvvmExampleViewModel()
        {
            TestMessageOne = "Hello World!";
            TestMessageTwo = "I have not been initialized!";
            TestChildViewOne = ViewLocator.LocateFor<ChildExampleViewModel>();
            TestChildViewTwo = ViewLocator.LocateFor<ChildNoViewExampleViewModel>();
            TestChildViewModelOne = new ChildExampleViewModel();
        }

        protected override void OnInitialize()
        {
            TestMessageTwo = "I have been initialized!";
        }
    }
}
