using System;
using System.Collections.Generic;
using Flightplanner.API.Models;
using Flightplanner.API.Storage;
using Microsoft.AspNetCore.Mvc;
using PageResult = Flightplanner.API.Models.PageResult;

namespace Flightplanner.API.Controllers
{
    [ApiController]
    [Route("api")]

    public class CustomerApiController : ControllerBase
    {
        [HttpGet]
        [Route("airports")]

        public Airport[] ReturnAirport(string search)
        {
            return AirportsStorage.GetAirport(search);
        }

        [HttpPost]
        [Route("flights/search")]

        public ActionResult<PageResult> ReturnFlights(SearchFlights search)
        {
            Flight _flight = FlightsStorage.SearchFlight(search);

            if (!(!string.IsNullOrEmpty(search.From) && !string.IsNullOrEmpty(search.To) &&
                !string.IsNullOrEmpty(search.DepartureDate)) ||
                string.Equals(search.From, search.To, StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest();
            }
            else if (_flight != null)
            {
                PageResult result = new PageResult
                {
                    Page = 0,
                    TotalItems = 1,
                    Items = new List<Flight>
                    {
                        _flight
                    }
                };
                return result;
            }
            else
            {
                return new PageResult
                {
                    Page = 0,
                    TotalItems = 0,
                    Items = new List<Flight>()
                };
            }
        }

        [HttpGet]
        [Route("flights/{id:int}")]

        public ActionResult<Flight> ReturnFlightById(int id)
        {
            var flight = FlightsStorage.FlightById(id);

            if (flight != null)
            {
                return flight;
            }
            return NotFound();
        }
    }
}
