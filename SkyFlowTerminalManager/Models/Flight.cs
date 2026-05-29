using System;

namespace SkyFlowTerminalManager.Models
{
    public class Flight
    {
        public int FlightID { get; set; }
        public string FlightNumber { get; set; }
        public int OriginAirportID { get; set; }
        public int DestinationAirportID { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public int AircraftID { get; set; }
        public int CurrentOccupancy { get; set; }
        public string Status { get; set; }

        // Navigation properties (optional, depending on ORM/ADO.NET usage)
        // public Airport OriginAirport { get; set; }
        // public Airport DestinationAirport { get; set; }
        // public Aircraft Aircraft { get; set; }
    }
}
