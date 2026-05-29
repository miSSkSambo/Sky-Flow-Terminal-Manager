# SkyFlow Terminal Manager Report/Testing

## Overview

SkyFlow Terminal Manager is a console-based airport terminal management system developed using **C# (.NET 8)** and **Microsoft SQL Server** running in Docker on Ubuntu. The application provides role-based access for Administrators and Gate Agents to manage airport operations including flights, passengers, check-ins, boarding, aircraft, and airport information.

---

# Technologies Used

* C# (.NET 8)
* Microsoft SQL Server
* Docker
* Ubuntu Linux
* Repository Pattern
* Console-Based User Interface

---

## Project Structure

```text
SkyFlowTerminalManager/
│
├── screenshots/
│   ├── app-running.png
│   ├── admin-login.png
│   ├── manage-flights.png
│   ├── view-flights.png
│   ├── add-flight.png
│   ├── flight-added-verification.png
│   ├── update-flight-status.png
│   ├── system-overview.png
│   ├── gate-agent-login.png
│   ├── flight-manifest.png
│   ├── passenger-checkin.png
│   ├── boarding-gate.png
│   ├── schema-imported.png
│   ├── tables-created.png
│   ├── file-found.png
│   ├── sql-aircraft.png
│   ├── sql-passenger.png
│   └── sql-flight.png
│
└── README.md
```

---

# Database Setup Verification

## Project Files Located

This screenshot shows the SkyFlow project structure and confirms that all required source files, folders, and database scripts are available.

<img width="942" height="759" alt="file found" src="https://github.com/user-attachments/assets/65cbced2-7a81-4b41-b322-c545911be840" />


---

## Database Schema Imported

The database schema was successfully imported into SQL Server and sample data was inserted.

<img width="944" height="687" alt="schema impirted" src="https://github.com/user-attachments/assets/8a857f0b-5aa2-4f63-8078-fbf011f60fbe" />


---

## Database Tables Created

The screenshot below confirms that all required tables were created successfully.

### Tables Created

* Users
* Airport
* Aircraft
* Flight
* Passenger
* Booking
* FlightAssignment
* Crew
* FlightLog
* Baggage
* Notification
* AuditLog

<img width="960" height="814" alt="table created" src="https://github.com/user-attachments/assets/c1a162de-4c4b-42d3-bfe7-2f0736ed8c58" />


---

# Application Execution

## Application Startup

The application successfully starts and establishes a connection to SQL Server.

<img width="947" height="232" alt="app running" src="https://github.com/user-attachments/assets/35b79a86-a5da-4959-9641-f1c1e0ec6616" />


---

# Administrator Functions

## Administrator Login

The administrator successfully logs into the system and gains access to the Admin Dashboard.

<img width="967" height="568" alt="logged in an admin" src="https://github.com/user-attachments/assets/d8ac4f62-bbca-43de-8306-ef939e2e3cd9" />


---

## Manage Flights Menu

The administrator can manage flights using the following options:

1. View All Flights
2. Add New Flight
3. Update Flight Status
4. Return to Dashboard

<img width="1127" height="404" alt="manage flights" src="https://github.com/user-attachments/assets/b3aa0232-667f-41bf-9e36-dd583eff4218" />


---

## View All Flights

This screen displays all flights stored in the database.

Displayed information includes:

* Flight Number
* Origin Airport
* Destination Airport
* Departure Time
* Arrival Time
* Aircraft Assignment
* Flight Status

<img width="1919" height="1015" alt="view all flights" src="https://github.com/user-attachments/assets/d8581f40-bb88-40d9-b45c-34bea0faf561" />


---

## Add New Flight

The administrator successfully adds a new flight (SF450) into the database.

Details entered include:

* Flight Number
* Origin Airport
* Destination Airport
* Departure Time
* Arrival Time
* Aircraft ID

<img width="956" height="949" alt="add a new flight" src="https://github.com/user-attachments/assets/d3afd86e-7681-4aa3-bc13-0538f5a16257" />


---

## Verify Added Flight

After adding the flight, the administrator verifies that the flight has been successfully stored in the database.

<img width="1919" height="655" alt="checking if flight was added" src="https://github.com/user-attachments/assets/10b6b21b-765c-4b5c-b83a-a966f04b455f" />


---

## Update Flight Status

The administrator updates Flight SF450 from **Scheduled** to **Departed**.

This demonstrates successful update functionality and database persistence.

<img width="960" height="506" alt="update flight status" src="https://github.com/user-attachments/assets/ea4d218f-61f6-4b76-8784-aac3757fedec" />


---

## System Overview

The system overview provides a consolidated display of:

### Flights

* Flight Information
* Flight Status

### Aircraft

* Aircraft Type
* Capacity
* Status

### Airports

* Airport Code
* Airport Name
* City
* Country

<img width="1919" height="956" alt="system overview" src="https://github.com/user-attachments/assets/bd2f122b-5a9d-4529-a2db-a0dc252454d7" />


---

# Gate Agent Functions

## Gate Agent Login

The Gate Agent successfully authenticates and accesses the Gate Agent Dashboard.

Available functions:

1. Flight Manifest
2. Passenger Check-In
3. Boarding Gate
4. Logout

<img width="938" height="860" alt="logged in as gateagent" src="https://github.com/user-attachments/assets/428be8e8-3f1c-474e-a706-ce5b57cf97d2" />


---

## Flight Manifest

The Flight Manifest displays all passengers assigned to a specific flight.

Information shown:

* Passport Number
* First Name
* Last Name
* Seat Number
* Booking Status

<img width="942" height="435" alt="flight manifest" src="https://github.com/user-attachments/assets/89d4b578-eab8-4a4b-b408-e3c9a3af8f37" />


---

## Passenger Check-In

The Gate Agent searches for a passenger using a passport number and retrieves booking information.

Displayed information:

* Passenger Details
* Booking Reference
* Seat Number
* Booking Status

<img width="964" height="1009" alt="passenger checked in" src="https://github.com/user-attachments/assets/86fa49b0-71f7-4118-91ff-9420e8a7f366" />


---

## Boarding Gate Processing

The Gate Agent finalizes boarding for Flight SF102 and updates the flight status to **Departed**.

This demonstrates successful boarding operations and flight status updates.

<img width="961" height="349" alt="Boarding gate" src="https://github.com/user-attachments/assets/8d031dba-5d73-40aa-b6bb-b6ef5559c50e" />


---

# SQL Database Verification

The following screenshots verify that the application is connected to SQL Server and that data is correctly stored.

## Aircraft Records

Aircraft data successfully retrieved from SQL Server.

<img width="1377" height="429" alt="aircraft records" src="https://github.com/user-attachments/assets/8ba8ce6c-d079-4468-9f8b-afa3bbd5f923" />


---

## Passenger Records

Passenger information successfully retrieved from SQL Server.

<img width="1130" height="290" alt="passenger records" src="https://github.com/user-attachments/assets/8351c52a-4a08-4b5b-b5cb-c863ccc4a97c" />


---

## Flight Records

Flight data successfully retrieved from SQL Server.

<img width="1264" height="341" alt="flight records" src="https://github.com/user-attachments/assets/c5b0168e-190c-49b5-9a92-d3285b7ebce0" />


---

# Features Implemented

## Administrator Features

* User Authentication
* View Flights
* Add Flights
* Update Flight Status
* View System Overview

## Gate Agent Features

* View Flight Manifest
* Passenger Check-In
* Boarding Gate Processing

## Database Features

* SQL Server Integration
* Repository Pattern
* Relational Database Design
* Persistent Data Storage

---

# Conclusion

The SkyFlow Terminal Manager system was successfully developed and tested. The screenshots above demonstrate:

* Successful database creation
* Successful SQL Server integration
* Role-based authentication
* Flight management
* Passenger management
* Check-in operations
* Boarding operations
* Data persistence and retrieval

---



