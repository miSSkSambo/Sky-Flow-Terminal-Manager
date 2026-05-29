using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using SkyFlowTerminalManager.Interfaces;
using SkyFlowTerminalManager.Models;

namespace SkyFlowTerminalManager.Repositories
{
    public class UserRepository : IDataRepository<User>
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public User GetById(int id)
        {
            User user = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT UserID, Username, PasswordHash, Email, FirstName, LastName, CredentialID, CreatedAt FROM Users WHERE UserID = @UserID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", id);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new User
                        {
                            UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                            Username = reader.GetString(reader.GetOrdinal("Username")),
                            PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            CredentialID = reader.IsDBNull(reader.GetOrdinal("CredentialID")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("CredentialID")),
                            CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
                        };
                    }
                }
            }
            return user;
        }

        public IEnumerable<User> GetAll()
        {
            List<User> users = new List<User>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT UserID, Username, PasswordHash, Email, FirstName, LastName, CredentialID, CreatedAt FROM Users";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new User
                        {
                            UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                            Username = reader.GetString(reader.GetOrdinal("Username")),
                            PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            CredentialID = reader.IsDBNull(reader.GetOrdinal("CredentialID")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("CredentialID")),
                            CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
                        });
                    }
                }
            }
            return users;
        }

        public void Add(User user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Users (Username, PasswordHash, Email, FirstName, LastName, CredentialID, CreatedAt) VALUES (@Username, @PasswordHash, @Email, @FirstName, @LastName, @CredentialID, @CreatedAt)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@FirstName", user.FirstName);
                command.Parameters.AddWithValue("@LastName", user.LastName);
                command.Parameters.AddWithValue("@CredentialID", (object)user.CredentialID ?? DBNull.Value);
                command.Parameters.AddWithValue("@CreatedAt", user.CreatedAt);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(User user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Users SET Username = @Username, PasswordHash = @PasswordHash, Email = @Email, FirstName = @FirstName, LastName = @LastName, CredentialID = @CredentialID WHERE UserID = @UserID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@FirstName", user.FirstName);
                command.Parameters.AddWithValue("@LastName", user.LastName);
                command.Parameters.AddWithValue("@CredentialID", (object)user.CredentialID ?? DBNull.Value);
                command.Parameters.AddWithValue("@UserID", user.UserID);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Users WHERE UserID = @UserID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
