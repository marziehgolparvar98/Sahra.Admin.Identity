using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;

namespace Sahra.Accelerator.api.Startups.Swagger
{
    public static class Swagger
    {
        public static void AddSwagger(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddSwaggerGen(current =>
            {
                current.SwaggerDoc("ClientV1", new OpenApiInfo { Title = "Accelerator", Version = "v1" });
                current.SwaggerDoc("AdminV1", new OpenApiInfo { Title = "Accelerator Admin", Version = "v1" });
                current.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                current.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Name = "Bearer",
                            Scheme = "oauth2",

                            In =
                             ParameterLocation.Header,

                            Reference =
                                new OpenApiReference
                                {
                                    Type =ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                },
                        },
                        new List<string>()
                    }
                });
            });
        }
    }
}
