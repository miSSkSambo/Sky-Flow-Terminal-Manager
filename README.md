# SkyFlow Terminal Manager

## Overview

SkyFlow Terminal Manager is a console-based Airport and Airline Management System developed in C# using .NET 8 and Microsoft SQL Server. The system provides role-based access control for airport personnel and supports flight management, passenger processing, boarding operations, and database-driven record management.

The project demonstrates Object-Oriented Programming (OOP), SQL Server integration, repository patterns, and business rule validation.

## Features

### Administrator
- Manage Flights
- View System Overview
- Manage Staff
- Update Flight Statuses

### Gate Agent
- View Flight Manifests
- Passenger Check-In
- Boarding Gate Operations
- Flight Departure Processing

## Database

Database Name: **SkyFlowDB**

Main Tables:
- Users
- Airport
- Aircraft
- Flight
- Passenger
- Booking
- FlightAssignment
- Crew
- FlightLog
- Baggage
- Notification
- AuditLog

## Technologies Used

- C#
- .NET 8
- Microsoft SQL Server 2022
- ADO.NET
- Docker
- Ubuntu WSL / Visual Studio

## Installation

### Restore Packages

```bash
dotnet restore
```

### Build Project

```bash
dotnet build
```

### Run Application

```bash
dotnet run
```

## Database Setup

Create SQL Server container:

```bash
docker run -e "ACCEPT_EULA=Y" \
-e "MSSQL_SA_PASSWORD=YourStrong@Password" \
-p 1433:1433 \
--name skyflow-sql \
-d mcr.microsoft.com/mssql/server:2022-latest
```

Import schema:

```bash
docker cp skyflow_db_schema.sql skyflow-sql:/tmp/skyflow_db_schema.sql
```

## Test Accounts

### Administrator
Username: admin

### Gate Agent
Username: gateagent

## Project Structure

```text
SkyFlowTerminalManager
│
├── Models
├── Interfaces
├── Repositories
├── Program.cs
├── DatabaseHelper.cs
├── ConsoleTableRenderer.cs
├── skyflow_db_schema.sql
└── README.md
```

## Testing Completed

- Authentication
- Admin Dashboard
- Gate Agent Dashboard
- Flight Management
- Add Flight
- Update Flight Status
- Flight Manifest
- Passenger Check-In
- Boarding Gate Processing
- SQL Server Persistence

## Conclusion

SkyFlow Terminal Manager successfully demonstrates airport operations management using a role-based architecture, SQL Server persistence, and Object-Oriented Programming principles.
