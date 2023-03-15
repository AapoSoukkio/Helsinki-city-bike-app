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

        public DbSet<MayJourney> MayJourneys { get; set; }
        public DbSet<JuneJourney> JuneJourneys { get; set; }
        public DbSet<JulyJourney> JulyJourneys { get; set; }
        public DbSet<BikeStation> BikeStations { get; set; }
    }
}
