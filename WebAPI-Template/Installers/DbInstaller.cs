using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAPI_Template.Data;

namespace WebAPI_Template.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options => options.UseLazyLoadingProxies().UseMySql(configuration["ConnectionString:SimpleDB"], ServerVersion.AutoDetect(configuration["ConnectionString:SimpleDB"])));
            services.AddIdentityCore<IdentityUser>().AddEntityFrameworkStores<DataContext>();
        }
    }
}
