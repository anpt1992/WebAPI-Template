using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using WebAPI_Template.Data;
using WebAPI_Template.Services;

namespace WebAPI_Template.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            if (String.IsNullOrEmpty(configuration["ConnectionString:SimpleDB"]))
            {
                configuration["ConnectionString:SimpleDB"] = Environment.GetEnvironmentVariable("ConnectionString");
            }
            services.AddDbContext<DataContext>(options => options.UseLazyLoadingProxies().UseMySql(configuration["ConnectionString:SimpleDB"], ServerVersion.AutoDetect(configuration["ConnectionString:SimpleDB"])));
            services.AddDefaultIdentity<IdentityUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<DataContext>();

            services.AddScoped<ITestService, TestService>();
        }
    }
}
