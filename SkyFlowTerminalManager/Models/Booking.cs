using System;

namespace SkyFlowTerminalManager.Models
{
    public class Booking
    {
        public int BookingID { get; set; }
        public int FlightID { get; set; }
        public int PassengerID { get; set; }
        public string BookingReference { get; set; }
        public DateTime BookingDate { get; set; }
        public string SeatNumber { get; set; }
        public string BookingStatus { get; set; } // e.g., 'Confirmed', 'Cancelled', 'CheckedIn', 'Boarded'
    }
}
