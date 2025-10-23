# Car Auction Management System

This project implements a vehicle auction management system using ASP.NET Core. It allows for adding vehicles, starting and closing auctions, and placing bids.

## Design Decisions

### 1. Object-Oriented Programming (OOP)
The system follows OOP principles to ensure clean separation of concerns, code reuse, and easy maintenance.

### 2. Use of Interfaces
Interfaces like `IVehicleService` and `IAuctionService` abstract service layer, making the system easier to test and more flexible for future changes.

### 3. In-Memory Repositories
In-memory data storage via `List<T>` is used instead of a database, as persistence was not required. Repositories are registered as 'Singleton' to preserve data during application runtime.

### 4. Business Validation
Dedicated validator classes `VehicleValidator`, `BidValidator` encapsulate domain rules such as unique vehicle IDs or valid bid amounts, ensuring consistency and maintainability.

### 5. Layered Architecture
The system is divided into:
- **Models**: Represent the domain
- **Services**: Contain business logic
- **Controllers**: Expose API endpoints

### 6. Unit Testing with xUnit
Tests are included for:
- Adding and removing vehicles
- Creating and closing auctions
- Valid and invalid bids
- Expected exceptions and business rule enforcement

## Assumptions

- A vehicle can have **only one active auction** at a time.
- Vehicle uniqueness is enforced using a `Guid` identifier.
- No database or UI is required.
- Authentication is not included.
- All data is temporary and stored in memory.
