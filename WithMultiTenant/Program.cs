
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WithMultiTenant.DbContextModel;
using WithMultiTenant.MiddleWare;
using WithMultiTenant.Services;

namespace WithMultiTenant
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddTransient<IProductService, ProductService>();
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

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseMiddleware<TenantResolver>();

            app.MapControllers();

            app.Run();
        }
    }
}
