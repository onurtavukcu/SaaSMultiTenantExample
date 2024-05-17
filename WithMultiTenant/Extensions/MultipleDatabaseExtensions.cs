using Microsoft.EntityFrameworkCore;
using WithMultiTenant.DbContextModel;
using WithMultiTenant.Tenants;

namespace WithMultiTenant.Extensions
{
    public static class MultipleDatabaseExtensions
    {
        public static IServiceCollection AddAndMigrateTenantDatabases(this IServiceCollection services, IConfiguration configuration)
        {
            using IServiceScope scopeTenant = services.BuildServiceProvider().CreateScope();
            TenantDbContext tenantDbContext = scopeTenant.ServiceProvider.GetService<TenantDbContext>();

            if (tenantDbContext.Database.GetPendingMigrations().Any())
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Applying the Base Database Migrations");
                Console.ResetColor();
                tenantDbContext.Database.Migrate();
            }

            List<Tenant> tenantsInDb = tenantDbContext.Tenants.ToList();
            string defaultConnectionString = configuration.GetConnectionString("DefaultConnection");


            foreach (Tenant tenant in tenantsInDb)
            {
                string connectionString = string.IsNullOrEmpty(tenant.ConnectionString) ? defaultConnectionString : tenant.ConnectionString;

                using IServiceScope scopeApplication = services.BuildServiceProvider().CreateScope();
                ApplicationDbContext dbContext = scopeApplication.ServiceProvider.GetService<ApplicationDbContext>();
                dbContext.Database.SetConnectionString(connectionString);

                if (dbContext.Database.GetPendingMigrations().Any())
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"Applying Migrations For {tenant.Id}");
                    Console.ResetColor();
                    dbContext.Database.Migrate();
                }
            }
            return services;
        }
    }
}
