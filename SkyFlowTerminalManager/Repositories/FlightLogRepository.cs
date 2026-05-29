using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using SkyFlowTerminalManager.Interfaces;
using SkyFlowTerminalManager.Models;

namespace SkyFlowTerminalManager.Repositories
{
    public class FlightLogRepository : IDataRepository<FlightLog>
    {
        private readonly string _connectionString;

        public FlightLogRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public FlightLog GetById(int id)
        {
            FlightLog flightLog = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT LogID, FlightID, Action, Details, Timestamp FROM FlightLog WHERE LogID = @LogID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LogID", id);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        flightLog = new FlightLog
                        {
                            LogID = reader.GetInt32(reader.GetOrdinal("LogID")),
                            FlightID = reader.GetInt32(reader.GetOrdinal("FlightID")),
                            Action = reader.GetString(reader.GetOrdinal("Action")),
                            Details = reader.GetString(reader.GetOrdinal("Details")),
                            Timestamp = reader.GetDateTime(reader.GetOrdinal("Timestamp"))
                        };
                    }
                }
            }
            return flightLog;
        }

        public IEnumerable<FlightLog> GetAll()
        {
            List<FlightLog> flightLogs = new List<FlightLog>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT LogID, FlightID, Action, Details, Timestamp FROM FlightLog";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        flightLogs.Add(new FlightLog
                        {
                            LogID = reader.GetInt32(reader.GetOrdinal("LogID")),
                            FlightID = reader.GetInt32(reader.GetOrdinal("FlightID")),
                            Action = reader.GetString(reader.GetOrdinal("Action")),
                            Details = reader.GetString(reader.GetOrdinal("Details")),
                            Timestamp = reader.GetDateTime(reader.GetOrdinal("Timestamp"))
                        });
                    }
                }
            }
            return flightLogs;
        }

        public void Add(FlightLog flightLog)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO FlightLog (FlightID, Action, Details, Timestamp) VALUES (@FlightID, @Action, @Details, @Timestamp)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FlightID", flightLog.FlightID);
                command.Parameters.AddWithValue("@Action", flightLog.Action);
                command.Parameters.AddWithValue("@Details", flightLog.Details);
                command.Parameters.AddWithValue("@Timestamp", flightLog.Timestamp);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(FlightLog flightLog)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE FlightLog SET FlightID = @FlightID, Action = @Action, Details = @Details, Timestamp = @Timestamp WHERE LogID = @LogID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FlightID", flightLog.FlightID);
                command.Parameters.AddWithValue("@Action", flightLog.Action);
                command.Parameters.AddWithValue("@Details", flightLog.Details);
                command.Parameters.AddWithValue("@Timestamp", flightLog.Timestamp);
                command.Parameters.AddWithValue("@LogID", flightLog.LogID);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM FlightLog WHERE LogID = @LogID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LogID", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
