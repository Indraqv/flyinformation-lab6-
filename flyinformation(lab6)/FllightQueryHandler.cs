using System;
using System.Collections.Generic;
using System.Linq;

namespace flyinformation_lab6_
{
    public class FlightQueryHandler
    {
        private readonly FlightInformationSystem flightSystem;

        public FlightQueryHandler(FlightInformationSystem system)
        {
            flightSystem = system;
        }

        public List<Flight> GetFlightsByAirline(string airline)
        {
            return flightSystem.GetAllFlights()
                .Where(f => f.Airline.Equals(airline, StringComparison.OrdinalIgnoreCase))
                .OrderBy(f => f.DepartureTime)
                .ToList();
        }

        public List<Flight> GetDelayedFlights()
        {
            return flightSystem.GetAllFlights()
                .Where(f => f.Status == FlightStatus.Delayed)
                .OrderByDescending(f => f.Duration)
                .ToList();
        }

        public List<Flight> GetFlightsByDepartureDate(DateTime date)
        {
            return flightSystem.GetAllFlights()
                .Where(f => f.DepartureTime.Date == date.Date)
                .OrderBy(f => f.DepartureTime)
                .ToList();
        }

        public List<Flight> GetFlightsByTimeRangeAndDestination(DateTime startTime, DateTime endTime, string destination)
        {
            return flightSystem.GetAllFlights()
                .Where(f => f.DepartureTime >= startTime
                            && f.DepartureTime <= endTime
                            && f.Destination.Equals(destination, StringComparison.OrdinalIgnoreCase))
                .OrderBy(f => f.DepartureTime)
                .ToList();
        }

        public List<Flight> GetFlightsArrivedInTimeRange(DateTime startTime, DateTime endTime)
        {
            return flightSystem.GetAllFlights()
                .Where(f => f.ArrivalTime >= startTime && f.ArrivalTime <= endTime)
                .OrderBy(f => f.ArrivalTime)
                .ToList();
        }
    }
}