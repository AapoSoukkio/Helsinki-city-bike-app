using Microsoft.EntityFrameworkCore;
using Solita.HelsinkiBikeApp.Shared;

namespace Solita.HelsinkiBikeApp.Server.Data
{
    public class BikeContext : DbContext
    {
        public BikeContext(DbContextOptions<BikeContext> options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Here we could do some configuration to table models like say HasNoKey(); etc...
        }

        public DbSet<BikeStation> BikeStations { get; set; }
        public DbSet<Summer21Journey> Summer21Journeys { get; set; }
    }
}
