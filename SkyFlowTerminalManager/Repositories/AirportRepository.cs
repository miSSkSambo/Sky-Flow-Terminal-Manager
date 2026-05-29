using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using SkyFlowTerminalManager.Interfaces;
using SkyFlowTerminalManager.Models;

namespace SkyFlowTerminalManager.Repositories
{
    public class AirportRepository : IDataRepository<Airport>
    {
        private readonly string _connectionString;

        public AirportRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Airport GetById(int id)
        {
            Airport airport = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT AirportID, AirportCode, Name, City, Country FROM Airport WHERE AirportID = @AirportID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AirportID", id);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        airport = new Airport
                        {
                            AirportID = reader.GetInt32(reader.GetOrdinal("AirportID")),
                            AirportCode = reader.GetString(reader.GetOrdinal("AirportCode")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            City = reader.GetString(reader.GetOrdinal("City")),
                            Country = reader.GetString(reader.GetOrdinal("Country"))
                        };
                    }
                }
            }
            return airport;
        }

        public IEnumerable<Airport> GetAll()
        {
            List<Airport> airports = new List<Airport>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT AirportID, AirportCode, Name, City, Country FROM Airport";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        airports.Add(new Airport
                        {
                            AirportID = reader.GetInt32(reader.GetOrdinal("AirportID")),
                            AirportCode = reader.GetString(reader.GetOrdinal("AirportCode")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            City = reader.GetString(reader.GetOrdinal("City")),
                            Country = reader.GetString(reader.GetOrdinal("Country"))
                        });
                    }
                }
            }
            return airports;
        }

        public void Add(Airport airport)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Airport (AirportCode, Name, City, Country) VALUES (@AirportCode, @Name, @City, @Country)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AirportCode", airport.AirportCode);
                command.Parameters.AddWithValue("@Name", airport.Name);
                command.Parameters.AddWithValue("@City", airport.City);
                command.Parameters.AddWithValue("@Country", airport.Country);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(Airport airport)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Airport SET AirportCode = @AirportCode, Name = @Name, City = @City, Country = @Country WHERE AirportID = @AirportID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AirportCode", airport.AirportCode);
                command.Parameters.AddWithValue("@Name", airport.Name);
                command.Parameters.AddWithValue("@City", airport.City);
                command.Parameters.AddWithValue("@Country", airport.Country);
                command.Parameters.AddWithValue("@AirportID", airport.AirportID);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Airport WHERE AirportID = @AirportID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AirportID", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
