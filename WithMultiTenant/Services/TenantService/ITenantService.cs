using WithMultiTenant.Services.TenantService.DTOs;
using WithMultiTenant.Tenants;

namespace WithMultiTenant.Services.TenantService
{
    public interface ITenantService
    {
        Tenant CreateTenant(CreateTenantRequest request);
    }
}
