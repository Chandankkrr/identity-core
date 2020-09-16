using Microsoft.Extensions.DependencyInjection;

namespace Identity.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services)
        {
            services.AddControllers();
        }
    }
}