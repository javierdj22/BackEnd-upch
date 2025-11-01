using BackEndUpch.Data;
using BackEndUpch.Repositories;
using BackEndUpch.Repositories.Interfaces;
using BackEndUpch.Services;
using BackEndUpch.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace BackEndUpch
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        { 
            // CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:5173") // URL de tu frontend
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });
            // DB: asegúrate que DefaultConnection esté en appsettings.json o variables de entorno de Lambda
            services.AddDbContext<CarsDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                       .EnableSensitiveDataLogging()
                       .EnableDetailedErrors());

            // Inyecciones
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<ICarService, CarService>();

            // Controllers
            services.AddControllers();

            // Swagger solo si realmente quieres exponerlo (opcional en Lambda)
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCors("AllowReactApp");
            // NO dependas de IWebHostEnvironment, puede ser null en Lambda
            // Usa un try/catch o simplemente ignora Swagger
            if (app.ApplicationServices.GetService<IWebHostEnvironment>()?.IsDevelopment() == true)
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
