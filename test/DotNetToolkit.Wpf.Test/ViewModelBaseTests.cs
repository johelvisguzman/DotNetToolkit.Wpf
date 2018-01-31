namespace DotNetToolkit.Wpf.Test
{
    using Common;
    using Mvvm;
    using NUnit.Framework;

    [TestFixture]
    public class ViewModelBaseTests
    {
        [Test]
        public void IsAbstractClass()
        {
            Assert.IsTrue(typeof(ViewModelBase).IsAbstract);
        }

        [Test]
        public void IsObservableObject()
        {
            Assert.IsTrue(typeof(ObservableObject).IsAssignableFrom(typeof(ViewModelBase)));
        }
    }
}
