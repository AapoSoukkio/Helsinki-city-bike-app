using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solita.HelsinkiBikeApp.Server.Data;
using Solita.HelsinkiBikeApp.Shared;

namespace Solita.HelsinkiBikeApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Summer21JourneyController : ControllerBase
    {
        private readonly ILogger<Summer21JourneyController> _logger;
        private readonly BikeContext _db;

        public Summer21JourneyController(ILogger<Summer21JourneyController> logger, BikeContext context)
        {
            _logger = logger;
            _db = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Summer21Journey>> Get()
        {
            //Returns all data from Summer21Journey table
            return await _db.Summer21Journeys.ToListAsync();
        }

        [HttpGet("getjourneys")]
        public async Task<ActionResult<IEnumerable<Summer21Journey>>> GetJourneys(DateTime? departureDate = null, int pageNumber = 1, int pageSize = 100)
        {
            // Default departure date if not provided by user
            if (!departureDate.HasValue)
            {
                departureDate = new DateTime(2021, 5, 31);
            }

            var departureDateString = departureDate.Value.ToString("yyyy-MM-dd");

            var result = await _db.Summer21Journeys
                .Where(j => j.Departure != null && j.Return != null
                            && j.DepartureStationName != null && j.ReturnStationName != null
                            && j.CoveredDistance != null && j.Duration != null
                            && j.CoveredDistance >= 10 && j.Duration >= 10
                            && j.Departure.Contains(departureDateString))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Distinct()
                .ToListAsync();

            return Ok(result);
        }

        [HttpGet("journeysstarted")]
        public async Task<int> GetNumberOfJourneysStarted(int id)
        {
            //Returns the total number of journeys starting from the station 
            return await _db.Summer21Journeys.Where(j => j.DepartureStationId == id).CountAsync();
        }

        [HttpGet("journeysended")]
        public async Task<int> GetNumberOfJourneysEnded(int id)
        {
            //Returns the total number of journeys ending at the station
            return await _db.Summer21Journeys.Where(j => j.ReturnStationId == id).CountAsync();
        }

        [HttpGet("average_distance_starting_from_station")]
        public async Task<ActionResult<double>> GetAverageDistanceStartingFromStation(int id)
        {
            var journeys = await _db.Summer21Journeys
                                         .Where(j => j.DepartureStationId == id && j.CoveredDistance >= 10)
                                         .ToListAsync();

            if (journeys.Count == 0)
            {
                return NotFound();
            }

            double totalDistanceInMeters = (double)journeys.Sum(j => j.CoveredDistance);
            double totalDistanceInKilometers = totalDistanceInMeters / 1000;
            double averageDistanceInKilometers = totalDistanceInKilometers / journeys.Count;

            return Ok(Math.Round(averageDistanceInKilometers, 1));
        }

        [HttpGet("average_distance_ending_at_station")]
        public async Task<ActionResult<double>> GetAverageDistanceEndingAtStation(int id)
        {
            var journeys = await _db.Summer21Journeys
                                         .Where(j => j.ReturnStationId == id && j.CoveredDistance >= 10)
                                         .ToListAsync();

            if (journeys.Count == 0)
            {
                return NotFound();
            }

            double totalDistanceInMeters = (double)journeys.Sum(j => j.CoveredDistance);
            double totalDistanceInKilometers = totalDistanceInMeters / 1000;
            double averageDistanceInKilometers = totalDistanceInKilometers / journeys.Count;

            return Ok(Math.Round(averageDistanceInKilometers, 1));
        }

        [HttpGet("top_return_stations")]
        public async Task<ActionResult<List<BikeStation>>> GetTopReturnStations(int id)
        {
            var journeys = await _db.Summer21Journeys
                                         .Where(j => j.DepartureStationId == id)
                                         .GroupBy(j => j.ReturnStationId)
                                         .Select(g => new { StationId = g.Key, Count = g.Count() })
                                         .OrderByDescending(g => g.Count)
                                         .Take(5)
                                         .ToListAsync();

            var stationIds = journeys.Select(j => j.StationId).ToList();
            var stations = await _db.BikeStations
                                        .Where(s => stationIds.Contains(s.ID))
                                        .ToListAsync();

            // Order the stations by the order of the journey count
            var orderedStations = stationIds.Select(id => stations.First(s => s.ID == id)).ToList();

            return Ok(orderedStations);
        }

    }
}
