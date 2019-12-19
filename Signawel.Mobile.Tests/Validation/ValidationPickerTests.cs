using System.Collections.Generic;
using NUnit.Framework;
using Signawel.Domain.Reports;
using Signawel.Mobile.Validation.Controls;
using Xamarin.Forms;

namespace Signawel.Mobile.Tests.Validation
{
    [TestFixture]
    public class ValidationPickerTests
    {
        private ValidationPicker _sutPicker;

        [SetUp]
        public void SetUp()
        {
            _sutPicker = new ValidationPicker();
        }

        [Test]
        public void IsValid_ShouldBeTrue_WhenItemIsSelected()
        {
            _sutPicker.ItemSource = new List<ReportDefaultIssue>
            {
                new ReportDefaultIssue(),
                new ReportDefaultIssue()
            };

            _sutPicker.PickerIndex = 1;

            Assert.That(_sutPicker.SelectedItem, Is.Not.Null);
            Assert.That(_sutPicker.IsValid, Is.True);
        }

        [Test]
        public void IsValid_ShouldBeFalse_WhenNoItemIsSelected()
        {
            _sutPicker.ValidationPickerPickerOnUnfocused(_sutPicker, 
                new FocusEventArgs(_sutPicker, false));

            Assert.That(_sutPicker.SelectedItem, Is.Null);
            Assert.That(_sutPicker.IsValid, Is.False);
        }
    }
}
