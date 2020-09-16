using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Identity.Installers
{
    public class SwaggerInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
           {
               s.SwaggerDoc("v1", new OpenApiInfo
               {
                   Version = "v1",
                   Title = "Identity Core API",
                   Description = "Identity Core API",
                   TermsOfService = new Uri("https://example.com/terms"),
                   Contact = new OpenApiContact
                   {
                       Name = "Chandan Rauniyar",
                       Email = string.Empty,
                       Url = new Uri("https://twitter.com/rauniyrchandan"),
                   },
               });

               s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
               {
                   Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                   Name = "Authorization",
                   In = ParameterLocation.Header,
                   Type = SecuritySchemeType.ApiKey
               });
           });
        }
    }
}