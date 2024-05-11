using Domain.Common.Players;
using Domain.NFL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DepthChart.Infrastructure.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Domain.Common.DepthChart> Chart { get; set; }

        //public DbSet<Player> Players { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            builder.Entity<Domain.Common.DepthChart>()
            .HasKey(d => new { d.League, d.Team });

            builder.Entity<Domain.Common.DepthChart>()
            .Property(b => b.Chart)
            .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<Dictionary<string, IEnumerable<Player>>>(v));


            base.OnModelCreating(builder);
        }
    }
}
