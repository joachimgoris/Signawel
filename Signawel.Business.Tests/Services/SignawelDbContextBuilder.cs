using Microsoft.EntityFrameworkCore;
using Signawel.Data;
using System;

namespace Signawel.Business.Tests.Services
{
    internal static class SignawelDbContextBuilder
    {
        internal static SignawelDbContext GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<SignawelDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new SignawelDbContext(options);
            databaseContext.Database.EnsureCreated();
            return databaseContext;
        }
    }
}
