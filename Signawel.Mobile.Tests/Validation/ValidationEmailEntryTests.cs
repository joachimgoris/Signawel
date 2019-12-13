using System;
using NUnit.Framework;
using Signawel.Mobile.Validation.Controls;
using Xamarin.Forms;

namespace Signawel.Mobile.Tests.Validation
{
    [TestFixture]
    public class ValidationEmailEntryTests
    {
        private ValidationEmailEntry _sutValidationEmailEntry;

        [SetUp]
        public void SetUp()
        {
            _sutValidationEmailEntry = new ValidationEmailEntry();
        }

        [Test]
        public void IsValid_ShouldBeFalse_WhenNoValidEmailIsEntered()
        {
            var email = Guid.NewGuid().ToString();

            _sutValidationEmailEntry.OnTextChanged(_sutValidationEmailEntry, 
                new TextChangedEventArgs(string.Empty, email));

            Assert.That(_sutValidationEmailEntry.EntryText, Is.EqualTo(email));
            Assert.That(_sutValidationEmailEntry.IsValid, Is.False);
        }

        [Test]
        public void IsValid_ShouldBeFalse_WhdnValidEmailIsEntered()
        {
            var email = "test@test.com";

            _sutValidationEmailEntry.OnTextChanged(_sutValidationEmailEntry,
                new TextChangedEventArgs(string.Empty, email));

            Assert.That(_sutValidationEmailEntry.EntryText, Is.EqualTo(email));
            Assert.That(_sutValidationEmailEntry.IsValid, Is.True);
        }
    }
}
