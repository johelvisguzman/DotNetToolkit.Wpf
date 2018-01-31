namespace DotNetToolkit.Wpf.Test
{
    using Commands;
    using NUnit.Framework;

    [TestFixture]
    public class RelayCommandTests
    {
        private class MockViewModel
        {
            public RelayCommand Command { get; set; }
        }

        [Test]
        public void Execute()
        {
            var isExecuted = false;
            var mockViewModel = new MockViewModel();

            mockViewModel.Command = new RelayCommand(() => isExecuted = true);
            mockViewModel.Command.Execute(null);

            Assert.IsTrue(isExecuted);
        }

        [Test]
        public void Can_Execute()
        {
            var canExecute = false;
            var mockViewModel = new MockViewModel();

            mockViewModel.Command = new RelayCommand(() => { }, o => canExecute);

            Assert.IsFalse(mockViewModel.Command.CanExecute(null));

            canExecute = true;

            Assert.IsTrue(mockViewModel.Command.CanExecute(null));
        }
    }
}
