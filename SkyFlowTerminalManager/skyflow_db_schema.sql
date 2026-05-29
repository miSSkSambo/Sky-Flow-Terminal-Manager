-- Create Database
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'SkyFlowDB')
BEGIN
    CREATE DATABASE SkyFlowDB;
END;
GO

USE SkyFlowDB;
GO

-- Drop existing tables if they exist to ensure a clean slate
IF OBJECT_ID('dbo.AuditLog', 'U') IS NOT NULL DROP TABLE dbo.AuditLog;
IF OBJECT_ID('dbo.Notification', 'U') IS NOT NULL DROP TABLE dbo.Notification;
IF OBJECT_ID('dbo.Baggage', 'U') IS NOT NULL DROP TABLE dbo.Baggage;
IF OBJECT_ID('dbo.FlightLog', 'U') IS NOT NULL DROP TABLE dbo.FlightLog;
IF OBJECT_ID('dbo.Crew', 'U') IS NOT NULL DROP TABLE dbo.Crew;
IF OBJECT_ID('dbo.FlightAssignment', 'U') IS NOT NULL DROP TABLE dbo.FlightAssignment;
IF OBJECT_ID('dbo.Booking', 'U') IS NOT NULL DROP TABLE dbo.Booking;
IF OBJECT_ID('dbo.Passenger', 'U') IS NOT NULL DROP TABLE dbo.Passenger;
IF OBJECT_ID('dbo.Flight', 'U') IS NOT NULL DROP TABLE dbo.Flight;
IF OBJECT_ID('dbo.Aircraft', 'U') IS NOT NULL DROP TABLE dbo.Aircraft;
IF OBJECT_ID('dbo.Airport', 'U') IS NOT NULL DROP TABLE dbo.Airport;
IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL DROP TABLE dbo.Users;
GO

-- Create Tables
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Username VARCHAR(50) UNIQUE NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL,
    Email VARCHAR(100) UNIQUE NOT NULL,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    CredentialID INT,
    CreatedAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE Airport (
    AirportID INT PRIMARY KEY IDENTITY(1,1),
    AirportCode VARCHAR(3) UNIQUE NOT NULL,
    Name VARCHAR(100) NOT NULL,
    City VARCHAR(50) NOT NULL,
    Country VARCHAR(50) NOT NULL
);

CREATE TABLE Aircraft (
    AircraftID INT PRIMARY KEY IDENTITY(1,1),
    AircraftType VARCHAR(50) NOT NULL,
    Manufacturer VARCHAR(50) NOT NULL,
    Capacity INT NOT NULL,
    Status VARCHAR(20) NOT NULL -- e.g., 'Active', 'Maintenance'
);

CREATE TABLE Flight (
    FlightID INT PRIMARY KEY IDENTITY(1,1),
    FlightNumber VARCHAR(10) UNIQUE NOT NULL,
    OriginAirportID INT FOREIGN KEY REFERENCES Airport(AirportID),
    DestinationAirportID INT FOREIGN KEY REFERENCES Airport(AirportID),
    DepartureTime DATETIME NOT NULL,
    ArrivalTime DATETIME NOT NULL,
    AircraftID INT FOREIGN KEY REFERENCES Aircraft(AircraftID),
    CurrentOccupancy INT DEFAULT 0,
    Status VARCHAR(20) NOT NULL -- e.g., 'Scheduled', 'Departed', 'Arrived', 'Cancelled'
);

CREATE TABLE Passenger (
    PassengerID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT FOREIGN KEY REFERENCES Users(UserID),
    PassportNumber VARCHAR(20) UNIQUE NOT NULL,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    Nationality VARCHAR(50),
    DateOfBirth DATE,
    ContactInfo VARCHAR(100)
);

CREATE TABLE Booking (
    BookingID INT PRIMARY KEY IDENTITY(1,1),
    FlightID INT FOREIGN KEY REFERENCES Flight(FlightID),
    PassengerID INT FOREIGN KEY REFERENCES Passenger(PassengerID),
    BookingReference VARCHAR(10) UNIQUE NOT NULL,
    BookingDate DATETIME DEFAULT GETDATE(),
    SeatNumber VARCHAR(5),
    BookingStatus VARCHAR(20) NOT NULL -- e.g., 'Confirmed', 'Cancelled', 'CheckedIn', 'Boarded'
);

CREATE TABLE FlightAssignment (
    FlightAssignmentID INT PRIMARY KEY IDENTITY(1,1),
    FlightID INT FOREIGN KEY REFERENCES Flight(FlightID),
    CrewID INT, -- Assuming Crew table will be created later
    AssignedDate DATETIME DEFAULT GETDATE()
);

CREATE TABLE Crew (
    CrewID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT FOREIGN KEY REFERENCES Users(UserID),
    CrewType VARCHAR(50) NOT NULL, -- e.g., 'Pilot', 'Cabin Crew', 'Ground Staff'
    Rank VARCHAR(50),
    YearsExperience INT
);

ALTER TABLE FlightAssignment
ADD CONSTRAINT FK_FlightAssignment_Crew FOREIGN KEY (CrewID) REFERENCES Crew(CrewID);

CREATE TABLE FlightLog (
    LogID INT PRIMARY KEY IDENTITY(1,1),
    FlightID INT FOREIGN KEY REFERENCES Flight(FlightID),
    Action VARCHAR(100) NOT NULL,
    Details TEXT,
    Timestamp DATETIME DEFAULT GETDATE()
);

CREATE TABLE Baggage (
    BaggageID INT PRIMARY KEY IDENTITY(1,1),
    BookingID INT FOREIGN KEY REFERENCES Booking(BookingID),
    Weight DECIMAL(5,2),
    Dimensions VARCHAR(50),
    Status VARCHAR(20) NOT NULL -- e.g., 'Checked-in', 'In Transit', 'Delivered', 'Lost'
);

CREATE TABLE Notification (
    NotificationID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT FOREIGN KEY REFERENCES Users(UserID),
    Type VARCHAR(50) NOT NULL,
    Message TEXT NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    IsRead BIT DEFAULT 0
);

CREATE TABLE AuditLog (
    AuditLogID INT PRIMARY KEY IDENTITY(1,1),
    Action VARCHAR(100) NOT NULL,
    TableName VARCHAR(50),
    RecordID INT,
    OldValues TEXT,
    NewValues TEXT,
    Timestamp DATETIME DEFAULT GETDATE()
);
GO

-- Insert Initial Data
-- Passwords are hashed for 'admin' and 'gateagent'
INSERT INTO Users (Username, PasswordHash, Email, FirstName, LastName, CredentialID)
VALUES
('admin', 'hashed_admin_password', 'admin@skyflow.com', 'Admin', 'User', 1), -- CredentialID 1 for Admin
('gateagent', 'hashed_gateagent_password', 'gateagent@skyflow.com', 'Gate', 'Agent', 2); -- CredentialID 2 for Gate Agent

INSERT INTO Airport (AirportCode, Name, City, Country)
VALUES
('JHB', 'O.R. Tambo International Airport', 'Johannesburg', 'South Africa'),
('CPT', 'Cape Town International Airport', 'Cape Town', 'South Africa'),
('DBN', 'King Shaka International Airport', 'Durban', 'South Africa');

INSERT INTO Aircraft (AircraftType, Manufacturer, Capacity, Status)
VALUES
('Boeing 737', 'Boeing', 180, 'Active'),
('Airbus A320', 'Airbus', 150, 'Active');

INSERT INTO Flight (FlightNumber, OriginAirportID, DestinationAirportID, DepartureTime, ArrivalTime, AircraftID, Status)
VALUES
('SF102', 1, 2, '2026-06-01 08:30:00', '2026-06-01 10:30:00', 1, 'Scheduled'),
('SF221', 3, 1, '2026-06-01 10:15:00', '2026-06-01 12:00:00', 2, 'Scheduled'),
('SF305', 1, 3, '2026-06-01 13:45:00', '2026-06-01 15:30:00', 1, 'Scheduled');

INSERT INTO Passenger (UserID, PassportNumber, FirstName, LastName, Nationality, DateOfBirth, ContactInfo)
VALUES
(NULL, 'PASSPORT12345', 'Neroshen', 'Govender', 'South African', '1990-01-01', 'neroshen@example.com'),
(NULL, 'PASSPORT67890', 'Jane', 'Doe', 'British', '1985-05-10', 'jane.doe@example.com');

INSERT INTO Booking (FlightID, PassengerID, BookingReference, SeatNumber, BookingStatus)
VALUES
(1, 1, 'BK1001', '12A', 'Confirmed'),
(2, 2, 'BK1002', '05C', 'Confirmed');

INSERT INTO Crew (UserID, CrewType, Rank, YearsExperience)
VALUES
(1, 'Pilot', 'Captain', 10),
(2, 'Cabin Crew', 'Flight Attendant', 5);

INSERT INTO FlightAssignment (FlightID, CrewID, AssignedDate)
VALUES
(1, 1, GETDATE()),
(1, 2, GETDATE());

-- Example of a FlightLog entry
INSERT INTO FlightLog (FlightID, Action, Details)
VALUES
(1, 'Flight Scheduled', 'Flight SF102 from JHB to CPT scheduled.');

-- Example of a Baggage entry
INSERT INTO Baggage (BookingID, Weight, Dimensions, Status)
VALUES
(1, 20.5, '50x30x20', 'Checked-in');

-- Example of a Notification entry
INSERT INTO Notification (UserID, Type, Message)
VALUES
(1, 'System Alert', 'New flight SF102 has been scheduled.');

-- Example of an AuditLog entry
INSERT INTO AuditLog (Action, TableName, RecordID, OldValues, NewValues)
VALUES
('INSERT', 'Flight', 1, NULL, 'FlightNumber: SF102, Status: Scheduled');
