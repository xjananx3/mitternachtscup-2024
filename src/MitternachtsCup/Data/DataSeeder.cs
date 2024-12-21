using System.Configuration;
using Microsoft.AspNetCore.Identity;

namespace MitternachtsCup.Data;

public class DataSeeder
{
    public static async Task SeedAdminUser(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();

        var adminRole = configuration["AdminUser:Role"];
        var adminEmail = configuration["AdminUser:Email"];
        var adminPassword = configuration["AdminUser:Password"];

        if (!await roleManager.RoleExistsAsync(adminRole))
        {
            await roleManager.CreateAsync(new IdentityRole(adminRole));
        }

        // Admin-Benutzer erstellen, falls er nicht existiert
        if (await userManager.FindByEmailAsync(adminEmail) == null)
        {
            var adminUser = new IdentityUser
            {
                UserName = adminEmail,
                Email = adminEmail
            };

            var createResult = await userManager.CreateAsync(adminUser, adminPassword);

            if (createResult.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, adminRole);
                Console.WriteLine($"Admin user '{adminEmail}' successfully created and assigned role '{adminRole}'.");
            }
            else
            {
                Console.WriteLine("Error creating admin user:");
                foreach (var error in createResult.Errors)
                {
                    Console.WriteLine($"- {error.Description}");
                }
            }
        }
        else
        {
            Console.WriteLine($"Admin user '{adminEmail}' already exists.");
        }
    }
}