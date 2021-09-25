using System.Collections.Generic;
using Flightplanner.API.Models;
using Flightplanner.API.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Flightplanner.API.Controllers
{
    [ApiController]
    [Route("admin-api/flights")]
    [Authorize]
    public class AdminApiController : ControllerBase
    {
        [HttpGet]
        public List<Flight> ReturnFlights()
        {
            return FlightsStorage.GetFlights();
        }

        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<Flight> ReturnFlight(int id)
        {
            var _flight = FlightsStorage.FlightById(id);

            if (_flight != null)
            {
                return _flight;
            }
            return NotFound();
        }

        [HttpPut]
        public IActionResult AddFlightRequest(AddFlight _flight)
        {
            Airport fromAirport = _flight.From;
            Airport toAirport = _flight.To;
            Flight _flightCreated;
            AirportsStorage.AddAirport(fromAirport);
            AirportsStorage.AddAirport(toAirport);

            if (!FlightsStorage.IsFlightAcceptable(_flight) ||
                FlightsStorage.IsAirportAcceptable(_flight) ||
                FlightsStorage.IsStrangeDate(_flight))
            {
                return BadRequest();
            }
            else if (FlightsStorage.IsOnFlightsBoard(_flight))
            {
                return Conflict(_flight);
            }
            else 
            {
                _flightCreated = FlightsStorage.AddFlight(_flight);
                return Created("", _flightCreated);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public void DeleteFlightRequest(int id)
        {
            FlightsStorage.DeleteFlight(id);
        }
    }
}
