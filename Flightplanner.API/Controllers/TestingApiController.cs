using Flightplanner.API.Storage;
using Microsoft.AspNetCore.Mvc;

namespace Flightplanner.API.Controllers
{
    [ApiController]
    [Route("testing-api")]

    public class TestingApiController : ControllerBase
    {
        [HttpPost]
        [Route("clear")]

        public IActionResult Clear()
        {
            FlightsStorage.ClearFlightsList();
            AirportsStorage.ClearAirportsList();
            return Ok();
        }
    }
}