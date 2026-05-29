using System;

namespace SkyFlowTerminalManager.Models
{
    public class FlightLog
    {
        public int LogID { get; set; }
        public int FlightID { get; set; }
        public string Action { get; set; }
        public string Details { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
