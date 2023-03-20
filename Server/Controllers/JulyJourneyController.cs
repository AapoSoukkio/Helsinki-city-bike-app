using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solita.HelsinkiBikeApp.Server.Data;
using Solita.HelsinkiBikeApp.Shared;

namespace Solita.HelsinkiBikeApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JulyJourneyController : ControllerBase
    {
        private readonly ILogger<JulyJourneyController> _logger;
        private readonly BikeContext _db;
        public JulyJourneyController(ILogger<JulyJourneyController> logger, BikeContext context)
        {
            _logger = logger;
            _db = context;
        }

        [HttpGet]
        public async Task<IEnumerable<JulyJourney>> Get()
        {
            //Returns all data from JulyJouney table
            return await _db.JulyJourneys.ToListAsync();
        }

        [HttpGet("getjourneys")]
        public async Task<ActionResult<IEnumerable<JulyJourney>>> GetJulyJourneys(DateTime? departureDate = null, int pageNumber = 1, int pageSize = 100)
        {
            // Default departure date if not provided by user
            if (!departureDate.HasValue)
            {
                departureDate = new DateTime(2021, 7, 31);
            }

            var departureDateString = departureDate.Value.ToString("yyyy-MM-dd");

            var result = await _db.JulyJourneys
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
    }
}
