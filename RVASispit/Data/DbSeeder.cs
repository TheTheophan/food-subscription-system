using Microsoft.AspNetCore.Identity;
using RVASispit.Constants;
using RVASispit.Models;

namespace RVASispit.Data
{
    public static class DbSeeder
    {
        public static async Task SeedRolesAsync(IServiceProvider service)
        {
            var userManager = service.GetService<UserManager<ApplicationUser>>();
            var roleManager = service.GetService<RoleManager<IdentityRole>>();

            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Moderator.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));

            var user = new ApplicationUser
            {
                UserName = "admin@raf.rs",
                Email = "admin@raf.rs",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,

            };

            var userInDB = await userManager.FindByEmailAsync(user.Email);

            if (userInDB == null)
            {
                await userManager.CreateAsync(user, "Cet123$");
                await userManager.AddToRoleAsync(user, Roles.Admin.ToString());

            }


        }
    }
}
