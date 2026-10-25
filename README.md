# Zero Hunger API

An ASP.NET Core API for managing food collection requests between restaurants and collection employees.

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&logoColor=white) ![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-Web%20API-512BD4) ![SQL Server](https://img.shields.io/badge/Database-SQL%20Server-CC2927?logo=microsoftsqlserver&logoColor=white)

This project is intended to run locally and be shared as source on GitHub. It has no hosted demo.

## Contents

- [Project overview](#project-overview)
- [Architecture](#architecture)
- [API areas](#api-areas)
- [Technology stack](#technology-stack)
- [Run locally](#run-locally)
- [Database](#database)
- [Notes](#notes)

## Project overview

The API supports restaurant and employee records, plus a collection-request workflow. A separate ASP.NET Core MVC project in this repository provides a web interface and uses the same business and data projects.

## Architecture

The solution separates HTTP handling, business operations, and database access into three projects:

```text
HTTP clients
    ├── AppLayerAPI (ASP.NET Core controllers / JSON endpoints)
    └── ZeroHungerMVC (server-rendered MVC web interface)
                  │
                  ▼
             BLL (services, models, AutoMapper profile)
                  │
                  ▼
             DAL (repositories, EF Core DbContext and entities)
                  │
                  ▼
             SQL Server
```

- **AppLayerAPI** defines REST-style controllers and registers services, repositories, AutoMapper, and the SQL Server context through dependency injection.
- **BLL** contains service classes and API-facing models. Services map between models and database entities with AutoMapper.
- **DAL** contains repository classes and `ZeroHungerDbContext`, which maps entities to SQL Server tables.
- **ZeroHungerMVC** is a separate Razor MVC web application. It references the same BLL and DAL projects and uses session-based login in its own web layer.

## API areas

Routes use controller names as their prefixes (for example, `/Restaurant` and `/CollectRequest`). The API project exposes:

| Area | Capabilities |
| --- | --- |
| Restaurants | List, retrieve, create, update, and delete restaurant records |
| Employees | List, retrieve, create, update, and delete employee records |
| Collection requests | CRUD operations; filter by status, restaurant, or employee; list pending requests by deadline; accept, collect, complete, or reject a request |

In development, the API maps its OpenAPI document. The default ASP.NET Core weather forecast sample endpoint is also present.

## Technology stack

| Area | Technology |
| --- | --- |
| API and web app | ASP.NET Core / .NET 10 |
| Data access | Entity Framework Core 10 |
| Database provider | Microsoft SQL Server |
| Entity/model mapping | AutoMapper |
| Web interface | ASP.NET Core MVC, Razor, Bootstrap |

## Run locally

### Prerequisites

- .NET 10 SDK
- A SQL Server instance
- A `ZeroHungerDb` database with the tables described below

### Configure the database connection

The API reads the `DbConn` connection string. Set it with .NET User Secrets for local development rather than committing credentials:

```sh
cd AppLayerAPI
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DbConn" "Server=localhost,1433;Database=ZeroHungerDb;User Id=sa;Password=<your-local-password>;TrustServerCertificate=True"
```

Use a local SQL Server account and password appropriate for your machine. Do not put real credentials in `appsettings.json` or commit secrets.

### Start the API

From the repository root:

```sh
dotnet run --project AppLayerAPI
```

The launch profile selects the local URL. In Development, the OpenAPI document is available at `/openapi/v1.json` on that API host.

### Start the MVC application (optional)

The MVC project uses the same `DbConn` configuration key. Configure User Secrets for it as well, then run:

```sh
cd ZeroHungerMVC
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DbConn" "Server=localhost,1433;Database=ZeroHungerDb;User Id=sa;Password=<your-local-password>;TrustServerCertificate=True"
cd ..
dotnet run --project ZeroHungerMVC
```

## Database

The EF Core context maps these core tables. This repository does not include a migrations directory or database creation script, so provide a compatible database before running the applications.

| Table | Required columns | Purpose |
| --- | --- | --- |
| `Restaurant` | `id`, `name`, `address` | Restaurant identity and contact location |
| `Employee` | `id`, `name`, `phno` | Collection employee details |
| `CollectRequest` | `id`, `resid`, `empid`, `status`, `maxtime` | Request restaurant, optional assigned employee, workflow status, and collection deadline |

`CollectRequest.resid` references `Restaurant.id`; nullable `CollectRequest.empid` references `Employee.id`. New requests are saved as `Pending` with no employee assigned. The urgent-request endpoint returns pending requests ordered by `maxtime`.

## Notes

- No screenshots are included in the repository yet.
- The API currently registers restaurant, employee, and collection-request services. Login and registration logic exists in the shared BLL and is used by the MVC app; the API does not currently expose authentication endpoints.
- The API connection string is intentionally omitted from tracked settings. Configure it with User Secrets before running locally; the project has a `UserSecretsId` for that purpose.
