using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using SkyFlowTerminalManager.Models;
using SkyFlowTerminalManager.Repositories;

namespace SkyFlowTerminalManager
{
    class Program
    {
        private static string _connectionString;
        private static UserRepository _userRepository;
        private static FlightRepository _flightRepository;
        private static PassengerRepository _passengerRepository;
        private static BookingRepository _bookingRepository;
        private static AircraftRepository _aircraftRepository;
        private static AirportRepository _airportRepository;

        static void Main(string[] args)
        {
            Console.WriteLine("SkyFlow > Welcome to SkyFlow Terminal Manager");

            // Database Connection Setup
            Console.WriteLine("SkyFlow > Setting up database connection...");
            // In a real application, these would come from configuration (e.g., appsettings.json)
            DatabaseHelper.SetConnectionString("localhost", "SkyFlowDB", "SA", "YourStrong@Password"); // REPLACE WITH YOUR SQL SERVER CREDENTIALS
            _connectionString = DatabaseHelper.ConnectionString;

            if (!DatabaseHelper.TestConnection())
            {
                Console.WriteLine("SkyFlow > Failed to connect to the database. Please check your connection string and ensure the SQL Server is running.");
                Console.WriteLine("SkyFlow > Exiting application.");
                return;
            }
            Console.WriteLine("SkyFlow > Database connection successful.");

            // Initialize Repositories
            _userRepository = new UserRepository(_connectionString);
            _flightRepository = new FlightRepository(_connectionString);
            _passengerRepository = new PassengerRepository(_connectionString);
            _bookingRepository = new BookingRepository(_connectionString);
            _aircraftRepository = new AircraftRepository(_connectionString);
            _airportRepository = new AirportRepository(_connectionString);

            // Initial data setup (optional, for testing)
            // Ensure your SQL script is run against the database first.
            // Hash initial passwords if not already done in SQL script
            // var adminUser = _userRepository.GetById(1); // Assuming admin is UserID 1
            // if (adminUser != null && adminUser.PasswordHash == "hashed_admin_password")
            // {
            //     adminUser.PasswordHash = HashPassword("adminpass"); // Example password
            //     _userRepository.Update(adminUser);
            // }

            // var gateAgentUser = _userRepository.GetById(2); // Assuming gateagent is UserID 2
            // if (gateAgentUser != null && gateAgentUser.PasswordHash == "hashed_gateagent_password")
            // {
            //     gateAgentUser.PasswordHash = HashPassword("agentpass"); // Example password
            //     _userRepository.Update(gateAgentUser);
            // }

            AuthenticateUser();
        }

        static void AuthenticateUser()
        {
            BaseUser currentUser = null;
            while (currentUser == null)
            {
                Console.WriteLine("SkyFlow > Please enter your username:");
                string username = Console.ReadLine();

                Console.WriteLine("SkyFlow > Please enter your password:");
                string password = ReadPassword();

                User user = _userRepository.GetAll().FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));

                if (user != null)
                {
                    Console.WriteLine("Authentication successful.");
                    Console.WriteLine($"Role: {(Role)user.CredentialID}");

                    switch ((Role)user.CredentialID)
                    {
                        case Role.Admin:
                            currentUser = new Admin
                            {
                                UserID = user.UserID,
                                Username = user.Username,
                                FirstName = user.FirstName,
                                LastName = user.LastName
                            };
                            AdminDashboard((Admin)currentUser);
                            break;
                        case Role.GateAgent:
                            currentUser = new GateAgent
                            {
                                UserID = user.UserID,
                                Username = user.Username,
                                FirstName = user.FirstName,
                                LastName = user.LastName
                            };
                            GateAgentDashboard((GateAgent)currentUser);
                            break;
                        default:
                            Console.WriteLine("SkyFlow > Unknown role. Access denied.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("SkyFlow > Invalid username or password. Please try again.");
                }
            }
        }

        static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                // Ignore any key that is not a character or backspace
                if (char.IsLetterOrDigit(key.KeyChar) || char.IsSymbol(key.KeyChar) || char.IsPunctuation(key.KeyChar))
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b");
                }
            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return password;
        }

        public static string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static bool VerifyPassword(string password, string storedHash)
        {
            string hashedPassword = HashPassword(password);
            return hashedPassword.Equals(storedHash);
        }

        static void AdminDashboard(Admin admin)
        {
            Console.WriteLine($"Welcome, {admin.FirstName} {admin.LastName}!");
            admin.DisplayDashboard();

            bool running = true;
            while (running)
            {
                Console.WriteLine("\nAdmin Dashboard Options:");
                Console.WriteLine("1. Manage Flights");
                Console.WriteLine("2. View System Overview");
                Console.WriteLine("3. Manage Staff");
                Console.WriteLine("4. Logout");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ManageFlights();
                        break;
                    case "2":
                        ViewSystemOverview();
                        break;
                    case "3":
                        ManageStaff();
                        break;
                    case "4":
                        running = false;
                        Console.WriteLine("Logging out...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
            AuthenticateUser(); // Return to login screen after logout
        }

        static void ManageFlights()
        {
            Console.WriteLine("\n--- Manage Flights ---");
            bool managing = true;
            while (managing)
            {
                Console.WriteLine("1. View All Flights");
                Console.WriteLine("2. Add New Flight");
                Console.WriteLine("3. Update Flight Status");
                Console.WriteLine("4. Back to Admin Dashboard");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ViewAllFlights();
                        break;
                    case "2":
                        AddNewFlight();
                        break;
                    case "3":
                        UpdateFlightStatus();
                        break;
                    case "4":
                        managing = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void ViewAllFlights()
        {
            Console.WriteLine("\n--- All Flights ---");
            var flights = _flightRepository.GetAll();
            if (flights.Any())
            {
                ConsoleTableRenderer.RenderTable(flights, "Current Flights");
            }
            else
            {
                Console.WriteLine("No flights available.");
            }
        }

        static void AddNewFlight()
        {
            Console.WriteLine("\n--- Add New Flight ---");
            Console.Write("Flight Number: ");
            string flightNumber = Console.ReadLine();

            Console.Write("Origin Airport Code (e.g., JHB): ");
            string originCode = Console.ReadLine();
            var originAirport = _airportRepository.GetAll().FirstOrDefault(a => a.AirportCode.Equals(originCode, StringComparison.OrdinalIgnoreCase));
            if (originAirport == null)
            {
                Console.WriteLine("Invalid Origin Airport Code.");
                return;
            }

            Console.Write("Destination Airport Code (e.g., CPT): ");
            string destCode = Console.ReadLine();
            var destinationAirport = _airportRepository.GetAll().FirstOrDefault(a => a.AirportCode.Equals(destCode, StringComparison.OrdinalIgnoreCase));
            if (destinationAirport == null)
            {
                Console.WriteLine("Invalid Destination Airport Code.");
                return;
            }

            Console.Write("Departure Time (YYYY-MM-DD HH:MM): ");
            DateTime departureTime;
            if (!DateTime.TryParse(Console.ReadLine(), out departureTime))
            {
                Console.WriteLine("Invalid Departure Time format.");
                return;
            }

            Console.Write("Arrival Time (YYYY-MM-DD HH:MM): ");
            DateTime arrivalTime;
            if (!DateTime.TryParse(Console.ReadLine(), out arrivalTime))
            {
                Console.WriteLine("Invalid Arrival Time format.");
                return;
            }

            Console.Write("Aircraft ID: ");
            int aircraftId;
            if (!int.TryParse(Console.ReadLine(), out aircraftId))
            {
                Console.WriteLine("Invalid Aircraft ID.");
                return;
            }
            var aircraft = _aircraftRepository.GetById(aircraftId);
            if (aircraft == null)
            {
                Console.WriteLine("Aircraft not found.");
                return;
            }

            Flight newFlight = new Flight
            {
                FlightNumber = flightNumber,
                OriginAirportID = originAirport.AirportID,
                DestinationAirportID = destinationAirport.AirportID,
                DepartureTime = departureTime,
                ArrivalTime = arrivalTime,
                AircraftID = aircraftId,
                CurrentOccupancy = 0,
                Status = "Scheduled"
            };

            _flightRepository.Add(newFlight);
            Console.WriteLine($"Flight {flightNumber} added successfully.");
        }

        static void UpdateFlightStatus()
        {
            Console.WriteLine("\n--- Update Flight Status ---");
            Console.Write("Enter Flight Number to update: ");
            string flightNumber = Console.ReadLine();

            var flight = _flightRepository.GetAll().FirstOrDefault(f => f.FlightNumber.Equals(flightNumber, StringComparison.OrdinalIgnoreCase));
            if (flight == null)
            {
                Console.WriteLine("Flight not found.");
                return;
            }

            Console.WriteLine($"Current Status for Flight {flight.FlightNumber}: {flight.Status}");
            Console.Write("Enter new status (e.g., Departed, Arrived, Cancelled): ");
            string newStatus = Console.ReadLine();

            // Basic validation for status
            if (newStatus == "Departed" || newStatus == "Arrived" || newStatus == "Cancelled" || newStatus == "Scheduled")
            {
                flight.Status = newStatus;
                _flightRepository.Update(flight);
                Console.WriteLine($"Flight {flight.FlightNumber} status updated to {newStatus}.");
            }
            else
            {
                Console.WriteLine("Invalid status. Please use \'Departed\', \'Arrived\', \'Cancelled\', or \'Scheduled\'.");
            }
        }

        static void ViewSystemOverview()
        {
            Console.WriteLine("\n--- System Overview ---");
            Console.WriteLine("\n--- All Flights ---");
            ViewAllFlights();

            Console.WriteLine("\n--- All Aircraft ---");
            var aircrafts = _aircraftRepository.GetAll();
            if (aircrafts.Any())
            {
                ConsoleTableRenderer.RenderTable(aircrafts, "Aircraft Fleet");
            }
            else
            {
                Console.WriteLine("No aircraft available.");
            }

            Console.WriteLine("\n--- All Airports ---");
            var airports = _airportRepository.GetAll();
            if (airports.Any())
            {
                ConsoleTableRenderer.RenderTable(airports, "Registered Airports");
            }
            else
            {
                Console.WriteLine("No airports available.");
            }
        }

        static void ManageStaff()
        {
            Console.WriteLine("\n--- Manage Staff ---");
            Console.WriteLine("Functionality to add/remove/update staff (users with GateAgent or Admin roles) would be implemented here.");
            Console.WriteLine("For this assignment, this is a placeholder.");
        }

        static void GateAgentDashboard(GateAgent gateAgent)
        {
            Console.WriteLine($"Welcome, {gateAgent.FirstName} {gateAgent.LastName}!");
            gateAgent.DisplayDashboard();

            bool running = true;
            while (running)
            {
                Console.WriteLine("\nGate Agent Dashboard Options:");
                Console.WriteLine("1. Flight Manifest");
                Console.WriteLine("2. Passenger Check-in");
                Console.WriteLine("3. Boarding Gate");
                Console.WriteLine("4. Logout");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ViewFlightManifest();
                        break;
                    case "2":
                        PassengerCheckIn();
                        break;
                    case "3":
                        BoardingGate();
                        break;
                    case "4":
                        running = false;
                        Console.WriteLine("Logging out...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
            AuthenticateUser(); // Return to login screen after logout
        }

        static void ViewFlightManifest()
        {
            Console.WriteLine("\n--- Flight Manifest ---");
            Console.Write("Enter Flight Number: ");
            string flightNumber = Console.ReadLine();

            var flight = _flightRepository.GetAll().FirstOrDefault(f => f.FlightNumber.Equals(flightNumber, StringComparison.OrdinalIgnoreCase));
            if (flight == null)
            {
                Console.WriteLine("Flight not found.");
                return;
            }

            var bookings = _bookingRepository.GetAll().Where(b => b.FlightID == flight.FlightID).ToList();
            if (bookings.Any())
            {
                var passengersOnFlight = from b in bookings
                                         join p in _passengerRepository.GetAll() on b.PassengerID equals p.PassengerID
                                         select new
                                         {
                                             p.PassportNumber,
                                             p.FirstName,
                                             p.LastName,
                                             b.SeatNumber,
                                             b.BookingStatus
                                         };
                ConsoleTableRenderer.RenderTable(passengersOnFlight, $"Manifest for Flight {flight.FlightNumber}");
            }
            else
            {
                Console.WriteLine($"No passengers booked for Flight {flight.FlightNumber}.");
            }
        }

        static void PassengerCheckIn()
        {
            Console.WriteLine("\n--- Passenger Check-in ---");
            Console.Write("Enter Passenger ID or Passport Number: ");
            string passengerIdentifier = Console.ReadLine();

            Passenger passenger = _passengerRepository.GetAll().FirstOrDefault(p => p.PassportNumber.Equals(passengerIdentifier, StringComparison.OrdinalIgnoreCase) || p.PassengerID.ToString() == passengerIdentifier);

            if (passenger == null)
            {
                Console.WriteLine("Passenger not found.");
                return;
            }

            Console.WriteLine($"Passenger found: {passenger.FirstName} {passenger.LastName} (Passport: {passenger.PassportNumber})");

            var bookings = _bookingRepository.GetAll().Where(b => b.PassengerID == passenger.PassengerID).ToList();
            if (!bookings.Any())
            {
                Console.WriteLine("No bookings found for this passenger.");
                return;
            }

            Console.WriteLine("Current Bookings for this Passenger:");
            ConsoleTableRenderer.RenderTable(bookings, "Passenger Bookings");

            Console.Write("Enter Booking ID to check-in: ");
            int bookingId;
            if (!int.TryParse(Console.ReadLine(), out bookingId))
            {
                Console.WriteLine("Invalid Booking ID.");
                return;
            }

            Booking bookingToUpdate = bookings.FirstOrDefault(b => b.BookingID == bookingId);
            if (bookingToUpdate == null)
            {
                Console.WriteLine("Booking not found for this passenger.");
                return;
            }

            if (bookingToUpdate.BookingStatus == "CheckedIn" || bookingToUpdate.BookingStatus == "Boarded")
            {
                Console.WriteLine($"Passenger already {bookingToUpdate.BookingStatus}.");
                return;
            }

            // Check flight capacity before check-in
            var flight = _flightRepository.GetById(bookingToUpdate.FlightID);
            var aircraft = _aircraftRepository.GetById(flight.AircraftID);

            if (flight.CurrentOccupancy >= aircraft.Capacity)
            {
                Console.WriteLine("Flight is full. Cannot check-in passenger.");
                return;
            }

            Console.WriteLine($"Current status: {bookingToUpdate.BookingStatus}");
            Console.Write("Update status to CheckedIn? (Y/N): ");
            string confirm = Console.ReadLine();

            if (confirm.Equals("Y", StringComparison.OrdinalIgnoreCase))
            {
                bookingToUpdate.BookingStatus = "CheckedIn";
                _bookingRepository.Update(bookingToUpdate);
                flight.CurrentOccupancy++; // Increment occupancy on check-in
                _flightRepository.Update(flight);
                Console.WriteLine("Status updated successfully.");
            }
            else
            {
                Console.WriteLine("Check-in cancelled.");
            }
        }

        static void BoardingGate()
        {
            Console.WriteLine("\n--- Boarding Gate ---");
            Console.Write("Enter Flight Number to finalize boarding: ");
            string flightNumber = Console.ReadLine();

            var flight = _flightRepository.GetAll().FirstOrDefault(f => f.FlightNumber.Equals(flightNumber, StringComparison.OrdinalIgnoreCase));
            if (flight == null)
            {
                Console.WriteLine("Flight not found.");
                return;
            }

            Console.WriteLine($"Current Status for Flight {flight.FlightNumber}: {flight.Status}");
            Console.Write("Finalize flight status to Departed or Arrived? (D/A): ");
            string finalStatusChoice = Console.ReadLine();

            if (finalStatusChoice.Equals("D", StringComparison.OrdinalIgnoreCase))
            {
                if (flight.Status == "CheckedIn" || flight.Status == "Scheduled") // Allow departure if checked-in or scheduled
                {
                    flight.Status = "Departed";
                    _flightRepository.Update(flight);
                    Console.WriteLine($"Flight {flight.FlightNumber} status updated to Departed.");
                }
                else
                {
                    Console.WriteLine("Flight cannot be departed from its current status.");
                }
            }
            else if (finalStatusChoice.Equals("A", StringComparison.OrdinalIgnoreCase))
            {
                if (flight.Status == "Departed") // Only allow arrival if already departed
                {
                    flight.Status = "Arrived";
                    _flightRepository.Update(flight);
                    Console.WriteLine($"Flight {flight.FlightNumber} status updated to Arrived.");
                }
                else
                {
                    Console.WriteLine("Flight cannot be marked as Arrived from its current status.");
                }
            }
            else
            {
                Console.WriteLine("Invalid choice. Status not updated.");
            }
        }
    }
}
