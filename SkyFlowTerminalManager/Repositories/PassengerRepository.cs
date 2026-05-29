using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using SkyFlowTerminalManager.Interfaces;
using SkyFlowTerminalManager.Models;

namespace SkyFlowTerminalManager.Repositories
{
    public class PassengerRepository : IDataRepository<Passenger>
    {
        private readonly string _connectionString;

        public PassengerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Passenger GetById(int id)
        {
            Passenger passenger = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT PassengerID, UserID, PassportNumber, FirstName, LastName, Nationality, DateOfBirth, ContactInfo FROM Passenger WHERE PassengerID = @PassengerID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PassengerID", id);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        passenger = new Passenger
                        {
                            PassengerID = reader.GetInt32(reader.GetOrdinal("PassengerID")),
                            UserID = reader.IsDBNull(reader.GetOrdinal("UserID")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("UserID")),
                            PassportNumber = reader.GetString(reader.GetOrdinal("PassportNumber")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            Nationality = reader.GetString(reader.GetOrdinal("Nationality")),
                            DateOfBirth = reader.IsDBNull(reader.GetOrdinal("DateOfBirth")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                            ContactInfo = reader.GetString(reader.GetOrdinal("ContactInfo"))
                        };
                    }
                }
            }
            return passenger;
        }

        public IEnumerable<Passenger> GetAll()
        {
            List<Passenger> passengers = new List<Passenger>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT PassengerID, UserID, PassportNumber, FirstName, LastName, Nationality, DateOfBirth, ContactInfo FROM Passenger";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        passengers.Add(new Passenger
                        {
                            PassengerID = reader.GetInt32(reader.GetOrdinal("PassengerID")),
                            UserID = reader.IsDBNull(reader.GetOrdinal("UserID")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("UserID")),
                            PassportNumber = reader.GetString(reader.GetOrdinal("PassportNumber")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            Nationality = reader.GetString(reader.GetOrdinal("Nationality")),
                            DateOfBirth = reader.IsDBNull(reader.GetOrdinal("DateOfBirth")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                            ContactInfo = reader.GetString(reader.GetOrdinal("ContactInfo"))
                        });
                    }
                }
            }
            return passengers;
        }

        public void Add(Passenger passenger)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Passenger (UserID, PassportNumber, FirstName, LastName, Nationality, DateOfBirth, ContactInfo) VALUES (@UserID, @PassportNumber, @FirstName, @LastName, @Nationality, @DateOfBirth, @ContactInfo)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", (object)passenger.UserID ?? DBNull.Value);
                command.Parameters.AddWithValue("@PassportNumber", passenger.PassportNumber);
                command.Parameters.AddWithValue("@FirstName", passenger.FirstName);
                command.Parameters.AddWithValue("@LastName", passenger.LastName);
                command.Parameters.AddWithValue("@Nationality", passenger.Nationality);
                command.Parameters.AddWithValue("@DateOfBirth", (object)passenger.DateOfBirth ?? DBNull.Value);
                command.Parameters.AddWithValue("@ContactInfo", passenger.ContactInfo);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(Passenger passenger)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Passenger SET UserID = @UserID, PassportNumber = @PassportNumber, FirstName = @FirstName, LastName = @LastName, Nationality = @Nationality, DateOfBirth = @DateOfBirth, ContactInfo = @ContactInfo WHERE PassengerID = @PassengerID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", (object)passenger.UserID ?? DBNull.Value);
                command.Parameters.AddWithValue("@PassportNumber", passenger.PassportNumber);
                command.Parameters.AddWithValue("@FirstName", passenger.FirstName);
                command.Parameters.AddWithValue("@LastName", passenger.LastName);
                command.Parameters.AddWithValue("@Nationality", passenger.Nationality);
                command.Parameters.AddWithValue("@DateOfBirth", (object)passenger.DateOfBirth ?? DBNull.Value);
                command.Parameters.AddWithValue("@ContactInfo", passenger.ContactInfo);
                command.Parameters.AddWithValue("@PassengerID", passenger.PassengerID);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Passenger WHERE PassengerID = @PassengerID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PassengerID", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
