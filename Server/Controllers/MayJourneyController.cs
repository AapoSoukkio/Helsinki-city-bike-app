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
        public async Task<ActionResult<IEnumerable<MayJourney>>> GetMayJourneys(int pageNumber = 1, int pageSize = 100)
        {
            var result = await _db.MayJourneys
                .Where(j => j.Departure != null && j.Return != null && j.DepartureStationName != null && j.ReturnStationName != null && j.CoveredDistance != null && j.Duration != null)
                /*.OrderBy(j => j.Departure)*/ //Note to myself: this is making the query much slower --> because basicly we are taking last 100 rows from the table
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return Ok(result);
        }
    }
}
