# Elektronické Posudky (Electronic Medical Assessments)

This project is a modern, robust, and scalable .NET 8 Web API for managing electronic medical assessments (Posudky). It follows Clean Architecture principles and incorporates Domain-Driven Design (DDD) patterns.

## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (if running locally without Docker)

### Running with Docker (Recommended)

The easiest way to get the project up and running is using Docker Compose.

1.  **Clone the repository**:

    ```bash
    git clone <repository-url>
    cd ElektronickePosudky
    ```

2.  **Configure environment variables**:
    Copy `env.sample` to `.env` and adjust the values if necessary (especially `SA_PASSWORD`).

    ```bash
    cp env.sample .env
    ```

3.  **Start the services**:

    ```bash
    docker-compose up -d
    ```

4.  **Access the API**:
    Once the containers are healthy, the API will be available at:
    - Swagger UI: [http://localhost:8080/swagger](http://localhost:8080/swagger) (configured in docker-compose)

### Running Locally (Manual)

1.  **Start a SQL Server instance** and ensure the connection string in `ElektronickePosudky.Api/appsettings.json` is correct.
2.  **Apply Migrations**:
    ```bash
    dotnet ef database update --project ElektronickePosudky.Infrastructure --startup-project ElektronickePosudky.Api
    ```
3.  **Run the API**:
    ```bash
    dotnet run --project ElektronickePosudky.Api
    ```

---

## 🌱 Seeding Initial Data

The application strictly validates input requests against standardized medical codebooks (Číselníky) defined by the national TermX server. To ensure the API validations pass and the Postman test collection runs successfully, you must seed this initial data into your database.

The mapping and seeding logic is completely integrated via EF Core's `HasData` within `CiselnikSeeder.cs`.

**To insert the list of codelist values into the database manually**, open your terminal in the root directory of the solution and run the following command:

```bash
dotnet ef database update --project ElektronickePosudky.Infrastructure --startup-project ElektronickePosudky.Api
```

## 🏗️ Architecture

The project follows **Clean Architecture** (Onion Architecture) to ensure separation of concerns, testability, and maintainability.

### Layers

1.  **Domain**: Core business logic. Contains Entities, Value Objects, Aggregates, and Domain Exceptions. It has no dependencies on other layers.
    - _Key Entities_: `PosudekRo`, `PosudekZpusobilost`, `CiselnikPolozka`.
2.  **Application**: Orchestrates business logic. Contains MediatR Commands/Queries, DTOs, Mapping profiles, and Interfaces.
    - _CQRS_: Implemented via **MediatR**.
    - _Validation_: Uses **FluentValidation** via a MediatR Pipeline Behavior.
3.  **Infrastructure**: External concerns. Contains Database context (EF Core), Repository implementations, and external services (PDF generation).
    - _Persistence_: Entity Framework Core with SQL Server.
    - _Reporting_: **QuestPDF** for generating medical assessment reports.
4.  **API**: Entry point. Contains Controllers, Middleware, and API configuration.
    - _Logging_: **Serilog** with structured logging (Console & File).
    - _Error Handling_: Global exception handling with `ValidationExceptionHandler`.

---

## 🛠️ Tech Stack

- **Framework**: .NET 8
- **ORM**: Entity Framework Core
- **Database**: Microsoft SQL Server
- **Messaging**: MediatR (CQRS)
- **Validation**: FluentValidation
- **Mapping**: AutoMapper
- **PDF Generation**: QuestPDF
- **Logging**: Serilog
- **Testing**: xUnit, FluentAssertions, Moq
- **Containerization**: Docker & Docker Compose

---

## 📝 Key Assumptions & Design Decisions

- **Domain Language**: The primary domain language is Czech (e.g., _Posudek_, _Ciselnik_), reflecting the local regulatory context, while the code and documentation maintain English technical terms where appropriate.
- **Standardization**: Use of **Codebooks (Číselníky)** for critical fields (Specializations, Statuses, Exam Types) to ensure data integrity and compatibility with national health systems.
- **Immutability**: Value Objects (e.g., `PacientVO`, `PoskytovatelVO`) are used for complex attributes to ensure structural equality and immutability.
- **Soft Validation**: While the API enforces strict technical validation, it assumes that business-level "eligibility" (Způsobilost) is a clinical decision reflected in the data provided by the doctor.
- **Localization**: The system is built to support multiple languages (defaulting to Czech `cs`), with localization infrastructure in place for the API and PDF reports.
- **Security**: The system assumes deployment in a secure environment with HTTPS redirection enabled in non-development environments. Authentication/Authorization is expected to be handled via an external Identity Provider (OIDC/OAuth2).

---

## 🧪 Testing

The solution includes a comprehensive test suite in `ElektronickePosudky.Tests`:

- **Unit Tests**: Testing individual components like Validators, Handlers, and Mappings.
- **Integration Tests**: Testing the full flow from Controller to Database (using in-memory or test database containers).

Run all tests:

```bash
dotnet test
```
