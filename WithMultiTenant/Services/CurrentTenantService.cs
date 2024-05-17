
using Microsoft.EntityFrameworkCore;
using WithMultiTenant.DbContextModel;

namespace WithMultiTenant.Services
{
    public class CurrentTenantService : ICurrentTenantService
    {
        private readonly TenantDbContext _context;

        public CurrentTenantService(TenantDbContext context)
        {
            _context = context;
        }
        public string? TenantId { get; set; }
        public string? ConnectionString { get; set; }

        public async Task<bool> SetTenant(string tenant)
        {
            var tenantInfo = await _context.Tenants.Where(x => x.Id == tenant).FirstOrDefaultAsync();

            if (tenantInfo != null) 
            {
                TenantId = tenantInfo.Id;
                ConnectionString = tenantInfo.ConnectionString;
                return true;
            }
            else
            {
                throw new Exception("Invalid Tenant Name");
            }
        }
    }
}
