
using System.Collections.Generic;
using Flightplanner.API.Models;

namespace Flightplanner.API.Storage
{
    public static class AirportsStorage
    {
        private static List<Airport> Airports;

        static AirportsStorage()
        {
            Airports = new List<Airport>();
        }

        public static void ClearAirportsList()
        {
            Airports.Clear();
        }

        public static void AddAirport(Airport airport)
        {
            if (!Airports.Contains(airport))
            {
                Airports.Add(airport);
            }
        }

        public static Airport[] GetAirport(string search)
        {
            Airport result = Airports.Find(a => a.City.ToLower().StartsWith(search.ToLower().Replace(" ", string.Empty)) 
            || a.Country.ToLower().StartsWith(search.ToLower().Replace(" ", string.Empty)) 
            || a.AirportCode.ToLower().StartsWith(search.ToLower().Replace(" ", string.Empty)));
            return new Airport[]
            {
                result
            };
        }
    }
}