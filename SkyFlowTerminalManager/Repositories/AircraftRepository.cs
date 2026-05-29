using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using SkyFlowTerminalManager.Interfaces;
using SkyFlowTerminalManager.Models;

namespace SkyFlowTerminalManager.Repositories
{
    public class AircraftRepository : IDataRepository<Aircraft>
    {
        private readonly string _connectionString;

        public AircraftRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Aircraft GetById(int id)
        {
            Aircraft aircraft = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT AircraftID, AircraftType, Manufacturer, Capacity, Status FROM Aircraft WHERE AircraftID = @AircraftID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AircraftID", id);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        aircraft = new Aircraft
                        {
                            AircraftID = reader.GetInt32(reader.GetOrdinal("AircraftID")),
                            AircraftType = reader.GetString(reader.GetOrdinal("AircraftType")),
                            Manufacturer = reader.GetString(reader.GetOrdinal("Manufacturer")),
                            Capacity = reader.GetInt32(reader.GetOrdinal("Capacity")),
                            Status = reader.GetString(reader.GetOrdinal("Status"))
                        };
                    }
                }
            }
            return aircraft;
        }

        public IEnumerable<Aircraft> GetAll()
        {
            List<Aircraft> aircrafts = new List<Aircraft>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT AircraftID, AircraftType, Manufacturer, Capacity, Status FROM Aircraft";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        aircrafts.Add(new Aircraft
                        {
                            AircraftID = reader.GetInt32(reader.GetOrdinal("AircraftID")),
                            AircraftType = reader.GetString(reader.GetOrdinal("AircraftType")),
                            Manufacturer = reader.GetString(reader.GetOrdinal("Manufacturer")),
                            Capacity = reader.GetInt32(reader.GetOrdinal("Capacity")),
                            Status = reader.GetString(reader.GetOrdinal("Status"))
                        });
                    }
                }
            }
            return aircrafts;
        }

        public void Add(Aircraft aircraft)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Aircraft (AircraftType, Manufacturer, Capacity, Status) VALUES (@AircraftType, @Manufacturer, @Capacity, @Status)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AircraftType", aircraft.AircraftType);
                command.Parameters.AddWithValue("@Manufacturer", aircraft.Manufacturer);
                command.Parameters.AddWithValue("@Capacity", aircraft.Capacity);
                command.Parameters.AddWithValue("@Status", aircraft.Status);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(Aircraft aircraft)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Aircraft SET AircraftType = @AircraftType, Manufacturer = @Manufacturer, Capacity = @Capacity, Status = @Status WHERE AircraftID = @AircraftID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AircraftType", aircraft.AircraftType);
                command.Parameters.AddWithValue("@Manufacturer", aircraft.Manufacturer);
                command.Parameters.AddWithValue("@Capacity", aircraft.Capacity);
                command.Parameters.AddWithValue("@Status", aircraft.Status);
                command.Parameters.AddWithValue("@AircraftID", aircraft.AircraftID);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Aircraft WHERE AircraftID = @AircraftID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AircraftID", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
