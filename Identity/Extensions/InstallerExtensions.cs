using System;
using System.Linq;
using Identity.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Extensions
{
    public static class InstallerExtensions
    {
        public static void InstallServicesInAssembly(this IServiceCollection services, IConfiguration configuration)
        {
            var installers = typeof(Startup).Assembly.ExportedTypes
                .Where(t => typeof(IInstaller).IsAssignableFrom(t)
                    && !t.IsAbstract
                    && !t.IsInterface
                )
                .Select(Activator.CreateInstance)
                .Cast<IInstaller>()
                .ToList();

            foreach (var installer in installers)
            {
                installer.InstallServices(services, configuration);
            }
        }
    }
}