using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Signawel.Domain.Tests.Identity
{
    [TestFixture]
    public class RoleTests
    {

        [Test]
        public void DumbTest()
        {
            Assert.That(Role.Constants.Roles, Is.EqualTo(new string[] { Role.Constants.Instance, Role.Constants.Admin }));
        }
    }
}
