
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WithMultiTenant.DbContextModel;
using WithMultiTenant.Extensions;
using WithMultiTenant.MiddleWare;
using WithMultiTenant.Services;
using WithMultiTenant.Services.TenantService;
using WithMultiTenant.Tenants;

namespace WithMultiTenant
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            
            builder.Services.AddScoped<ICurrentTenantService, CurrentTenantService>();


            builder.Services.AddDbContext<ApplicationDbContext>
                (
                options => options
                .UseNpgsql
                (
                 builder.Configuration.GetConnectionString("DefaultConnection"),
                 b => b.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName)
                 )
                );

            builder.Services.AddDbContext<TenantDbContext>
               (
               options => options
               .UseNpgsql
               (
                builder.Configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName)
                )
               );

            builder.Services.AddAndMigrateTenantDatabases(builder.Configuration);

            builder.Services.AddTransient<IProductService, ProductService>();
            builder.Services.AddTransient<ITenantService, TenantService>();


            var app = builder.Build();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseMiddleware<TenantResolver>();

            app.MapControllers();

            app.Run();
        }
    }
}
