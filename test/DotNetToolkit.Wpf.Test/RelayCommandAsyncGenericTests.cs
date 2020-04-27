namespace DotNetToolkit.Wpf.Test
{
    using Commands;
    using NUnit.Framework;
    using System.Threading.Tasks;
    using System.Windows.Input;

    [TestFixture]
    public class RelayCommandAsyncGenericTests
    {
        private class MockViewModel
        {
            public RelayCommandAsync<string> Command { get; set; }
        }

        [Test]
        public async Task ExecuteAsync()
        {
            var expectedArgument = "Test";
            var isExecuted = false;
            var mockViewModel = new MockViewModel();

            mockViewModel.Command = new RelayCommandAsync<string>(x =>
            {
                if (!string.IsNullOrEmpty(x) && x.Equals(expectedArgument))
                    isExecuted = true;

                return Task.FromResult(0);
            });

            await mockViewModel.Command.ExecuteAsync(null);
            Assert.IsFalse(isExecuted);

            await mockViewModel.Command.ExecuteAsync(expectedArgument);
            Assert.IsTrue(isExecuted);
        }

        [Test]
        public void Execute()
        {
            var expectedArgument = "Test";
            var isExecuted = false;
            var mockViewModel = new MockViewModel();

            mockViewModel.Command = new RelayCommandAsync<string>(x =>
            {
                if (!string.IsNullOrEmpty(x) && x.Equals(expectedArgument))
                    isExecuted = true;

                return Task.FromResult(0);
            });

            ((ICommand)mockViewModel.Command).Execute(null);
            Assert.IsFalse(isExecuted);

            ((ICommand)mockViewModel.Command).Execute(expectedArgument);
            Assert.IsTrue(isExecuted);
        }

        [Test]
        public void Can_Execute()
        {
            var canExecute = false;
            var mockViewModel = new MockViewModel();

            mockViewModel.Command = new RelayCommandAsync<string>(x => { return Task.FromResult(0); }, x => canExecute);

            Assert.IsFalse(mockViewModel.Command.CanExecute(null));

            canExecute = true;

            Assert.IsTrue(mockViewModel.Command.CanExecute(null));
        }
    }
}
