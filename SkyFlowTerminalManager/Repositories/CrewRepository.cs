using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using SkyFlowTerminalManager.Interfaces;
using SkyFlowTerminalManager.Models;

namespace SkyFlowTerminalManager.Repositories
{
    public class CrewRepository : IDataRepository<Crew>
    {
        private readonly string _connectionString;

        public CrewRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Crew GetById(int id)
        {
            Crew crew = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT CrewID, UserID, CrewType, Rank, YearsExperience FROM Crew WHERE CrewID = @CrewID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CrewID", id);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        crew = new Crew
                        {
                            CrewID = reader.GetInt32(reader.GetOrdinal("CrewID")),
                            UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                            CrewType = reader.GetString(reader.GetOrdinal("CrewType")),
                            Rank = reader.GetString(reader.GetOrdinal("Rank")),
                            YearsExperience = reader.GetInt32(reader.GetOrdinal("YearsExperience"))
                        };
                    }
                }
            }
            return crew;
        }

        public IEnumerable<Crew> GetAll()
        {
            List<Crew> crews = new List<Crew>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT CrewID, UserID, CrewType, Rank, YearsExperience FROM Crew";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        crews.Add(new Crew
                        {
                            CrewID = reader.GetInt32(reader.GetOrdinal("CrewID")),
                            UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                            CrewType = reader.GetString(reader.GetOrdinal("CrewType")),
                            Rank = reader.GetString(reader.GetOrdinal("Rank")),
                            YearsExperience = reader.GetInt32(reader.GetOrdinal("YearsExperience"))
                        });
                    }
                }
            }
            return crews;
        }

        public void Add(Crew crew)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Crew (UserID, CrewType, Rank, YearsExperience) VALUES (@UserID, @CrewType, @Rank, @YearsExperience)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", crew.UserID);
                command.Parameters.AddWithValue("@CrewType", crew.CrewType);
                command.Parameters.AddWithValue("@Rank", crew.Rank);
                command.Parameters.AddWithValue("@YearsExperience", crew.YearsExperience);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(Crew crew)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Crew SET UserID = @UserID, CrewType = @CrewType, Rank = @Rank, YearsExperience = @YearsExperience WHERE CrewID = @CrewID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", crew.UserID);
                command.Parameters.AddWithValue("@CrewType", crew.CrewType);
                command.Parameters.AddWithValue("@Rank", crew.Rank);
                command.Parameters.AddWithValue("@YearsExperience", crew.YearsExperience);
                command.Parameters.AddWithValue("@CrewID", crew.CrewID);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Crew WHERE CrewID = @CrewID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CrewID", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
