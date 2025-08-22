using LesAmes.Domain.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LesAmes.Infrastructure.Database;

public static class MigrationManager
{
    public static async Task MigrateDatabaseAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger("MigrationManager");

        try
        {
            var dbContext = serviceProvider.GetRequiredService<AppDbContext>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // 1. Appliquer les migrations  
            logger.LogInformation("Applying database migrations...");
            await dbContext.Database.MigrateAsync();
            logger.LogInformation("Database migrations applied successfully.");

            // 2. Créer les rôles  
            logger.LogInformation("Seeding roles...");
            string[] roles = [Roles.Admin, Roles.Tutor, Roles.SuperAdmin, Roles.Advisor];
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new ApplicationRole { Name = role });
                    logger.LogInformation("Role {Role} created.", role);
                }
            }

            // 3. Créer un compte admin par défaut  
            string adminEmail = "admin@gmail.com";
            string adminPassword = "Admin1234!";

            var adminUser = await userManager.FindByNameAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var createResult = await userManager.CreateAsync(adminUser, adminPassword);
                if (!createResult.Succeeded)
                {
                    logger.LogError("Failed to create Admin user: {Errors}",
                        string.Join(", ", createResult.Errors.Select(e => e.Description)));
                    return; // On s'arrête ici, pas la peine de continuer si l'user n'existe pas
                }

                var addToRoleResult = await userManager.AddToRoleAsync(adminUser, Roles.SuperAdmin);
                if (!addToRoleResult.Succeeded)
                {
                    logger.LogError("Failed to add Admin user to role {Role}: {Errors}",
                        Roles.SuperAdmin,
                        string.Join(", ", addToRoleResult.Errors.Select(e => e.Description)));
                }
                else
                {
                    logger.LogInformation("Default admin user created and assigned to {Role}.", Roles.SuperAdmin);
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred during database migration and seeding.");
            throw; // Très important : remonter l'exception pour éviter de lancer une app cassée  
        }
    }
}
