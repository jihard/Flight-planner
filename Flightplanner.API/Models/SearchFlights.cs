using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flightplanner.API.Models
{
    public class SearchFlights
    {
        public string From { get; set; }
        public string To { get; set; }
        public string DepartureDate { get; set; }
    }
}
