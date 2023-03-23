using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Solita.HelsinkiBikeApp.Server.Controllers;
using Solita.HelsinkiBikeApp.Server.Data;
using Solita.HelsinkiBikeApp.Shared;

namespace Solita.HelsinkiBikeApp.Tests.Controllers
{
    [TestFixture]
    public class BikeStationControllerTests
    {
        private BikeContext _context;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<BikeContext>()
                .UseInMemoryDatabase(databaseName: "BikeStations")
                .Options;

            _context = new BikeContext(options);

            // It is possible that the Setup method is being called multiple times when running the tests
            // That's the reason why we are clearing and creating the database again

            // Clear the database
            _context.Database.EnsureDeleted();

            // Create a new database
            _context.Database.EnsureCreated();

            _context.BikeStations.Add(new BikeStation { ID = 1, Name = "StationOne", Adress = "Address1" });
            _context.BikeStations.Add(new BikeStation { ID = 2, Name = "StationTwo", Adress = "Address2" });
            _context.SaveChanges();
        }

        [Test]
        public async Task Get_ReturnsAllDataFromBikeStationTable()
        {
            // Arrange
            var controller = new BikeStationController(null, _context);

            // Act
            var result = await controller.Get();

            // Assert
            NUnit.Framework.Assert.IsNotNull(result);
            NUnit.Framework.Assert.IsInstanceOf<IEnumerable<BikeStation>>(result);
            NUnit.Framework.Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task GetStations_ReturnsAllDataFromBikeStationTable_WhenStationNameIsNull()
        {
            // Arrange
            var controller = new BikeStationController(null, _context);

            // Act
            var result = await controller.GetStations(null, 1, 100);

            // Assert
            NUnit.Framework.Assert.IsNotNull(result);
            NUnit.Framework.Assert.IsInstanceOf<ActionResult<IEnumerable<BikeStation>>>(result);
            NUnit.Framework.Assert.AreEqual(2, result.Value.Count());
        }
    }
}
