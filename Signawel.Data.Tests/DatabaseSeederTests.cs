using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Signawel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signawel.Data.Tests
{
    [TestFixture]
    public class DatabaseSeederTests
    {

        [Test]
        public async Task Seed_ShouldSeedData()
        {
            var services = new ServiceCollection();

            services.AddDbContext<SignawelDbContext>(options =>
                options.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()));

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<SignawelDbContext>();


            var provider = services.BuildServiceProvider();

            var context = provider.GetRequiredService<SignawelDbContext>();
            var userManager = provider.GetService<UserManager<User>>();
            var roleManager = provider.GetService<RoleManager<Role>>();

            // Act
            await DatabaseSeeder.Seed(context, roleManager, userManager);

            // Assert
            Assert.That(context.DeterminationGraphs.Count(), Is.EqualTo(1));
            Assert.That(context.DeterminationNodes.Count(), Is.EqualTo(1));
            Assert.That(context.Roles.Count(), Is.EqualTo(2));

            // Act 2 (If existing data is preset no more should be added
            await DatabaseSeeder.Seed(context, roleManager, userManager);

            // Assert
            Assert.That(context.DeterminationGraphs.Count(), Is.EqualTo(1));
            Assert.That(context.DeterminationNodes.Count(), Is.EqualTo(1));
            Assert.That(context.Roles.Count(), Is.EqualTo(2));
        }

    }
}
