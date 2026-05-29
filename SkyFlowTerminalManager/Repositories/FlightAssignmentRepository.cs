using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using SkyFlowTerminalManager.Interfaces;
using SkyFlowTerminalManager.Models;

namespace SkyFlowTerminalManager.Repositories
{
    public class FlightAssignmentRepository : IDataRepository<FlightAssignment>
    {
        private readonly string _connectionString;

        public FlightAssignmentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public FlightAssignment GetById(int id)
        {
            FlightAssignment flightAssignment = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT FlightAssignmentID, FlightID, CrewID, AssignedDate FROM FlightAssignment WHERE FlightAssignmentID = @FlightAssignmentID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FlightAssignmentID", id);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        flightAssignment = new FlightAssignment
                        {
                            FlightAssignmentID = reader.GetInt32(reader.GetOrdinal("FlightAssignmentID")),
                            FlightID = reader.GetInt32(reader.GetOrdinal("FlightID")),
                            CrewID = reader.GetInt32(reader.GetOrdinal("CrewID")),
                            AssignedDate = reader.GetDateTime(reader.GetOrdinal("AssignedDate"))
                        };
                    }
                }
            }
            return flightAssignment;
        }

        public IEnumerable<FlightAssignment> GetAll()
        {
            List<FlightAssignment> flightAssignments = new List<FlightAssignment>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT FlightAssignmentID, FlightID, CrewID, AssignedDate FROM FlightAssignment";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        flightAssignments.Add(new FlightAssignment
                        {
                            FlightAssignmentID = reader.GetInt32(reader.GetOrdinal("FlightAssignmentID")),
                            FlightID = reader.GetInt32(reader.GetOrdinal("FlightID")),
                            CrewID = reader.GetInt32(reader.GetOrdinal("CrewID")),
                            AssignedDate = reader.GetDateTime(reader.GetOrdinal("AssignedDate"))
                        });
                    }
                }
            }
            return flightAssignments;
        }

        public void Add(FlightAssignment flightAssignment)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO FlightAssignment (FlightID, CrewID, AssignedDate) VALUES (@FlightID, @CrewID, @AssignedDate)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FlightID", flightAssignment.FlightID);
                command.Parameters.AddWithValue("@CrewID", flightAssignment.CrewID);
                command.Parameters.AddWithValue("@AssignedDate", flightAssignment.AssignedDate);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(FlightAssignment flightAssignment)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE FlightAssignment SET FlightID = @FlightID, CrewID = @CrewID, AssignedDate = @AssignedDate WHERE FlightAssignmentID = @FlightAssignmentID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FlightID", flightAssignment.FlightID);
                command.Parameters.AddWithValue("@CrewID", flightAssignment.CrewID);
                command.Parameters.AddWithValue("@AssignedDate", flightAssignment.AssignedDate);
                command.Parameters.AddWithValue("@FlightAssignmentID", flightAssignment.FlightAssignmentID);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM FlightAssignment WHERE FlightAssignmentID = @FlightAssignmentID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FlightAssignmentID", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
