namespace DotNetToolkit.Wpf.Test
{
    using DotNetToolkit.Wpf.Mvvm;
    using NUnit.Framework;
    using System.Windows;

    [TestFixture]
    public class ViewModelLocatorTests
    {
        private class MockView : FrameworkElement { }
        private class MockViewModel { }
        private class MockRandomBView : FrameworkElement { }

        [Test]
        public void Can_Locate_View()
        {
            object viewModel = null;
            var expectedViewModelName = typeof(MockViewModel).Name;

            viewModel = ViewModelLocator.LocateFor(typeof(MockView));
            Assert.IsNotNull(viewModel);
            Assert.AreEqual(expectedViewModelName, viewModel.GetType().Name);

            viewModel = ViewModelLocator.LocateFor<MockView>();
            Assert.IsNotNull(viewModel);
            Assert.AreEqual(expectedViewModelName, viewModel.GetType().Name);
        }

        [Test]
        public void Can_Not_Locate_View()
        {
            Assert.IsNull(ViewModelLocator.LocateFor(typeof(MockRandomBView)));
            Assert.IsNull(ViewModelLocator.LocateFor<MockRandomBView>());
        }
    }
}
