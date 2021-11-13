using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Reservas.Areas.Identity.Data;
using Reservas.Map;
using Reservas.Models;

namespace Reservas.Data
{
    public class ReservasDbContext : IdentityDbContext<ApplicationUser>
    {
        public ReservasDbContext(DbContextOptions<ReservasDbContext> options)
            : base(options)
        {
        }

        public DbSet<Mesa> Mesa { get; set; }
        public DbSet<Reserva> Reserva { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new MesaMap());
            builder.ApplyConfiguration(new ReservaMap());
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
