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

        [HttpGet("gethundred")]
        public async Task<IEnumerable<MayJourney>> GetHundred()
        {
            // Returns the first 100 rows from MayJourney table
            return await _db.MayJourneys.Take(100).ToListAsync();
        }
    }
}
