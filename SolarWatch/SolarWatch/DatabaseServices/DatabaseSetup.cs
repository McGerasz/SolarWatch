using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SolarWatch.DatabaseServices.Authentication;
using SolarWatch.Model;

namespace SolarWatch.DatabaseServices;

public static class DatabaseSetup
{
    public static void Run(IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            SeedData(serviceScope.ServiceProvider.GetService<SolarWatchApiContext>(),
                serviceScope.ServiceProvider.GetService<UsersContext>());
            AddRoles(serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>());
            AddAdmin(serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>());
        }

        void SeedData(SolarWatchApiContext solarContext, UsersContext identityContext)
        {
            //Waiting for database to setup

            Thread.Sleep(15000);
            solarContext.Database.EnsureDeleted();
            identityContext.Database.EnsureDeleted();
            solarContext.Database.Migrate();
            identityContext.Database.Migrate();
            
            Thread.Sleep(5000);
        }

        void AddRoles(RoleManager<IdentityRole> roleManager)
        {

            Console.WriteLine("Adding Roles...");
            
            var tAdmin = CreateAdminRole(roleManager);
            tAdmin.Wait();

            var tUser = CreateUserRole(roleManager);
            tUser.Wait();
        }

        async Task CreateAdminRole(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        async Task CreateUserRole(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole("User"));
        }

        void AddAdmin(UserManager<IdentityUser> userManager)
        {
            var tAdmin = CreateAdminIfNotExists(userManager);
            tAdmin.Wait();
        }

        async Task CreateAdminIfNotExists(UserManager<IdentityUser> userManager)
        {

            var adminInDb = await userManager.FindByEmailAsync(Environment.GetEnvironmentVariable("ASPNETCORE_ADMINEMAIL"));
            if (adminInDb == null)
            {

                Console.WriteLine("Creating Admin Role...");
                
                var admin = new IdentityUser { UserName = "admin", Email = Environment.GetEnvironmentVariable("ASPNETCORE_ADMINEMAIL") };
                var adminCreated = await userManager.CreateAsync(admin, Environment.GetEnvironmentVariable("ASPNETCORE_ADMINPASSWORD"));

                if (adminCreated.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}
