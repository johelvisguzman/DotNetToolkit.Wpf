namespace DotNetToolkit.Wpf.Test
{
    using Commands;
    using NUnit.Framework;
    using System.Threading.Tasks;
    using System.Windows.Input;

    [TestFixture]
    public class RelayCommandAsyncTests
    {
        private class MockViewModel
        {
            public RelayCommandAsync Command { get; set; }
        }

        [Test]
        public async Task ExecuteAsync()
        {
            var isExecuted = false;
            var mockViewModel = new MockViewModel();

            mockViewModel.Command = new RelayCommandAsync(() =>
            {
                isExecuted = true;
                return Task.FromResult(0);
            });
            await mockViewModel.Command.ExecuteAsync(null);

            Assert.IsTrue(isExecuted);
        }

        [Test]
        public void Execute()
        {
            var isExecuted = false;
            var mockViewModel = new MockViewModel();

            mockViewModel.Command = new RelayCommandAsync(() =>
            {
                isExecuted = true;
                return Task.FromResult(0);
            });
            ((ICommand)mockViewModel.Command).Execute(null);

            Assert.IsTrue(isExecuted);
        }

        [Test]
        public void Can_Execute()
        {
            var canExecute = false;
            var mockViewModel = new MockViewModel();

            mockViewModel.Command = new RelayCommandAsync(() => { return Task.FromResult(0); }, o => canExecute);

            Assert.IsFalse(mockViewModel.Command.CanExecute(null));

            canExecute = true;

            Assert.IsTrue(mockViewModel.Command.CanExecute(null));
        }
    }
}
