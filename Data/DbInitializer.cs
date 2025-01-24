using Core.Constants;
using Core.Entities;

using Microsoft.AspNetCore.Identity;

namespace IdentityProject
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
           await AddRolesAsync(roleManager);
           await AddAdminAsync(userManager, roleManager);
        }
        private static async Task AddRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            foreach (var role in Enum.GetValues<UserRoles>())
            {
                if (!await roleManager.RoleExistsAsync(role.ToString()))
                {
                    await roleManager.CreateAsync(new IdentityRole
                    {
                        Name = role.ToString(),
                    });
                }
            }
        }
        private static async Task AddAdminAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {

            if (await userManager.FindByEmailAsync("admin@code.edu.az") is null)
            {

                var user = new User
                {
                    UserName = "admin@code.edu.az",
                    Email = "admin@code.edu.az",
                };

                var result = await userManager.CreateAsync(user, "Admin123.");
                if (!result.Succeeded)
                    throw new Exception("Could not add admin");

                var role = await roleManager.FindByNameAsync("Admin");
                if (role?.Name is null)
                    throw new Exception("Could not find admin role");

                var addToResult = await userManager.AddToRoleAsync(user, role.Name);
                if (!addToResult.Succeeded)
                    throw new Exception("It was not possible to add the admin role to the user");
            }
        }
    }
}
