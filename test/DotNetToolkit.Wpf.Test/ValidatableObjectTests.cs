namespace DotNetToolkit.Wpf.Test
{
    using ComponentModel;
    using NUnit.Framework;
    using System.ComponentModel;
    using System.Linq;

    [TestFixture]
    public class ValidatableObjectTests
    {
        private class MockValidatableObject : ValidatableObject
        {
            public string Name { get; set; }

            public void Invalidate()
            {
                SetError(() => Name, "The name is required.");
            }

            public void ClearValidationError()
            {
                ClearErrors(() => Name);
            }

            public void ClearAllValidationErrors()
            {
                ClearErrors();
            }
        }

        [Test]
        public void IsINotifyDataErrorInfo()
        {
            Assert.IsTrue(typeof(INotifyDataErrorInfo).IsAssignableFrom(typeof(ValidatableObject)));
        }

        [Test]
        public void IsObservableObject()
        {
            Assert.IsTrue(typeof(ObservableObject).IsAssignableFrom(typeof(ValidatableObject)));
        }

        [Test]
        public void Can_Set_And_Get_Errors()
        {
            var obj = new MockValidatableObject();

            Assert.IsFalse(obj.HasErrors);
            Assert.IsNull(obj.GetErrors(() => obj.Name)?.Cast<string>().ToList().FirstOrDefault());

            obj.Invalidate();

            Assert.IsTrue(obj.HasErrors);
            Assert.AreEqual("The name is required.", obj.GetErrors(() => obj.Name).Cast<string>().ToList().FirstOrDefault());
        }

        [Test]
        public void Can_Clear_Error()
        {
            var obj = new MockValidatableObject();

            Assert.IsFalse(obj.HasErrors);

            obj.Invalidate();

            Assert.IsTrue(obj.HasErrors);

            obj.ClearValidationError();

            Assert.IsFalse(obj.HasErrors);
        }

        [Test]
        public void Can_All_Clear_Errors()
        {
            var obj = new MockValidatableObject();

            Assert.IsFalse(obj.HasErrors);

            obj.Invalidate();

            Assert.IsTrue(obj.HasErrors);

            obj.ClearAllValidationErrors();

            Assert.IsFalse(obj.HasErrors);
        }

        [Test]
        public void Set_Errors_Raises_ErrorsChanged_Event()
        {
            var eventIsRaised = false;
            var obj = new MockValidatableObject();

            obj.ErrorsChanged += (sender, e) => eventIsRaised = true;

            obj.Invalidate();

            Assert.IsTrue(eventIsRaised);
        }
    }
}
