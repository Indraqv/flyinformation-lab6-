using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Newtonsoft.Json;

namespace flyinformation_lab6_
{
    public class FlightInformationSystem
    {
        private List<Flight> flights;

        public FlightInformationSystem()
        {
            flights = new List<Flight>();
        }

        public List<Flight> GetAllFlights()
        {
            return flights;
        }
        public void AddFlight(Flight flight)
        {
            flights.Add(flight);
        }

        public void RemoveFlight(string flightNumber)
        {
            var flightToRemove = flights.Find(f => f.FlightNumber == flightNumber);
            if (flightToRemove != null)
                flights.Remove(flightToRemove);
        }

        public void LoadFlightsFromJson(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("JSON файл не знайдено.");

            string jsonData = File.ReadAllText(filePath);

            var flightData = JsonConvert.DeserializeObject<FlightData>(jsonData);
            flights = flightData?.Flights ?? new List<Flight>();
        }

        public string SerializeFlightsToJson()
        {
            return JsonConvert.SerializeObject(flights);
        }
    }
}
