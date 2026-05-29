using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using SkyFlowTerminalManager.Interfaces;
using SkyFlowTerminalManager.Models;

namespace SkyFlowTerminalManager.Repositories
{
    public class FlightRepository : IDataRepository<Flight>
    {
        private readonly string _connectionString;

        public FlightRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Flight GetById(int id)
        {
            Flight flight = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT FlightID, FlightNumber, OriginAirportID, DestinationAirportID, DepartureTime, ArrivalTime, AircraftID, CurrentOccupancy, Status FROM Flight WHERE FlightID = @FlightID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FlightID", id);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        flight = new Flight
                        {
                            FlightID = reader.GetInt32(reader.GetOrdinal("FlightID")),
                            FlightNumber = reader.GetString(reader.GetOrdinal("FlightNumber")),
                            OriginAirportID = reader.GetInt32(reader.GetOrdinal("OriginAirportID")),
                            DestinationAirportID = reader.GetInt32(reader.GetOrdinal("DestinationAirportID")),
                            DepartureTime = reader.GetDateTime(reader.GetOrdinal("DepartureTime")),
                            ArrivalTime = reader.GetDateTime(reader.GetOrdinal("ArrivalTime")),
                            AircraftID = reader.GetInt32(reader.GetOrdinal("AircraftID")),
                            CurrentOccupancy = reader.GetInt32(reader.GetOrdinal("CurrentOccupancy")),
                            Status = reader.GetString(reader.GetOrdinal("Status"))
                        };
                    }
                }
            }
            return flight;
        }

        public IEnumerable<Flight> GetAll()
        {
            List<Flight> flights = new List<Flight>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT FlightID, FlightNumber, OriginAirportID, DestinationAirportID, DepartureTime, ArrivalTime, AircraftID, CurrentOccupancy, Status FROM Flight";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        flights.Add(new Flight
                        {
                            FlightID = reader.GetInt32(reader.GetOrdinal("FlightID")),
                            FlightNumber = reader.GetString(reader.GetOrdinal("FlightNumber")),
                            OriginAirportID = reader.GetInt32(reader.GetOrdinal("OriginAirportID")),
                            DestinationAirportID = reader.GetInt32(reader.GetOrdinal("DestinationAirportID")),
                            DepartureTime = reader.GetDateTime(reader.GetOrdinal("DepartureTime")),
                            ArrivalTime = reader.GetDateTime(reader.GetOrdinal("ArrivalTime")),
                            AircraftID = reader.GetInt32(reader.GetOrdinal("AircraftID")),
                            CurrentOccupancy = reader.GetInt32(reader.GetOrdinal("CurrentOccupancy")),
                            Status = reader.GetString(reader.GetOrdinal("Status"))
                        });
                    }
                }
            }
            return flights;
        }

        public void Add(Flight flight)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Flight (FlightNumber, OriginAirportID, DestinationAirportID, DepartureTime, ArrivalTime, AircraftID, CurrentOccupancy, Status) VALUES (@FlightNumber, @OriginAirportID, @DestinationAirportID, @DepartureTime, @ArrivalTime, @AircraftID, @CurrentOccupancy, @Status)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FlightNumber", flight.FlightNumber);
                command.Parameters.AddWithValue("@OriginAirportID", flight.OriginAirportID);
                command.Parameters.AddWithValue("@DestinationAirportID", flight.DestinationAirportID);
                command.Parameters.AddWithValue("@DepartureTime", flight.DepartureTime);
                command.Parameters.AddWithValue("@ArrivalTime", flight.ArrivalTime);
                command.Parameters.AddWithValue("@AircraftID", flight.AircraftID);
                command.Parameters.AddWithValue("@CurrentOccupancy", flight.CurrentOccupancy);
                command.Parameters.AddWithValue("@Status", flight.Status);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(Flight flight)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Flight SET FlightNumber = @FlightNumber, OriginAirportID = @OriginAirportID, DestinationAirportID = @DestinationAirportID, DepartureTime = @DepartureTime, ArrivalTime = @ArrivalTime, AircraftID = @AircraftID, CurrentOccupancy = @CurrentOccupancy, Status = @Status WHERE FlightID = @FlightID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FlightNumber", flight.FlightNumber);
                command.Parameters.AddWithValue("@OriginAirportID", flight.OriginAirportID);
                command.Parameters.AddWithValue("@DestinationAirportID", flight.DestinationAirportID);
                command.Parameters.AddWithValue("@DepartureTime", flight.DepartureTime);
                command.Parameters.AddWithValue("@ArrivalTime", flight.ArrivalTime);
                command.Parameters.AddWithValue("@AircraftID", flight.AircraftID);
                command.Parameters.AddWithValue("@CurrentOccupancy", flight.CurrentOccupancy);
                command.Parameters.AddWithValue("@Status", flight.Status);
                command.Parameters.AddWithValue("@FlightID", flight.FlightID);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Flight WHERE FlightID = @FlightID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FlightID", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
