using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WEB_Boletim.Data;

[assembly: HostingStartup(typeof(WEB_Boletim.Areas.Identity.IdentityHostingStartup))]
namespace WEB_Boletim.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<LoginDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("LoginDbContextConnection")));

                services.AddDefaultIdentity<IdentityUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                })
                    
                    
                    .AddEntityFrameworkStores<LoginDbContext>();
            });
        }
    }
}