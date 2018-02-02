namespace DotNetToolkit.Wpf.Test
{
    using ComponentModel;
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

        [Test]
        public void IsValidatableObject()
        {
            Assert.IsTrue(typeof(ValidatableObject).IsAssignableFrom(typeof(ViewModelBase)));
        }
    }
}
