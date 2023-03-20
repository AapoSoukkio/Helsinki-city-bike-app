using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solita.HelsinkiBikeApp.Server.Data;
using Solita.HelsinkiBikeApp.Shared;

namespace Solita.HelsinkiBikeApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BikeStationController : ControllerBase
    {
        private readonly ILogger<BikeStationController> _logger;
        private readonly BikeContext _db;

        public BikeStationController(ILogger<BikeStationController> logger, BikeContext context)
        {
            _logger = logger;
            _db = context;
        }

        [HttpGet]
        public async Task<IEnumerable<BikeStation>> Get()
        {
            //Returns all data from BikeStation table
            return await _db.BikeStations.ToListAsync();
        }


        [HttpGet("stations")]
        public async Task<ActionResult<IEnumerable<BikeStation>>> GetStations(string? stationName = null, int pageNumber = 1, int pageSize = 100)
        {
            //TODO: If user is starting the search with 2 or more capital letter we will get no matches
            // Also white space in front of the search word not giving the correct results
            if (stationName != null && stationName != "")
            {
                string stationNameToUpper = char.ToUpper(stationName[0]) + stationName.Substring(1);

                var result = await _db.BikeStations
                    .Where(b => b.Name != null && b.Adress != null && b.Name.Contains(stationName) || b.Name.Contains(stationNameToUpper))
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Distinct()
                    .ToListAsync();

                return Ok(result);
            }
            else
            {
                var result = await _db.BikeStations
                    .Where(b => b.Name != null && b.Adress != null)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Distinct()
                    .ToListAsync();

                return Ok(result);
            }
        }
    }
}
