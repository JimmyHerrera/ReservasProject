using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reservas.Areas.Identity.Data;
using Reservas.Data;

[assembly: HostingStartup(typeof(Reservas.Areas.Identity.IdentityHostingStartup))]
namespace Reservas.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<ReservasDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("ReservasDbContextConnection")));

                services.AddDefaultIdentity<ApplicationUser>(options => { 
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;

                })
                    .AddEntityFrameworkStores<ReservasDbContext>();
            });
        }
    }
}