using Microsoft.EntityFrameworkCore;
using Signawel.MobileData;
using System;

namespace Signawel.Mobile.Tests.Builders
{
    internal static class SignawelMobileContextBuilder
    {

        internal static SignawelMobileContext GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<SignawelMobileContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new SignawelMobileContext(options);
            databaseContext.Database.EnsureCreated();
            return databaseContext;
        }

    }
}
