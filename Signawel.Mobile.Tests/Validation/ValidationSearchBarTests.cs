using System;
using NUnit.Framework;
using Signawel.Mobile.Validation.Controls;

namespace Signawel.Mobile.Tests.Validation
{
    [TestFixture]
    public class ValidationSearchBarTests
    {
        private ValidationSearchBar _sutSearchBar;

        [SetUp]
        public void SetUp()
        {
            _sutSearchBar = new ValidationSearchBar();
        }

        [Test]
        public void IsValid_ShouldBeFalse_WhenNavigatedButSearchedElementIsNull()
        {
            _sutSearchBar.Navigated = true;
            
            Assert.That(_sutSearchBar.SearchedElement, Is.Null);
            Assert.That(_sutSearchBar.IsValid, Is.False);
        }

        [Test]
        public void IsValid_ShouldBeTrue_WhenNavigatedAndObjectIsNotNull()
        {
            _sutSearchBar.Navigated = true;
            _sutSearchBar.SearchedElement = Guid.NewGuid().ToString();

            Assert.That(_sutSearchBar.SearchedElement, Is.Not.Null);
            Assert.That(_sutSearchBar.IsValid, Is.True);
        }
    }
}
