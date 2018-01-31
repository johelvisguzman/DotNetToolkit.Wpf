namespace DotNetToolkit.Wpf.Test
{
    using Common;
    using NUnit.Framework;

    [TestFixture]
    public class ObservableObjectTests
    {
        public class TestObservableObject : ObservableObject
        {
            private string _testPropertyOne;
            public string TestPropertyOne
            {
                get { return _testPropertyOne; }
                set { Set(ref _testPropertyOne, value); }
            }

            private string _testPropertyTwo;
            public string TestPropertyTwo
            {
                get { return _testPropertyTwo; }
                set { Set(() => TestPropertyTwo, ref _testPropertyTwo, value); }
            }

            private string _testPropertyThree;
            public string TestPropertyThree
            {
                get { return _testPropertyThree; }
                set
                {
                    _testPropertyThree = value;
                    RaisePropertyChanged();
                }
            }

            private string _testPropertyFour;
            public string TestPropertyFour
            {
                get { return _testPropertyFour; }
                set
                {
                    _testPropertyFour = value;
                    RaisePropertyChanged(() => TestPropertyFour);
                }
            }
        }

        [Test]
        public void RaisePropertyChanged_Raises_PropertyChanged_Event()
        {
            var eventIsRaised = false;
            var obj = new TestObservableObject();

            obj.PropertyChanged += (sender, e) => eventIsRaised = true;
            obj.TestPropertyThree = "Random Value";

            Assert.IsTrue(eventIsRaised);
        }

        [Test]
        public void RaisePropertyChanged_With_Expression_Raises_PropertyChanged_Event()
        {
            var eventIsRaised = false;
            var obj = new TestObservableObject();

            obj.PropertyChanged += (sender, e) => eventIsRaised = true;
            obj.TestPropertyFour = "Random Value";

            Assert.IsTrue(eventIsRaised);
        }

        [Test]
        public void Set_Raises_PropertyChanged_Event()
        {
            var eventIsRaised = false;
            var obj = new TestObservableObject();

            obj.PropertyChanged += (sender, e) => eventIsRaised = true;
            obj.TestPropertyOne = "Random Value";

            Assert.IsTrue(eventIsRaised);
        }

        [Test]
        public void Set_With_Expression_Raises_PropertyChanged_Event()
        {
            var eventIsRaised = false;
            var obj = new TestObservableObject();

            obj.PropertyChanged += (sender, e) => eventIsRaised = true;
            obj.TestPropertyTwo = "Random Value";

            Assert.IsTrue(eventIsRaised);
        }
    }
}
