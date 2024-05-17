using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using WithMultiTenant.DbContextModel;
using WithMultiTenant.Services.TenantService.DTOs;
using WithMultiTenant.Tenants;

namespace WithMultiTenant.Services.TenantService
{
    public class TenantService : ITenantService
    {
        private readonly TenantDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;

        public TenantService(IServiceProvider serviceProvider, IConfiguration configuration, TenantDbContext context)
        {
            _serviceProvider = serviceProvider;
            _configuration = configuration;
            _context = context;
        }

        public Tenant CreateTenant(CreateTenantRequest request)
        {
            string newConnectionString = null;

            if (request.Isolated == true)
            {
                string dbNane = "multiTenantAppDb-" + request.Id;
                string defaultConnectionString = _configuration.GetConnectionString("DefaultConnection");
                newConnectionString = defaultConnectionString.Replace("multiTenantAppDb", dbNane);


                try
                {
                    using IServiceScope scopeTenant = _serviceProvider.CreateScope();
                    ApplicationDbContext dbContext = scopeTenant.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    dbContext.Database.SetConnectionString(newConnectionString);

                    if (dbContext.Database.GetPendingMigrations().Any())
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"New Db Context Creating {request.Id}");
                        Console.ResetColor();

                        dbContext.Database.Migrate();
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }


            Tenant tenant = new Tenant()
            {
                Id = request.Id,
                Name = request.Name,
                ConnectionString = newConnectionString
            };

            _context.Add(tenant);
            _context.SaveChanges();

            return tenant;
        }
    }
}
