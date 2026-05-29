using System;

namespace SkyFlowTerminalManager.Models
{
    public class FlightAssignment
    {
        public int FlightAssignmentID { get; set; }
        public int FlightID { get; set; }
        public int CrewID { get; set; }
        public DateTime AssignedDate { get; set; }
    }
}
