using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solita.HelsinkiBikeApp.Server.Data;
using Solita.HelsinkiBikeApp.Shared;

namespace Solita.HelsinkiBikeApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MayJourneyController : ControllerBase
    {
        private readonly ILogger<MayJourneyController> _logger;
        private readonly BikeContext _db;

        public MayJourneyController(ILogger<MayJourneyController> logger, BikeContext context)
        {
            _logger = logger;
            _db = context;
        }

        [HttpGet]
        public async Task<IEnumerable<MayJourney>> Get()
        {
            //Returns all data from MayJourney table
            return await _db.MayJourneys.ToListAsync();
        }

        [HttpGet("getjourneys")]
        public async Task<ActionResult<IEnumerable<MayJourney>>> GetMayJourneys(DateTime? departureDate = null, int pageNumber = 1, int pageSize = 100)
        {
            // Default departure date if not provided by user
            if (!departureDate.HasValue)
            {
                departureDate = new DateTime(2021, 5, 31);
            }

            var departureDateString = departureDate.Value.ToString("yyyy-MM-dd");

            var result = await _db.MayJourneys
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
            return await _db.MayJourneys.Where(j => j.DepartureStationId == id).CountAsync();
        }

    }
}
