using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Signawel.Domain;
using Signawel.Domain.Determination;
using System.Threading.Tasks;

namespace Signawel.Data
{
    public static class DatabaseSeeder
    {

        public static async Task Seed(SignawelDbContext context, RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            var changes = false;

            if(await SeedDetermination(context))
                changes = true;

            if(await SeedRoles(roleManager))
                changes = true;

            if(await SeedUsers(userManager))
                changes = true;

            if(changes)
            {
                await context.SaveChangesAsync();
            }
        }

        private async static Task<bool> SeedDetermination(SignawelDbContext context)
        {
            if(await context.DeterminationGraphs.AnyAsync())
            {
                return false;
            }

            await context.DeterminationGraphs.AddAsync(new DeterminationGraph
            {
                Start = new DeterminationNode()
            });

            return true;
        }

        private static async Task<bool> SeedRoles(RoleManager<Role> roleManager)
        {
            var changes = false;

            foreach(var roleName in Role.Constants.Roles)
            {
                if(!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new Role
                    {
                        Name = roleName,
                        NormalizedName = roleName.ToUpper()
                    });
                    changes = true;
                }
            }

            return changes;
        }

        private async static Task<bool> SeedUsers(UserManager<User> userManager)
        {
            if(await userManager.Users.AnyAsync())
            {
                return false;
            }

            await userManager.CreateAsync(new User
            {
                Email = "admin@signawel.be",
                EmailConfirmed = true
            }, "Password@001");

            return true;
        }

    }
}
