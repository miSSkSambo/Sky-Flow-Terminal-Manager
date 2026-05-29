using System;

namespace SkyFlowTerminalManager.Models
{
    public class Passenger
    {
        public int PassengerID { get; set; }
        public int? UserID { get; set; } // Nullable, as not all passengers might be registered users
        public string PassportNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nationality { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string ContactInfo { get; set; }
    }
}
