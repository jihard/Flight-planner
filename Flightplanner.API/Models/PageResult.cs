﻿
using System.Collections.Generic;


namespace Flightplanner.API.Models
{
    public class PageResult
    {
        public int Page { get; set; }
        public int TotalItems { get; set; }
        public List<Flight> Items { get; set; }
    }
}
