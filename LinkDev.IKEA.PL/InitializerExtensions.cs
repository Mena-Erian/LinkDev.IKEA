using LinkDev.IKEA.DAL.Contracts;
using LinkDev.IKEA.DAL.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace LinkDev.IKEA.PL
{
    public static class InitializerExtensions
    {
        public static async Task InitializeDatabaseAsync(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var services = scope.ServiceProvider;
            var dbInitializer = services.GetRequiredService<IDbInitializer>(); // Ask Explicitly for the service

            dbInitializer.Initialize();
            dbInitializer.SeedData();

            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            //dbInitializer.SeedUsersAsync(userManager, roleManager).GetAwaiter().GetResult();
            await dbInitializer.SeedUsersAsync(userManager, roleManager);
        }
    }
}
