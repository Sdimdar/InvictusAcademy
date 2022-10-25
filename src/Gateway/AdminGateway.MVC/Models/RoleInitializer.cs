using AdminGateway.MVC.Models.DbModels;
using Microsoft.AspNetCore.Identity;

namespace AdminGateway.MVC.Models
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            string headAdmin = "admin@gmail.com";
            string password = "Aa12345!";

            var roles = new[] { "admin", "manager", "copywriter", "instructor", "moderator" };
            foreach (var role in roles)
            {
                if (await roleManager.FindByNameAsync(role) is null)
                    await roleManager.CreateAsync(new IdentityRole(role));
            }


            if (await userManager.FindByNameAsync(headAdmin) == null)
            {
                User admin = new User { Email = headAdmin, UserName = headAdmin };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}