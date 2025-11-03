using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace BackEndUpch.SConfigurator
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Mi API Local",
                    Version = "v1"
                });
            });

            return services;
        }
    }
}
