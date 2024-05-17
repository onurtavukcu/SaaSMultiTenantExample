namespace WithMultiTenant.Services
{
    public interface ICurrentTenantService
    {
        string? TenantId { get; set; }
        public Task<bool> SetTenant(string tenant);
        public string? ConnectionString { get; set; }
    }
}
