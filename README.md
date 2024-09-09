# Purchasing & Inventory Management Module

This is a .NET 8 project for managing suppliers, purchase orders, and purchase receipts in a retail system.

## Features
- Supplier Management (CRUD operations)
- Purchase Order Management (Create, Update, Cancel Orders)
- Purchase Receipt Management (Link with Purchase Order, Update Inventory)
- Entity Framework Core with SQL for data persistence
- Clean Architecture for separation of concerns

## Project Structure
The project follows **Clean Architecture**, divided into several layers:
- **Domain Layer**: Contains business logic and entity definitions.
- **Application Layer**: Implements use cases and manages services.
- **Infrastructure Layer**: Handles database interactions with Entity Framework Core and external services.
- **Presentation Layer**: Exposes APIs via ASP.NET Core Web API.

### Folder Structure:
