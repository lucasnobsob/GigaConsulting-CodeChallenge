using GigaConsulting.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GigaConsulting.Services.Api.StartupExtensions
{
    public static class DatabaseExtension
    {
        public static IServiceCollection AddCustomizedDatabase(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            var con = configuration.GetConnectionString("DefaultConnection") ?? "";
            services.AddDbContext<ApplicationDbContext>(options => {
                options.UseMySQL(con);
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                if (!env.IsProduction())
                {
                    options.EnableDetailedErrors();
                    options.EnableSensitiveDataLogging();
                }
            });

            services.AddDbContext<EventStoreSqlContext>(options => {
                options.UseMySQL(con);
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                if (!env.IsProduction())
                {
                    options.EnableDetailedErrors();
                    options.EnableSensitiveDataLogging();
                }
            });

            return services;
        }
    }
}
