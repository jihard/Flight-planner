using System;
using System.Collections.Generic;
using System.Linq;
using Flightplanner.API.Models;

namespace Flightplanner.API.Storage
{
    public class FlightsStorage
    {
        private static List<Flight> _flights;
        private static int _flightId;
        private static object _flightsLock = new();

        static FlightsStorage()
        {
            _flights = new List<Flight>();
            _flightId = 1;
        }

        public static void ClearFlightsList()
        {
            _flights.Clear();
        }

        public static List<Flight> GetFlights()
        {
            return _flights;
        }

        public static Flight FlightById(int id)
        {
            lock (_flightsLock)
            {
                return _flights.FirstOrDefault(f => f.Id == id);
            }
        }

        public static Flight SearchFlight(SearchFlights search)
        {
            lock (_flightsLock)
            {
                if (_flights.Any(i => i.From.AirportCode == search.From &&
                   i.To.AirportCode == search.To &&
                   i.DepartureTime.Substring(0, 10) == search.DepartureDate))
                {
                    Flight _flight = _flights.FirstOrDefault(i => i.From.AirportCode == search.From &&
                    i.To.AirportCode == search.To && i.DepartureTime.Substring(0, 10) == search.DepartureDate);
                    return _flight;
                }
                return null;
            }
        }

        public static Flight AddFlight(AddFlight nextFlight)
        {
            lock (_flightsLock)
            {
                var flight = new Flight
                {
                    Id = _flightId,
                    From = new Airport
                    {
                        Country = nextFlight.From.Country,
                        City = nextFlight.From.City,
                        AirportCode = nextFlight.From.AirportCode
                    },
                    To = new Airport
                    {
                        Country = nextFlight.To.Country,
                        City = nextFlight.To.City,
                        AirportCode = nextFlight.To.AirportCode
                    },
                    Carrier = nextFlight.Carrier,
                    DepartureTime = nextFlight.DepartureTime,
                    ArrivalTime = nextFlight.ArrivalTime
                };

                _flights.Add(flight);
                _flightId++;
                return flight;
            }
        }

        public static bool IsFlightAcceptable(AddFlight _flight)
        {
            if (_flight.From == null ||
                string.IsNullOrEmpty(_flight.From?.Country) ||
                string.IsNullOrEmpty(_flight.From?.City) ||
                string.IsNullOrEmpty(_flight.From?.AirportCode) ||
                _flight.To == null ||
                string.IsNullOrEmpty(_flight.To?.Country) ||
                string.IsNullOrEmpty(_flight.To?.City) ||
                string.IsNullOrEmpty(_flight.To?.AirportCode) ||
                string.IsNullOrEmpty(_flight.Carrier) ||
                string.IsNullOrEmpty(_flight.DepartureTime) ||
                string.IsNullOrEmpty(_flight.ArrivalTime))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool IsAirportAcceptable(AddFlight flight)
        {
            return string.Equals(flight.From.AirportCode.Replace(" ", string.Empty), flight.To.AirportCode.Replace(" ", string.Empty), StringComparison.CurrentCultureIgnoreCase);
        }

        public static bool IsOnFlightsBoard(AddFlight nextFlight)
        {
            lock (_flightsLock)
            {
                for (int i = 0; i < _flights.Count; i++)
                {
                    Flight flight = _flights[i];
                    if (flight.From.AirportCode == nextFlight.From.AirportCode &&
                        flight.From.Country == nextFlight.From.Country &&
                        flight.From.City == nextFlight.From.City &&
                        flight.To.AirportCode == nextFlight.To.AirportCode &&
                        flight.To.Country == nextFlight.To.Country &&
                        flight.To.City == nextFlight.To.City &&
                        flight.Carrier == nextFlight.Carrier &&
                        flight.DepartureTime == nextFlight.DepartureTime &&
                        flight.ArrivalTime == nextFlight.ArrivalTime)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public static bool IsStrangeDate(AddFlight _flight)
        {
            return DateTime.Parse(_flight.DepartureTime) >= DateTime.Parse(_flight.ArrivalTime);
        }

        public static void DeleteFlight(int id)
        {
            lock (_flightsLock)
            {
                var _flight = FlightById(id);

                if (_flight != null)
                {
                    _flights.Remove(_flight);
                }
            }
        }
    }
}
