using Microsoft.OpenApi.Models;

namespace Mauser
{
    public static class SwaggerServiceExtension
    {
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen((c) =>
            {
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }    });
            });
            return services;
        }
    }
}
