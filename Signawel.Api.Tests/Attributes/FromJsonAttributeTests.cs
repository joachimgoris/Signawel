using NUnit.Framework;
using Signawel.API.Attributes;
using Signawel.API.BinderProviders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Signawel.Api.Tests.Attributes
{
    [TestFixture]
    public class FromJsonAttributeTests
    {

        [Test]
        public void Constructor_ShouldSetBinderType_ToJsonModelBinder()
        {
            // arrange

            // Act
            var attrib = new FromJsonAttribute();

            // Assert
            Assert.That(attrib, Is.Not.Null);
            Assert.That(attrib.BinderType, Is.EqualTo(typeof(JsonModelBinder)));
        }

    }
}
