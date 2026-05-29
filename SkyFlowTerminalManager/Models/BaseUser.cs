using System;

namespace SkyFlowTerminalManager.Models
{
    public abstract class BaseUser
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? CredentialID { get; set; }
        public DateTime CreatedAt { get; set; }

        public abstract void DisplayDashboard();
    }
}
