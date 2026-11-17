<<<<<<< HEAD
# RaceDay_poe_part2
# RaceDay

![CI/CD](https://github.com/ST1047510/RaceDayPoe_Part2/actions/workflows/ci-cd.yml/badge.svg)

RaceDay is a web API for organising race events. Organizers create categories and events, participants join events and enrol with a bib number, and organizers record the results after the race.

## Features

- **Accounts:** register and log in as an Organizer or a Participant. Passwords are hashed with BCrypt.
- **Categories:** organizers create age-based categories (minimum and maximum age) for their events.
- **Events:** organizers create, update and delete their own events. Each event has a name, description, date, location, maximum participants and status.
- **Joining:** participants join an event. Joining the same event twice is refused.
- **Enrollment:** a participant who has joined an event enrols with a bib number and a status.
- **Results:** organizers record the finish time, position and status for each enrolled participant.

## Business rules

| Area | Rule |
| --- | --- |
| Registration | Emails must be unique. Registering as an Organizer also creates an organizer record. |
| Passwords | At least 8 characters, no spaces, and must include an uppercase letter, a lowercase letter, a number and a special character. |
| Events | Only Organizers can create events. Only the organizer who owns an event can update or delete it. Deleting an event also removes its participants. |
| Categories | Only Organizers can create categories. A category that is used by an event cannot be deleted. |
| Joining | Only registered Participants can join an event, and only once. |
| Enrollment | A participant must join an event before enrolling in it. |
| Results | Only Organizers can create results, and the bib number must belong to an enrollment in that event. |

## Tech stack

- C# / ASP.NET Core Web API
- SQL Server (LocalDB for development)
- BCrypt.Net for password hashing
- xUnit for tests
- GitHub Actions for CI/CD

## Project structure

```
RaceDay/
├── .github/workflows/ci-cd.yml     CI/CD pipeline
├── database/schema.sql             database tables (used by the pipeline)
├── RaceDay_poe_part2/              the API
│   ├── backend/                    business logic and database access
│   └── Controllers/                API controllers (Auth, Events, Categories, Enrollments)
└── Raceday_tests2/                 xUnit tests
```

> Update this tree if your folders are named differently.

## Database

The API uses a SQL Server database called `racedaydb` with these tables:

| Table | Purpose |
| --- | --- |
| `users` | every account, with its role |
| `Organizers` | one row per organizer, linked to a user |
| `Catergories` | race categories, linked to an organizer |
| `Events` | races, linked to an organizer and a category |
| `Participants` | a user who joined an event |
| `Enrollment` | a participant's bib number and status for an event |
| `Results` | finish time, position and status for an enrollment |

## Getting started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (the version in the project's `.csproj`)
- SQL Server LocalDB (installed with Visual Studio's "Data storage and processing" workload) or SQL Server Express
- Visual Studio 2022 or newer, or the `dotnet` CLI

### Set up

1. Clone the repository:

   ```bash
   git clone https://github.com/YOUR-USERNAME/YOUR-REPO.git
   cd YOUR-REPO
   ```

2. Create the LocalDB instance and the database:

   ```bash
   sqllocaldb create raceday_api
   sqllocaldb start raceday_api
   sqlcmd -S "(localdb)\raceday_api" -E -Q "CREATE DATABASE racedaydb"
   sqlcmd -S "(localdb)\raceday_api" -E -d racedaydb -i database/schema.sql
   ```

3. Check the connection string. The project expects:

   ```
   Data source=(localdb)\raceday_api;database=racedaydb;Integrated Security=true;TrustServerCertificate=true;
   ```

4. Run the API:

   ```bash
   dotnet run --project RaceDay_poe_part2
   ```

## API overview

> Fill this in with your real routes.

| Controller | What it does |
| --- | --- |
| `AuthController` | register and log in |
| `EventsController` | create, view, update, delete and join events |
| `CategoriesController` | create, update and delete categories |
| `EnrollmentsController` | create, view, update and delete enrollments |
| Results | create, view, update and delete results |

## Running the tests

```bash
dotnet test
```

The tests run against the real `racedaydb` database. Every test seeds its own data and cleans up before and after, and the cleanup only deletes rows whose names start with `XUNIT_` or whose emails look like `xunit_...@raceday.test`. Do not give your own data those names.

The tests are written in this style: one comment block per test, an object for the class under test, `test_results` and `expected_results` variables, then an assert.

## CI/CD

The workflow in `.github/workflows/ci-cd.yml` runs on every push and pull request to `main`:

1. **Build and test:** builds the solution, creates a LocalDB database from `database/schema.sql`, and runs the xUnit tests.
2. **Publish:** on pushes to `main`, publishes the API and stores it as a build artifact.
3. **Deploy:** if the repository variable `AZURE_WEBAPP_NAME` and the secret `AZURE_WEBAPP_PUBLISH_PROFILE` are set, deploys to Azure App Service.

## Author

[Your name] – [Your student number / contact]

## Licence

[Choose a licence, for example MIT, or remove this section.]
=======
RaceDay POE Part 2 — Race Event Management System

A full-stack race event management system built with ASP.NET Core 8. The system allows organisers to create and manage race events, and participants to enrol and view their results. It follows a client-server architecture where an MVC web application consumes a REST API backend.

Solution Overview

The solution contains three projects:

Project	Type	Port	Purpose
RaceDay_poe_part2	ASP.NET Core Web API	https://localhost:7062	REST API — handles all data and business logic
Raceday_poe	ASP.NET Core MVC	https://localhost:7121	Web frontend — consumes the API via HttpClient
RaceDay_poe.Tests	xUnit Test Project	—	Unit tests for API backend methods

Both the API and MVC app must run simultaneously. Configure this in Visual Studio under:

Right-click Solution → Set Startup Projects → Multiple startup projects → set both to Start

Prerequisites
.NET 8 SDK
SQL Server (LocalDB is fine — (localdb)\raceday_api)
Visual Studio 2022 (v17.8 or later)
SQL Server Management Studio (SSMS) — for database setup
Database Setup

The application uses a SQL Server database named racedaydb. The connection string is configured in the API project.

Tables
Table	Description
Users	Stores all user accounts (email, password, role)
Organizers	Organiser profiles linked to Users
Participants	Participant profiles linked to Users and Events
Events	Race events created by organisers
Catergories	Race categories (age groups, distances)
Enrollment	Participant enrollments into events
Results	Race results captured by organisers
Relationships
Organizers → Users (User_id)
Participants → Users (User_id) and Events (event_id)
Events → Organizers (Organiser_id) and Catergories (Category_id)
Enrollment → Events (event_id) and Participants (Participant_id)
Results → Enrollment (Enrollment_id)
Running the Application
Clone the repository
Open RaceDay_poe_part2.sln in Visual Studio 2022
Set up the database (run SQL scripts in SSMS)
Configure Multiple Startup Projects (see above)
Press F5
The API Swagger UI opens at: https://localhost:7062/swagger
The MVC web app opens at: https://localhost:7121
Architecture
API Layer — RaceDay_poe_part2

The API uses raw ADO.NET (no Entity Framework) with SqlConnection, SqlCommand and SqlDataReader for all database operations.

Controllers/          — API endpoints (HTTP routes)
backend/              — Database logic (SQL queries)
models/               — Data models (POCOs)

Each controller delegates to a backend class:

EventsController → Events.cs
CategoriesController → Categories.cs
EnrollmentsController → Enrollments.cs
ResultsController → Results.cs
UsersController → Users.cs
MVC Layer — Raceday_poe

The MVC app uses a typed HttpClient service (Api_sercices.cs) to call every API endpoint. Controllers never touch the database directly.

Controllers/          — Handle HTTP requests, call Api_sercices, pass data to Views
Models/               — ViewModels matching API JSON responses
Servicers/            — Api_sercices.cs (all HttpClient calls)
Views/                — Razor views (.cshtml)

Data flows like this:

Browser → MVC Controller → Api_sercices (HttpClient) → API Controller → backend class → SQL Server
Features
Authentication
Users register with email, password and role (Organizer or Participant)
Login stores Email and Role in ASP.NET Core Session
Role-based redirection: Organisers go to /Organizer/Dashboard, Participants go to /Participant/ParticipantHome
All controller actions check session before proceeding
Organizer Features
Feature	Route	Description
Dashboard	/Organizer/Dashboard	Shows total events, total enrollments, upcoming events
View Events	/Event/Events	List all events with edit/delete actions
Create Event	/Event/CreateEvent	Form to create a new race event
Edit Event	/Event/EditEvent/{id}	Pre-filled form to update an event
Delete Event	/Event/Events	Delete via POST form (no confirmation page)
View Event Details	/Event/EventDetails/{id}	Full event info
Create Category	/Organizer/CreateCategory	Form to create a race category
View Categories	/Organizer/Categories	List all categories
View Enrollments	/Organizer/Enrollments	View all participant enrollments with participant email, event and status
Capture Result	/Organizer/CaptureResult	Form to record finish time, position and status by bib number
View Results	/Organizer/Results	List all captured results
Participant Features
Feature	Route	Description
Home	/Participant/ParticipantHome	Browse all available events
Event Detail	/Participant/EventDetail/{id}	View event info, select category, enter bib number and enroll
My Enrollments	/Participant/MyEnrollments	View personal enrollment history filtered by logged-in email
My Results	/Participant/MyResults	View personal race results filtered by logged-in email
API Endpoints Reference
Users
Method	Endpoint	Parameters	Returns
POST	/api/Users/register	name, email, password, role	string message
POST	/api/Users/login	email, password	string message with role
Events
Method	Endpoint	Parameters	Returns
POST	/api/Events/Organizers	EventName, Eventdescription, EventDate, EventLocation, MaxParticipants, Eventstatus, organizerEmail, categoryName	string message
GET	/api/Events/all	—	List<EventsModel> JSON
GET	/api/Events/details/{EventId}	EventId (route)	EventsModel JSON
PUT	/api/Events/update/{EventId}	EventId, fields to update	string message
DELETE	/api/Events/{EventId}	EventId (route)	string message
Categories
Method	Endpoint	Parameters	Returns
POST	/api/Categories/Organizers	categoryName, categoryDescrition, minAge, maxAge, organizerEmail	string message
GET	/api/Categories/all	—	List<CatergoriesModel> JSON
GET	/api/Categories/details/{CatergoryId}	CatergoryId (route)	CatergoriesModel JSON
Enrollments
Method	Endpoint	Parameters	Returns
POST	/api/Enrollments/createenrollment/organizers	BibNumber, Enrollmentstatus, EventName, Email	string message
GET	/api/Enrollments/all	—	List<EnrollmentsModel> JSON (with JOIN to Events and Users)
GET	/api/Enrollments/MyEnrollments	email (query)	List<EnrollmentsModel> JSON filtered by participant email
Results
Method	Endpoint	Parameters	Returns
POST	/api/Results/CaptureResults	finishTime, position, ResultStatus, BibNumber, EventName, Email	string message
GET	/api/Results/all	—	List<ResultsModel> JSON
GET	/api/Results/MyResults	email (query)	List<ResultsModel> JSON filtered by participant email
Key Technical Decisions
Why raw ADO.NET instead of Entity Framework?

The API uses raw SqlConnection and SqlDataReader throughout. This gives full control over SQL queries and is consistent with the project's existing code style.

Why HttpClient instead of direct DB access in MVC?

The MVC app calls the API via HttpClient registered as a typed service in Program.cs. This enforces separation of concerns — the MVC layer handles presentation only and never touches the database directly.

JSON deserialization

All API responses are deserialized using System.Text.Json with PropertyNameCaseInsensitive = true to handle casing differences between API model property names and JSON keys.

DBNull handling

Several database columns are nullable (foreign keys like Organiser_id, Participant_id). Every SqlDataReader cast checks for DBNull.Value before casting:

csharp
cat.organizerID = reader["OrganizerID"] == DBNull.Value ? 0 : (int)reader["OrganizerID"];
Avoiding duplicate property deserialization failures

System.Text.Json with PropertyNameCaseInsensitive = true fails silently and returns null when a model has two properties that differ only by casing (e.g. eventName and EventName). All models were cleaned to have only one property per JSON field.

Project Structure
RaceDay_poe_part2/              ← API project
├── Controllers/
│   ├── UsersController.cs
│   ├── EventsController.cs
│   ├── CategoriesController.cs
│   ├── EnrollmentsController.cs
│   └── ResultsController.cs
├── backend/
│   ├── Users.cs
│   ├── Events.cs
│   ├── Categories.cs
│   ├── Enrollments.cs
│   └── Results.cs
├── models/
│   ├── UsersModel.cs
│   ├── EventsModel.cs
│   ├── CatergoriesModel.cs
│   ├── EnrollmentsModel.cs
│   └── ResultsModel.cs
└── Program.cs

Raceday_poe/                    ← MVC project
├── Controllers/
│   ├── HomeController.cs
│   ├── OrganizerController.cs
│   ├── EventController.cs
│   └── ParticipantController.cs
├── Models/
│   ├── UserModel.cs
│   ├── EventModel.cs
│   ├── CategoryModel.cs
│   ├── EnrollmentModel.cs
│   └── ResultModel.cs
├── Servicers/
│   └── Api_sercices.cs
├── Views/
│   ├── Home/
│   ├── Organizer/
│   ├── Event/
│   └── Participant/
└── Program.cs

RaceDay_poe.Tests/              ← Test project
└── UnitTests.cs
Developer

Lesego Mtshixa
Diploma in Information Technology — Software Development
Rosebank College | 2025–2028
GitHub: LESEGO002
>>>>>>> f6c6a8c9ff3d09fc3c87e0ae92490094682f2d25
