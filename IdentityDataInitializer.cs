using HomeSecurityApp.Data;
using Microsoft.AspNetCore.Identity;

public static class IdentityDataInitializer
{
    public static async Task SeedData(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        // Add roles
        if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
        {
            await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
        }
        if (!await roleManager.RoleExistsAsync(UserRoles.Viewer))
        {
            await roleManager.CreateAsync(new IdentityRole(UserRoles.Viewer));
        }

        // Add an admin user
        var adminUser = new IdentityUser { UserName = "admin@admin.com", Email = "admin@admin.com" };
        var user = await userManager.FindByEmailAsync(adminUser.Email);
        if (user == null)
        {
            await userManager.CreateAsync(adminUser, "Admin@123");
            await userManager.AddToRoleAsync(adminUser, UserRoles.Admin);
        }
    }
}
