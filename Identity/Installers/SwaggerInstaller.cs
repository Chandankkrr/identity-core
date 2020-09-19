using System;
using System.Collections.Generic;
using Identity.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Identity.Installers
{
    public class SwaggerInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
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

               var security = new OpenApiSecurityRequirement()
               {
                   {
                       new OpenApiSecurityScheme
                       {
                           Reference = new OpenApiReference
                           {
                            Id = "Bearer",
                           Type = ReferenceType.SecurityScheme
                       }
                   }, new List<string>()}
               };

               s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
               {
                   Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                   Name = "Authorization",
                   In = ParameterLocation.Header,
                   Type = SecuritySchemeType.ApiKey
               });

               s.AddSecurityRequirement(security);
           });
        }
    }
}