using System;
using System.Linq;
using Identity.Installers;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Extensions
{
    public static class InstallerExtensions
    {
        public static void InstallServicesInAssembly(this IServiceCollection services)
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
                installer.InstallServices(services);
            }
        }
    }
}