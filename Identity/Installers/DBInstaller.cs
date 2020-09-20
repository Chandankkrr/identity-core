using Identity.DBContext;
using Identity.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Installers
{
    public class DBInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<IdentityCoreDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("IdentityCoreDBConnection"));
            });

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
                options.SignIn.RequireConfirmedEmail = true)
            .AddEntityFrameworkStores<IdentityCoreDbContext>()
            .AddDefaultTokenProviders();
        }
    }
}