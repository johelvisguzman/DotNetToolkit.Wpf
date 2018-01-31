namespace DotNetToolkit.Wpf.Test
{
    using DotNetToolkit.Wpf.Mvvm;
    using NUnit.Framework;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;

    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class ViewLocatorTests
    {
        private class MockView : FrameworkElement { }
        private class MockViewModel { }
        private class MockRandomAViewModel { }

        [Test]
        public void Can_Locate_ViewModel()
        {
            FrameworkElement element;
            var viewModel = new MockViewModel();
            var expectedViewName = typeof(MockView).Name;

            element = ViewLocator.LocateFor(viewModel);
            Assert.IsNotNull(element);
            Assert.AreEqual(expectedViewName, element.GetType().Name);
            Assert.AreEqual(viewModel, element.DataContext);

            element = ViewLocator.LocateFor(viewModel.GetType());
            Assert.IsNotNull(element);
            Assert.AreEqual(expectedViewName, element.GetType().Name);
            Assert.AreNotEqual(viewModel, element.DataContext);

            element = ViewLocator.LocateFor<MockViewModel>();
            Assert.IsNotNull(element);
            Assert.AreEqual(expectedViewName, element.GetType().Name);
        }

        [Test]
        public void Can_Not_Locate_ViewModel()
        {
            TextBlock element;

            element = (TextBlock)ViewLocator.LocateFor(new MockRandomAViewModel());
            Assert.AreEqual($"Cannot locate view for '{typeof(MockRandomAViewModel).FullName}'.", element.Text);

            element = (TextBlock)ViewLocator.LocateFor(new MockRandomAViewModel().GetType());
            Assert.AreEqual($"Cannot locate view for '{typeof(MockRandomAViewModel).FullName}'.", element.Text);

            element = (TextBlock)ViewLocator.LocateFor<MockRandomAViewModel>();
            Assert.AreEqual($"Cannot locate view for '{typeof(MockRandomAViewModel).FullName}'.", element.Text);
        }
    }
}
