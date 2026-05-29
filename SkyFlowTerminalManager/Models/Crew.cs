using System;

namespace SkyFlowTerminalManager.Models
{
    public class Crew
    {
        public int CrewID { get; set; }
        public int UserID { get; set; }
        public string CrewType { get; set; }
        public string Rank { get; set; }
        public int YearsExperience { get; set; }
    }
}
