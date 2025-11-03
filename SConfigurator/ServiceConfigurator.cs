using BackEndUpch.Data;
using BackEndUpch.Domain.Interfaces;
using BackEndUpch.Infrastructure;
using BackEndUpch.Services;
using BackEndUpch.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackEndUpch.SConfigurator
{
    public static class ServiceConfigurator
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<CarsDbContext>(options =>
                options.UseSqlServer(connectionString)
                       .EnableSensitiveDataLogging()
                       .EnableDetailedErrors());

            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<ICarService, CarService>();

            return services;
        }
    }
}
