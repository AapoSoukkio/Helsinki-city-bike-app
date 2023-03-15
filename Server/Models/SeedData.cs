using Microsoft.EntityFrameworkCore;
using Solita.HelsinkiBikeApp.Server.Data;

namespace Solita.HelsinkiBikeApp.Server.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BikeContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<BikeContext>>()))
            {
                if (context == null)
                {
                    throw new ArgumentNullException("Null PeopleContext");
                }

                context.Database.EnsureCreated();


                if (context.MayJourneys.Any())
                {
                    return;   // DB has been seeded
                              // To re-seed the db: delete the existing *.db file and let the app create a new one
                }

                context.SaveChanges();
            }
        }
    }
}
