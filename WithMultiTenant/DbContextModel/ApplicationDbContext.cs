using Microsoft.EntityFrameworkCore;
using WithMultiTenant.Models;
using WithMultiTenant.Services;
using WithMultiTenant.Tenants;

namespace WithMultiTenant.DbContextModel
{
    public class ApplicationDbContext : DbContext
    {
        public readonly ICurrentTenantService _currentTenantService;
        public string CurrentTenantId;
        public string CurrentTenantConnectionString;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentTenantService currentTenantService) : base(options)
        {
            _currentTenantService = currentTenantService;
            CurrentTenantId = _currentTenantService.TenantId;
            CurrentTenantConnectionString = _currentTenantService.ConnectionString;
        }

        public DbSet<Product> Products { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasQueryFilter(x => x.TenantId == CurrentTenantId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string tenantConnectionString = CurrentTenantConnectionString;

            if (!string.IsNullOrEmpty(tenantConnectionString))
            {
                _ = optionsBuilder.UseNpgsql(tenantConnectionString);
            }
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<IMustHaveTenant>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                    case EntityState.Modified:
                        entry.Entity.TenantId = CurrentTenantId;
                        break;
                }
            }
                var result = base.SaveChanges();
                return result;
        }
    }
}
