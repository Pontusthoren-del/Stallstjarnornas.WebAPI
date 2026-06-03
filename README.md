# Stallstjärnornas WebAPI

A RESTful Web API for managing restaurant bookings, guests, tables, and mail logs for Stallstjärnornas restaurant. Built with ASP.NET Core 9 and Entity Framework Core targeting SQL Server.

---

## Table of Contents

- [Project Description](#project-description)
- [Architecture Overview](#architecture-overview)
- [Getting Started](#getting-started)
- [API Endpoints](#api-endpoints)
- [Test Strategy & Results](#test-strategy--results)

---

## Project Description

Stallstjärnornas WebAPI is the backend for a restaurant reservation system. It supports the full booking lifecycle — from guest registration and booking creation to table assignment and cancellation. The system distinguishes between guest-facing operations (create/cancel bookings) and admin operations (filter, update, delete bookings).

**Tech Stack:**
- .NET 9 / ASP.NET Core
- Entity Framework Core with SQL Server
- MSTest + Moq for testing
- EF Core InMemory for test isolation
- Scalar for API documentation (development only)

---

## Architecture Overview

The solution is structured into three projects:

```
Stallstjarnornas.WebAPI/
├── Stallstjarnornas.Library/        # Shared domain models
│   └── Models/
│       ├── Booking.cs
│       ├── Guest.cs
│       ├── Sitting.cs
│       ├── OperatingDay.cs
│       ├── Table.cs
│       ├── TableAssignment.cs
│       └── MailLog.cs
│
├── Stallstjarnornas.WebAPI/         # Main API project
│   ├── Controllers/                 # HTTP request handlers
│   ├── Services/                    # Business logic
│   ├── Interfaces/                  # Service contracts
│   ├── DTOs/                        # Data transfer objects
│   ├── Data/                        # EF Core DbContext & seed data
│   ├── Migrations/                  # EF Core migrations
│   └── Exceptions/                  # Custom domain exceptions
│
└── Stallstjarnornas.Test/           # Test project
    ├── ServiceTest/                 # Service-layer integration tests
    ├── ControllerTest/              # Controller unit tests (Moq)
    └── TestHelpers/                 # Shared test data & DbContext factory
```

### Layered Design

The API follows a clean layered architecture:

- **Controllers** handle HTTP routing and return appropriate status codes. They delegate all business logic to services via interfaces.
- **Services** contain business rules (e.g. capacity validation, duplicate email checks, booking status management).
- **Interfaces** decouple controllers from concrete service implementations, enabling testability via mocking.
- **DTOs** ensure a clean separation between the HTTP surface and the domain model.
- **Custom Exceptions** (`NotFoundException`, `ConflictException`, `ValidationException`) are used to communicate domain errors from services to controllers without leaking implementation details.

### Domain Model

```
OperatingDay  ──< Sitting ──< Booking >── Guest
                                 │
                           TableAssignment ──> Table
```

A `Sitting` belongs to an `OperatingDay` and defines a time slot with a maximum guest capacity. A `Booking` references a `Sitting` and optionally a `Guest`. `TableAssignments` link specific tables to a booking.

---

## Getting Started

### Prerequisites

- .NET 9 SDK
- SQL Server (or SQL Server LocalDB)

### Configuration

Update the connection string in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=StallstjarnornasDb;Trusted_Connection=True;"
  }
}
```

### Run Migrations & Start

```bash
dotnet ef database update --project Stallstjarnornas.WebAPI
dotnet run --project Stallstjarnornas.WebAPI
```

The API documentation (Scalar UI) is available at `/scalar/v1` when running in Development mode.

---

## API Endpoints

### Guest — `/api/Guest`

| Method | Route | Description | Success | Error |
|--------|-------|-------------|---------|-------|
| `GET` | `/api/Guest` | Retrieve all guests | `200 OK` | — |
| `GET` | `/api/Guest/{id}` | Get a guest by ID | `200 OK` | `404 Not Found` |
| `POST` | `/api/Guest` | Register a new guest | `201 Created` | `409 Conflict` (duplicate email) |
| `PUT` | `/api/Guest/{id}` | Update guest details | `200 OK` | `404 Not Found` |
| `DELETE` | `/api/Guest/{id}` | Delete a guest | `204 No Content` | `404 Not Found` |

**CreateGuestDto / UpdateGuestDto fields:** `Name`, `Phone`, `Email`

> Note: Deleting a guest with existing bookings does not delete the bookings — instead their status is set to `Cancelled` and the `GuestId` is nullified.

---

### Booking — `/api/Booking`

| Method | Route | Description | Success | Error |
|--------|-------|-------------|---------|-------|
| `POST` | `/api/Booking` | Create a new booking | `200 OK` | `400 Bad Request`, `404 Not Found` |
| `GET` | `/api/Booking/BookingNumber/{bookingNumber}` | Get booking by booking number | `200 OK` | `404 Not Found` |
| `GET` | `/api/Booking/Filter-Booking` | Filter bookings with query parameters | `200 OK` | `400 Bad Request` |
| `PATCH` | `/api/Booking/Cancel/{bookingNumber}` | Cancel a booking | `200 OK` | `404 Not Found`, `409 Conflict` |
| `PUT` | `/api/Booking/Update/{bookingNumber}` | Update a booking | `200 OK` | `400 Bad Request`, `404 Not Found` |
| `DELETE` | `/api/Booking/Delete/{bookingNumber}` | Permanently delete a booking | `200 OK` | `404 Not Found` |

**Filter-Booking query parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| `status` | `string?` | Booking status (e.g. `Active`, `Cancelled`) |
| `date` | `DateOnly?` | Exact booking date |
| `sittingId` | `int?` | Filter by sitting |
| `week` | `int?` | ISO week number |
| `month` | `int?` | Month (1–12) |
| `year` | `int?` | Year |
| `isPlaced` | `bool?` | Whether tables have been assigned |
| `guestName` | `string?` | Partial guest name match |
| `bookingNumber` | `int?` | Specific booking number |

---

### Table Assignment — `/api/TableAssignment`

| Method | Route | Description | Success | Error |
|--------|-------|-------------|---------|-------|
| `POST` | `/api/TableAssignment/Assign-table` | Assign tables to a booking | `200 OK` | `400 Bad Request` |
| `GET` | `/api/TableAssignment/Find-Available-Tables` | Find available tables for a given time and capacity | `200 OK` | `400 Bad Request` |
| `DELETE` | `/api/TableAssignment/Delete-Table-Assignments` | Remove table assignments from a booking | `200 OK` | `400 Bad Request` |

---

### Mail Log — `/api/MailLog`

| Method | Route | Description | Success |
|--------|-------|-------------|---------|
| `GET` | `/api/MailLog` | Retrieve all mail log entries | `200 OK` |

---

## Test Strategy & Results

### Overview

The project uses **MSTest** as the test framework with two distinct testing approaches:

**1. Service-layer integration tests** (`ServiceTest/`)
Tests run against a real EF Core InMemory database, seeded with consistent test data via `TestHelpers/TestHelperData.cs` and `DbContextFactory.cs`. Each test class initializes a fresh database context in `[TestInitialize]` and disposes it in `[TestCleanup]`, ensuring full test isolation.

**2. Controller unit tests** (`ControllerTest/`)
Tests use **Moq** to mock service interfaces, so controller logic (HTTP status code mapping, routing, response shape) can be verified independently of business logic. No database is involved.

---

### Test Coverage by Area

#### GuestService (12 tests)
| Test | Scenario |
|------|----------|
| `GetGuestByIdAsync_ShouldReturnGuest_WhenGuestExists` | Returns correct guest for valid ID |
| `GetGuestByIdAsync_WhenGuestDoesNotExist_ReturnNull` | Returns null for unknown ID |
| `GetAllGuestAsync_ReturnAllGuest` | Returns all guests from the database |
| `GetGuestEntityByEmailAsync_ReturnMatchingEmail` | Finds guest by exact email |
| `GetGuestEntityByEmailAsync_ShouldReturnNull_WhenEmailDoesNotExist` | Returns null for unknown email |
| `UpdateGuestAsync_ShouldUpdateAndReturnGuest_WhenGuestExists` | Updates all fields correctly |
| `UpdateGuestAsync_ShouldReturnNull_WhenGuestDoesNotExist` | Returns null for unknown ID |
| `RegisterGuestAsync_ShouldCreateAndReturnGuest_WhenEmailIsNew` | Creates guest and persists to DB |
| `RegisterGuestAsync_ShouldReturnNull_WhenEmailAlreadyExists` | Rejects duplicate email |
| `DeleteGuestAsync_ShouldReturnTrue_WhenGuestIsDeleted` | Confirms successful deletion |
| `DeleteGuestAsync_ShouldReturnCancelled_WhenDeleteGuestThatHadBookings` | Sets related bookings to Cancelled |
| `DeleteGuestAsync_GuestIdShouldReturnNull_WhenDeleteGuestThatHadBookings` | Nullifies GuestId on related bookings |

#### GuestController (9 tests, Moq)
Covers all five controller actions (`GetGuest`, `GetAllGuests`, `RegisterGuest`, `UpdateGuest`, `DeleteGuest`) with both happy-path and error scenarios, verifying correct HTTP status codes (`200`, `201`, `204`, `404`, `409`).

#### BookingService (10+ tests)
| Scenario |
|----------|
| Cancel booking — sets status to `Cancelled` |
| Cancel already-cancelled booking — throws `ConflictException` |
| Cancel non-existent booking — throws `NotFoundException` |
| Get booking by number — returns correct DTO |
| Create booking with new guest — creates guest and booking |
| Create booking with existing guest — reuses existing guest record |
| Create booking when sitting is full — throws `ValidationException` |
| Create booking with non-existent sitting — throws `NotFoundException` |
| Filter by date — returns matching bookings only |
| Filter by status — returns matching bookings only |
| Filter by `isPlaced` — returns bookings with/without table assignments |
| Filter with no criteria — returns all bookings |

#### BookingController (5+ tests, Moq)
Tests cover `GetBookingByNumber`, `DeleteBooking`, and `CreateBooking` with success, `404 Not Found`, and `400 Bad Request` scenarios.

#### TableAssignmentService (7+ tests)
| Scenario |
|----------|
| Assign tables with valid input — returns response DTO |
| Assign already-booked tables — throws exception |
| Assign non-existent table ID — throws exception |
| Assign fewer tables than needed for booking size — throws exception |
| Assign tables when booking not found — throws exception |
| Assign with no tables provided — throws exception |
| Assign with duplicate table IDs — throws exception |

#### TableAssignmentController (tests)
Covers `CreateTableAssignmentAsync`, `GetAvailableTablesAsync`, and `DeleteAssignedTablesAsync` with mocked service responses.

---

### What the Tests Taught Us

Writing the tests did not just verify existing code — in several cases they directly drove changes to the implementation.

**GuestService — deletion with active bookings**
When writing the tests for `DeleteGuestAsync` it became clear that a straightforward delete was not sufficient. The tests exposed the scenario where a guest has existing bookings, which led us to implement logic that sets `Status = "Cancelled"` and nullifies `GuestId` on all related bookings rather than blocking the deletion entirely. Without the tests this edge case would likely have been missed.

**TableAssignmentService — defensive input validation**
Writing tests for edge cases such as duplicate table IDs and empty table lists revealed that these scenarios were not handled at all in the initial implementation. The tests drove us to add defensive validation at the start of `CreateTableAssignmentAsync`, catching bad input before it reached the database.

**BookingService — cancellation of already-cancelled bookings**
The test `CancelBooking_AlreadyCancelled_ShouldThrowException` uncovered that the service originally allowed cancelling a booking that was already cancelled, silently doing nothing. The test made this a clearly defined error case that now throws a `ConflictException` with a descriptive message.


---

### Running the Tests

```bash
dotnet test Stallstjarnornas.Test
```
