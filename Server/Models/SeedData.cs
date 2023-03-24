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


                if (context.Summer21Journeys.Any())
                {
                    return;   // DB has been seeded
                              // To re-seed the db: delete the existing *.db file and let the app create a new one
                              // In this case we dont have any seed data below so it will create empty database
                }

                context.SaveChanges();
            }
        }
    }
}
