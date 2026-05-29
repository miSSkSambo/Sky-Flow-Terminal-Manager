# SkyFlow Terminal Manager Guide/steps


A console-based Airport and Airline Management System developed in **C# (.NET 8)** with **SQL Server** running in Docker on Ubuntu/WSL.

---

# Project Overview

SkyFlow Terminal Manager simulates an airport terminal management system that allows:

* Administrator Login
* Flight Management
* Passenger Management
* Aircraft Management
* Airport Management
* Flight Status Updates
* Passenger Check-In
* Boarding Gate Operations
* SQL Server Data Persistence
* Console Data Visualization

---

# Technologies Used

* C#
* .NET 8
* SQL Server 2022
* Docker
* Ubuntu (WSL)
* ADO.NET
* GitHub

---

# Prerequisites

Before running the project, install:

## Ubuntu Packages

```bash
sudo apt update
sudo apt upgrade -y
```

## Git

```bash
sudo apt install git -y
```

## .NET 8 SDK

```bash
sudo apt install dotnet-sdk-8.0 -y
```

Verify installation:

```bash
dotnet --version
```

Expected output:

```text
8.x.x
```

---

# Install Docker

```bash
sudo apt install docker.io -y
```

Start Docker:

```bash
sudo service docker start
```

Verify Docker:

```bash
docker --version
```

---

# Clone the Repository

```bash
git clone https://github.com/YourUsername/SkyFlowTerminalManager.git
```

Navigate into project:

```bash
cd SkyFlowTerminalManager
```

---

# Restore Project Dependencies

```bash
dotnet restore
```

Expected output:

```text
Determining projects to restore...
Restore completed successfully.
```

---

# SQL Server Setup

## Pull SQL Server Image

```bash
docker pull mcr.microsoft.com/mssql/server:2022-latest
```

---

## Create SQL Server Container

```bash
docker run -e "ACCEPT_EULA=Y" \
-e "SA_PASSWORD=YourStrong@Password" \
-p 1433:1433 \
--name skyflow-sql \
-d mcr.microsoft.com/mssql/server:2022-latest
```

Verify container:

```bash
docker ps
```

Expected output:

```text
skyflow-sql
```

---

# Create Database

Enter SQL Server:

```bash
docker exec -it skyflow-sql \
/opt/mssql-tools18/bin/sqlcmd \
-S localhost \
-U SA \
-P "YourStrong@Password" \
-C
```

Create database:

```sql
CREATE DATABASE SkyFlowDB;
GO
```

---

# Import Database Schema

Copy schema into container:

```bash
docker cp skyflow_db_schema.sql \
skyflow-sql:/tmp/skyflow_db_schema.sql
```

Execute schema:

```bash
docker exec -it skyflow-sql \
/opt/mssql-tools18/bin/sqlcmd \
-S localhost \
-U SA \
-P "YourStrong@Password" \
-C \
-d SkyFlowDB \
-i /tmp/skyflow_db_schema.sql
```

Expected output:

```text
(2 rows affected)
(3 rows affected)
...
```

---

# Verify Tables

```bash
docker exec -it skyflow-sql \
/opt/mssql-tools18/bin/sqlcmd \
-S localhost \
-U SA \
-P "YourStrong@Password" \
-C \
-d SkyFlowDB \
-Q "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES"
```

Expected Tables:

```text
Users
Airport
Aircraft
Flight
Passenger
Booking
FlightAssignment
Crew
FlightLog
Baggage
Notification
AuditLog
```

---

# Build the Project

```bash
dotnet build
```

Expected output:

```text
Build succeeded.
```

---

# Run the Application

```bash
dotnet run
```

Expected output:

```text
Welcome to SkyFlow Terminal Manager
Setting up database connection...
Database connection successful.
```

---

# Administrator Testing

## Login

Username:

```text
admin
```

Password:

```text
admin123
```

Expected output:

```text
Authentication successful.
Role: Admin
```

---

## View Flights

Select:

```text
1. Manage Flights
1. View All Flights
```

Expected result:

```text
Current Flights
SF102
SF221
SF305
```

---

## Add Flight

Select:

```text
2. Add New Flight
```

Example:

```text
Flight Number: SF450
Origin: JHB
Destination: CPT
Departure: 2026-06-02 09:00
Arrival: 2026-06-02 11:00
Aircraft ID: 1
```

Expected output:

```text
Flight added successfully.
```

---

## Update Flight Status

Select:

```text
3. Update Flight Status
```

Example:

```text
Flight Number: SF450
New Status: Departed
```

Expected output:

```text
Flight SF450 status updated.
```

---

## View System Overview

Select:

```text
2. View System Overview
```

Displays:

* Flights
* Aircraft
* Airports

---

# Gate Agent Testing

Login:

```text
gateagent
```

Password:

```text
gate123
```

Expected:

```text
Role: GateAgent
```

---

## Flight Manifest

Select:

```text
1. Flight Manifest
```

Enter:

```text
SF102
```

Displays:

```text
Passenger List
Seat Numbers
Booking Status
```

---

## Passenger Check-In

Select:

```text
2. Passenger Check-In
```

Example:

```text
PASSPORT12345
```

Expected:

```text
Passenger Found
Booking Confirmed
```

---

## Boarding Gate

Select:

```text
3. Boarding Gate
```

Example:

```text
SF102
```

Status:

```text
D
```

Expected:

```text
Flight SF102 status updated to Departed.
```

---

# SQL Verification Testing

## Verify Flights

```bash
docker exec -it skyflow-sql \
/opt/mssql-tools18/bin/sqlcmd \
-S localhost \
-U SA \
-P "YourStrong@Password" \
-C \
-d SkyFlowDB \
-Q "SELECT FlightID, FlightNumber, Status FROM Flight"
```

---

## Verify Aircraft

```bash
docker exec -it skyflow-sql \
/opt/mssql-tools18/bin/sqlcmd \
-S localhost \
-U SA \
-P "YourStrong@Password" \
-C \
-d SkyFlowDB \
-Q "SELECT AircraftID, AircraftType, Capacity FROM Aircraft"
```

---

## Verify Passengers

```bash
docker exec -it skyflow-sql \
/opt/mssql-tools18/bin/sqlcmd \
-S localhost \
-U SA \
-P "YourStrong@Password" \
-C \
-d SkyFlowDB \
-Q "SELECT PassengerID, PassportNumber FROM Passenger"
```

---

# Project Folder Structure

```text
SkyFlowTerminalManager/
│
├── Models/
├── Interfaces/
├── Repositories/
├── Program.cs
├── DatabaseHelper.cs
├── ConsoleTableRenderer.cs
├── skyflow_db_schema.sql
├── README.md
│
└── screenshots/
```

---

# Assignment Requirements Covered

✔ Inheritance

✔ Encapsulation

✔ Polymorphism

✔ Abstraction

✔ SQL Server Integration

✔ Persistent Data Storage

✔ CRUD Operations

✔ Role-Based Authentication

✔ Console Data Visualization

✔ Flight Management

✔ Passenger Check-In

✔ Boarding Gate Processing

✔ System Overview

---

# Author

Katlego Sambo

SkyFlow Terminal Manager

Developed using C#, SQL Server, Docker, and Ubuntu.
