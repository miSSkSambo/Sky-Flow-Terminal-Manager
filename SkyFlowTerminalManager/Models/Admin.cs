using System;

namespace SkyFlowTerminalManager.Models
{
    public class Admin : BaseUser
    {
        public override void DisplayDashboard()
        {
            Console.WriteLine("Admin Dashboard > 1. Manage Flights");
            Console.WriteLine("Admin Dashboard > 2. View System Overview");
            Console.WriteLine("Admin Dashboard > 3. Manage Staff");
        }
    }
}
