using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using SkyFlowTerminalManager.Interfaces;
using SkyFlowTerminalManager.Models;

namespace SkyFlowTerminalManager.Repositories
{
    public class BookingRepository : IDataRepository<Booking>
    {
        private readonly string _connectionString;

        public BookingRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Booking GetById(int id)
        {
            Booking booking = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT BookingID, FlightID, PassengerID, BookingReference, BookingDate, SeatNumber, BookingStatus FROM Booking WHERE BookingID = @BookingID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@BookingID", id);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        booking = new Booking
                        {
                            BookingID = reader.GetInt32(reader.GetOrdinal("BookingID")),
                            FlightID = reader.GetInt32(reader.GetOrdinal("FlightID")),
                            PassengerID = reader.GetInt32(reader.GetOrdinal("PassengerID")),
                            BookingReference = reader.GetString(reader.GetOrdinal("BookingReference")),
                            BookingDate = reader.GetDateTime(reader.GetOrdinal("BookingDate")),
                            SeatNumber = reader.GetString(reader.GetOrdinal("SeatNumber")),
                            BookingStatus = reader.GetString(reader.GetOrdinal("BookingStatus"))
                        };
                    }
                }
            }
            return booking;
        }

        public IEnumerable<Booking> GetAll()
        {
            List<Booking> bookings = new List<Booking>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT BookingID, FlightID, PassengerID, BookingReference, BookingDate, SeatNumber, BookingStatus FROM Booking";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        bookings.Add(new Booking
                        {
                            BookingID = reader.GetInt32(reader.GetOrdinal("BookingID")),
                            FlightID = reader.GetInt32(reader.GetOrdinal("FlightID")),
                            PassengerID = reader.GetInt32(reader.GetOrdinal("PassengerID")),
                            BookingReference = reader.GetString(reader.GetOrdinal("BookingReference")),
                            BookingDate = reader.GetDateTime(reader.GetOrdinal("BookingDate")),
                            SeatNumber = reader.GetString(reader.GetOrdinal("SeatNumber")),
                            BookingStatus = reader.GetString(reader.GetOrdinal("BookingStatus"))
                        });
                    }
                }
            }
            return bookings;
        }

        public void Add(Booking booking)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Booking (FlightID, PassengerID, BookingReference, BookingDate, SeatNumber, BookingStatus) VALUES (@FlightID, @PassengerID, @BookingReference, @BookingDate, @SeatNumber, @BookingStatus)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FlightID", booking.FlightID);
                command.Parameters.AddWithValue("@PassengerID", booking.PassengerID);
                command.Parameters.AddWithValue("@BookingReference", booking.BookingReference);
                command.Parameters.AddWithValue("@BookingDate", booking.BookingDate);
                command.Parameters.AddWithValue("@SeatNumber", booking.SeatNumber);
                command.Parameters.AddWithValue("@BookingStatus", booking.BookingStatus);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(Booking booking)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Booking SET FlightID = @FlightID, PassengerID = @PassengerID, BookingReference = @BookingReference, BookingDate = @BookingDate, SeatNumber = @SeatNumber, BookingStatus = @BookingStatus WHERE BookingID = @BookingID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FlightID", booking.FlightID);
                command.Parameters.AddWithValue("@PassengerID", booking.PassengerID);
                command.Parameters.AddWithValue("@BookingReference", booking.BookingReference);
                command.Parameters.AddWithValue("@BookingDate", booking.BookingDate);
                command.Parameters.AddWithValue("@SeatNumber", booking.SeatNumber);
                command.Parameters.AddWithValue("@BookingStatus", booking.BookingStatus);
                command.Parameters.AddWithValue("@BookingID", booking.BookingID);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Booking WHERE BookingID = @BookingID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@BookingID", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
