namespace DotNetToolkit.Wpf.Test
{
    using Commands;
    using NUnit.Framework;

    [TestFixture]
    public class RelayCommandGenericTests
    {
        private class MockViewModel
        {
            public RelayCommand<string> Command { get; set; }
        }

        [Test]
        public void Execute()
        {
            var expectedArgument = "Test";
            var isExecuted = false;
            var mockViewModel = new MockViewModel();

            mockViewModel.Command = new RelayCommand<string>(x =>
            {
                if (!string.IsNullOrEmpty(x) && x.Equals(expectedArgument))
                    isExecuted = true;
            });

            mockViewModel.Command.Execute(null);
            Assert.IsFalse(isExecuted);

            mockViewModel.Command.Execute(expectedArgument);
            Assert.IsTrue(isExecuted);
        }

        [Test]
        public void Can_Execute()
        {
            var canExecute = false;
            var mockViewModel = new MockViewModel();

            mockViewModel.Command = new RelayCommand<string>(x => { }, x => canExecute);

            Assert.IsFalse(mockViewModel.Command.CanExecute(null));

            canExecute = true;

            Assert.IsTrue(mockViewModel.Command.CanExecute(null));
        }
    }
}
