using System;

namespace SkyFlowTerminalManager.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? CredentialID { get; set; } // Nullable if not all users have a credential
        public DateTime CreatedAt { get; set; }
    }
}
