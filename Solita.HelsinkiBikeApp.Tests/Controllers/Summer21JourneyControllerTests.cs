using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Solita.HelsinkiBikeApp.Server.Controllers;
using Solita.HelsinkiBikeApp.Server.Data;
using Solita.HelsinkiBikeApp.Shared;

namespace Solita.HelsinkiBikeApp.Tests.Controllers
{
    [TestFixture]
    public class Summer21JourneyControllerTests
    {
        private BikeContext _context;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<BikeContext>()
                .UseInMemoryDatabase(databaseName: "Summer21Journeys")
                .Options;

            _context = new BikeContext(options);

            // It is possible that the Setup method is being called multiple times when running the tests
            // That's the reason why we are clearing and creating the database again

            // Clear the database
            _context.Database.EnsureDeleted();

            // Create a new database
            _context.Database.EnsureCreated();

            _context.Summer21Journeys.Add(new Summer21Journey { ID = 1, Departure = "2021-06-01", Return = "2021-06-01", DepartureStationId = 1, DepartureStationName = "Station1", ReturnStationId = 2, ReturnStationName = "Station2", CoveredDistance = 1000, Duration = 1200 });
            _context.Summer21Journeys.Add(new Summer21Journey { ID = 2, Departure = "2021-06-01", Return = "2021-06-01", DepartureStationId = 1, DepartureStationName = "Station1", ReturnStationId = 2, ReturnStationName = "Station2", CoveredDistance = 2000, Duration = 2400 });
            _context.SaveChanges();
        }

        [Test]
        public async Task Get_ReturnsAllDataFromSummer21JourneyTable()
        {
            // Arrange
            var controller = new Summer21JourneyController(null, _context);

            // Act
            var result = await controller.Get();

            // Assert
            NUnit.Framework.Assert.IsNotNull(result);
            NUnit.Framework.Assert.IsInstanceOf<IEnumerable<Summer21Journey>>(result);
            NUnit.Framework.Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task GetJourneys_ReturnsExpectedResults_WhenValidDataIsProvided()
        {
            // Arrange
            var controller = new Summer21JourneyController(null, _context);
            DateTime departureDate = new DateTime(2021, 6, 1);
            int pageNumber = 1;
            int pageSize = 100;

            // Act
            var result = await controller.GetJourneys(departureDate, pageNumber, pageSize);

            // Assert
            NUnit.Framework.Assert.IsNotNull(result);
            NUnit.Framework.Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            NUnit.Framework.Assert.IsInstanceOf<List<Summer21Journey>>(okResult.Value);
            NUnit.Framework.Assert.AreEqual(2, (okResult.Value as List<Summer21Journey>).Count());
        }

    }
}
