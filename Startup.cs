using BackEndUpch.Data;
using BackEndUpch.Domain.Interfaces;
using BackEndUpch.Infrastructure;
using BackEndUpch.Services;
using BackEndUpch.Services.Interfaces;
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

        // 🔹 CONFIGURACIÓN DE SERVICIOS
        public void ConfigureServices(IServiceCollection services)
        {
            // 🔸 CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp", builder =>
                {
                    builder.WithOrigins("http://localhost:3000")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            // 🔸 Conexión a la base de datos
            var connectionString = "Server=carsdb.cwluqou4e2qx.us-east-1.rds.amazonaws.com;Database=CarsDb;User Id=admin;Password=NuevaClaveSegura1!;TrustServerCertificate=True;Encrypt=True;";

            services.AddDbContext<CarsDbContext>(options =>
                options.UseSqlServer(connectionString)
                       .EnableSensitiveDataLogging()
                       .EnableDetailedErrors());

            // 🔸 Repositorios y servicios
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<ICarService, CarService>();

            // 🔸 Controllers y JSON
            services.AddControllers()
                    .AddNewtonsoftJson();

            // 🔸 Swagger
            services.AddSwaggerGen();
        }

        // 🔹 CONFIGURACIÓN DEL PIPELINE
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BackEndUpch API v1");
                });
            }

            app.UseRouting();

            // 🔸 CORS
            app.UseCors("AllowReactApp");

            // 🔸 Endpoints
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
