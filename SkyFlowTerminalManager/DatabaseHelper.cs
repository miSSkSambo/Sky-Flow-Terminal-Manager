using Microsoft.Data.SqlClient;
using System;

namespace SkyFlowTerminalManager
{
    public class DatabaseHelper
    {
        public static string ConnectionString { get; private set; }

        public static void SetConnectionString(string server, string database, string userId, string password)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = server; 
            builder.InitialCatalog = database; 
            builder.UserID = userId;            
            builder.Password = password;  
            builder.TrustServerCertificate = true; // For local development with self-signed certs
            ConnectionString = builder.ConnectionString;
        }

        public static bool TestConnection()
        {
            if (string.IsNullOrEmpty(ConnectionString))
            {
                Console.WriteLine("Database connection string is not set.");
                return false;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database connection failed: {ex.Message}");
                return false;
            }
        }
    }
}
