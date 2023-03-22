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
            _context.BikeStations.Add(new BikeStation { ID = 1, Name = "Station1", Adress = "Address1" });
            _context.BikeStations.Add(new BikeStation { ID = 2, Name = "Station2", Adress = "Address2" });
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
        public async Task GetStations_ReturnsExpectedResults_WhenStationNameIsProvided()
        {
            // Arrange
            var controller = new BikeStationController(null, _context);
            string stationName = "Station1";

            // Act
            var result = await controller.GetStations(stationName);

            // Assert
            NUnit.Framework.Assert.IsNotNull(result);
            NUnit.Framework.Assert.IsInstanceOf<ActionResult<IEnumerable<BikeStation>>>(result);
            NUnit.Framework.Assert.AreEqual(1, result.Value.Count());
            NUnit.Framework.Assert.AreEqual("Station1", result.Value.First().Name);
        }
    }
}
