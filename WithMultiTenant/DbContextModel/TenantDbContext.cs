using Microsoft.EntityFrameworkCore;
using WithMultiTenant.Tenants;

namespace WithMultiTenant.DbContextModel
{
    public class TenantDbContext : DbContext
    {
        public TenantDbContext(DbContextOptions<TenantDbContext> options) : base(options)
        {
        }

        public DbSet<Tenant> Tenants { get; set; }
    }
}
