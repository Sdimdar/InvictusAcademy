﻿using AdminGateway.MVC.Models.DbModels;
using Microsoft.AspNetCore.Identity;

namespace AdminGateway.MVC.Models;

public class RoleInitializer
{
    public static async Task InitializeAsync(UserManager<AdminUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        string headAdmin = "admin@gmail.com";
        string password = "Aa12345!";

        var roles = new[] { RolesHelper.Administrator, RolesHelper.Manager, RolesHelper.Instructor};
        foreach (var role in roles)
        {
            if (await roleManager.FindByNameAsync(role) is null)
                await roleManager.CreateAsync(new IdentityRole(role));
        }


        if (await userManager.FindByNameAsync(headAdmin) == null)
        {
            AdminUser admin = new AdminUser { Email = headAdmin, UserName = headAdmin };
            IdentityResult result = await userManager.CreateAsync(admin, password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, "admin");
            }
        }
    }
}