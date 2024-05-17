using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using WithMultiTenant.DbContextModel;

namespace WithMultiTenant.Services.TenantFactory
{
    public class TenantDbContextFactory : IDesignTimeDbContextFactory<TenantDbContext>
    {
        public TenantDbContext CreateDbContext(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = configuration.GetConnectionString("DefaultConnection");

            DbContextOptionsBuilder<TenantDbContext> optionsBuilder = new();

            _ = optionsBuilder.UseNpgsql(connectionString);

            return new TenantDbContext(optionsBuilder.Options);
        }
    }
}
