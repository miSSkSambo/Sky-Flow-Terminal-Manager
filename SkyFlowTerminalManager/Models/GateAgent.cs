using System;

namespace SkyFlowTerminalManager.Models
{
    public class GateAgent : BaseUser
    {
        public override void DisplayDashboard()
        {
            Console.WriteLine("Gate Agent Dashboard > 1. Flight Manifest");
            Console.WriteLine("Gate Agent Dashboard > 2. Passenger Check-in");
            Console.WriteLine("Gate Agent Dashboard > 3. Boarding Gate");
        }
    }
}
