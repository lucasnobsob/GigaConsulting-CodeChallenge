using GigaConsulting.Domain.Interfaces;
using GigaConsulting.Domain.Models;
using GigaConsulting.Infra.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GigaConsulting.Services.API.StartupExtensions
{
    public static class MigrationExtension
    {
        public static async Task ApplyMigrationsAsync(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                var context1 = services.GetRequiredService<EventStoreSqlContext>();
                var context2 = services.GetRequiredService<ApplicationDbContext>();

                ApplyMigrationIfNeeded(context1);
                ApplyMigrationIfNeeded(context2);

                var roleId = await SeedRoles(services);
                await SeedRooms(services);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "Error applying migrations.");
            }
        }

        private static void ApplyMigrationIfNeeded(DbContext context)
        {
            if (context.Database.GetPendingMigrations().Any())
                context.Database.Migrate();

        }

        private static async Task SeedRooms(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var roomRepository = serviceProvider.GetRequiredService<IRoomRepository>();

            if (await context.SeedHistory.AnyAsync(sh => sh.SeedName == "InitialSeed"))
                return;

            if (!context.Room.Any())
            {
                await roomRepository.Add(new Room
                {
                    IsDeleted = false,
                    Name = "Sala 1"
                });
                await roomRepository.Add(new Room
                {
                    IsDeleted = false,
                    Name = "Sala 2"
                });
                await roomRepository.Add(new Room
                {
                    IsDeleted = false,
                    Name = "Sala 3"
                });
                await roomRepository.Add(new Room
                {
                    IsDeleted = false,
                    Name = "Sala 4"
                });
                await roomRepository.Add(new Room
                {
                    IsDeleted = false,
                    Name = "Sala 5"
                });

                context.SeedHistory.Add(new SeedHistory
                {
                    Id = Guid.NewGuid(),
                    SeedName = "InitialSeed",
                    ExecutedAt = DateTime.UtcNow
                });

                await context.SaveChangesAsync();
            }
        }

        private static async Task<string?> SeedRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string roleName = "Admin";

            var role = await roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                role = new IdentityRole(roleName);
                var result = await roleManager.CreateAsync(role);

                if (!result.Succeeded)
                {
                    throw new Exception($"Erro ao criar a role '{roleName}': {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }

            return role.Id;
        }
    }
}
