using BackEndUpch.Data;
using BackEndUpch.Domain.Interfaces;
using BackEndUpch.Infrastructure;
using BackEndUpch.SConfigurator;
using BackEndUpch.Services;
using BackEndUpch.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ✅ Conexión a la base de datos
builder.Services.AddDbContext<CarsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ Inyección de dependencias
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<ICarService, CarService>();

// ✅ Agregar configuración de CORS desde tu clase CorsConfig
builder.Services.AddCorsConfig();

// ✅ Controladores y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ Comprobar conexión al crear el servicio
using (var scope = builder.Services.BuildServiceProvider().CreateScope())
{
    var db = scope.ServiceProvider.GetService<CarsDbContext>();
    Console.WriteLine(db == null ? "❌ CarsDbContext no está registrado" : "✅ CarsDbContext registrado correctamente");
}

var app = builder.Build();

// ✅ Middleware global de excepciones (debes tener tu clase ExceptionMiddleware creada)
app.UseMiddleware<ExceptionMiddleware>();

// ✅ Swagger solo en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ✅ Activar política CORS definida en CorsConfig
app.UseCors("AllowReactApp");

// ✅ HTTPS y autorización
app.UseHttpsRedirection();
app.UseAuthorization();

// ✅ Rutas
app.MapControllers();

// ✅ Iniciar la aplicación
app.Run();
