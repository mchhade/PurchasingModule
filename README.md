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
- **Data Access Layer**: This layer handles interactions with the underlying data storage,
  such as a database or external services. It includes components responsible for querying, inserting,
  updating, and deleting data. Its purpose is to abstract the data storage implementation details and provide a consistent interface for accessing and manipulating data..
- **Business Layer**: The business logic layer contains the core functionality and rules of the application.
   It implements business processes, workflows, validations, and calculations. Its purpose is to enforce business rules,
  manage data integrity, and orchestrate interactions between different parts of the system.
- **Presentation Layer**:This layer is responsible for presenting information to the user and handling user input.
   It includes components such as user interfaces, web pages, or API endpoints.
  Its purpose is to interact with users and translate their actions into requests that the application can understand.

## Prerequisites
To run the project, you need the following:
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Entity Framework Core 8
- SQL LocalDB or SQLite (or any SQL Server database)
- Git for version control
  ## Setup Instructions

1. **Clone the repository**:
    ```bash
    git clone https://github.com/your-username/purchasing-inventory-module.git
2. **Database Setup**:
    If you're using **SQL LocalDB**:
    - Update the `appsettings.json` file to reflect your database connection string.
    - Run migrations to set up the database:
      ```bash
      dotnet ef migrations add initial --project .\PurchasingModule.DataAccess
      dotnet ef database update --project .\PurchasingModule.DataAccess

    ```
    4. **Run the Project**:
    Start the web API by running the following command:
    ```bash
    dotnet run
    ```
    
