## Exercise Tracker

A console based exercise app that performs CRUD operations, allowing users to log and manage exercises.

## How to run application

- Ensure you have .NET SDK latest version

```
git clone https://github.com/your-username/exercise-tracker.git
cd exercise-tracker
```

- Database set up
  Before running the application for the first time, you need to create a database and apply migrations

1. Ensure SQL Server is running
2. Apply migrations by running the step below, in project directory via terminal

```
dotnet ef database update
```

- Set up User Secrets

The application uses User Secrets to store the database connection string securely. Set up your connection string using the following commands:

```
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Database=ShiftsLoggerDB;User=sa;Password=YOURPASSWORD;Encrypt=True;TrustServerCertificate=True"

```

- Run Application

- Open a terminal or command prompt into the project directory.
  Build the app:

```
dotnet build
```

Run the app:

```
dotnet run
```

## Requirements

1. **Error Handling**:

   - Able to handle all possible errors so that the application never crashes.

2. **Follow DRY Principle**:

   - Avoid code repetition.

3. **Separation of Concerns**:

   - Object-Oriented Programming

## Features

- User Secrets

  - Securely store database connection strings.

- Repository Pattern:

  - Decouples data access logic from business logic, improving maintainability
