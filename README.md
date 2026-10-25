# Zero Hunger API

An ASP.NET Core API for managing food collection requests between restaurants and collection employees.

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&logoColor=white) ![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-Web%20API-512BD4) ![SQL Server](https://img.shields.io/badge/Database-SQL%20Server-CC2927?logo=microsoftsqlserver&logoColor=white)

This project is intended to run locally and be shared as source on GitHub. It has no hosted demo.

## Contents

- [Project overview](#project-overview)
- [Architecture](#architecture)
- [API areas](#api-areas)
- [Example requests](#example-requests)
- [Technology stack](#technology-stack)
- [Run locally](#run-locally)
- [Database](#database)
- [Design decisions](#design-decisions)
- [Notes](#notes)

## Project overview

The API supports restaurant and employee records, plus a collection-request workflow. A restaurant posts a request with a pickup deadline, an employee accepts it, collects the food, and completes it.

## Architecture

The solution separates HTTP handling, business operations, and database access into three projects:

```text
HTTP clients
     │
     ▼
AppLayerAPI (ASP.NET Core controllers / JSON endpoints)
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

- **AppLayerAPI** defines the controllers and registers services, repositories, AutoMapper, and the SQL Server context through dependency injection.
- **BLL** contains service classes and API-facing models. Services map between models and database entities with AutoMapper.
- **DAL** contains repository classes and `ZeroHungerDbContext`, which maps entities to SQL Server tables.

## API areas

Routes use controller names as their prefixes (for example, `/Restaurant` and `/CollectRequest`). The API exposes:

| Area | Capabilities |
| --- | --- |
| Restaurants | List, retrieve, create, update, and delete restaurant records |
| Employees | List, retrieve, create, update, and delete employee records |
| Collection requests | CRUD operations; filter by status, restaurant, or employee; list pending requests by deadline; accept, collect, complete, or reject a request |

In development, the API maps its OpenAPI document at `/openapi/v1.json`.

## Example requests

These assume the API is running on the default local address from `launchSettings.json` (`http://localhost:5133`).

Create a restaurant:

```sh
curl -X POST http://localhost:5133/Restaurant/create \
  -H "Content-Type: application/json" \
  -d '{"name":"Green Bowl","address":"12 Lake Road"}'
```

Create an employee:

```sh
curl -X POST http://localhost:5133/Employee/create \
  -H "Content-Type: application/json" \
  -d '{"name":"Rafi","phno":"01712345678"}'
```

Create a collection request for restaurant 1 (it is saved as `Pending`):

```sh
curl -X POST http://localhost:5133/CollectRequest/create \
  -H "Content-Type: application/json" \
  -d '{"resid":1,"status":"Pending","maxtime":"2026-11-05T18:00:00"}'
```

Let employee 1 accept request 1:

```sh
curl -X PUT http://localhost:5133/CollectRequest/1/accept \
  -H "Content-Type: application/json" \
  -d '1'
```

List pending requests, most urgent first:

```sh
curl http://localhost:5133/CollectRequest/urgent
```

## Technology stack

| Area | Technology |
| --- | --- |
| API | ASP.NET Core / .NET 10 |
| Data access | Entity Framework Core 10 |
| Database provider | Microsoft SQL Server |
| Entity/model mapping | AutoMapper |

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

## Database

The EF Core context maps these core tables. This repository does not include a migrations directory or database creation script, so provide a compatible database before running the API.

| Table | Required columns | Purpose |
| --- | --- | --- |
| `Restaurant` | `id`, `name`, `address` | Restaurant identity and contact location |
| `Employee` | `id`, `name`, `phno` | Collection employee details |
| `CollectRequest` | `id`, `resid`, `empid`, `status`, `maxtime` | Request restaurant, optional assigned employee, workflow status, and collection deadline |

`CollectRequest.resid` references `Restaurant.id`; nullable `CollectRequest.empid` references `Employee.id`. New requests are saved as `Pending` with no employee assigned. The urgent-request endpoint returns pending requests ordered by `maxtime`.

## Design decisions

### Thin controllers

Controllers only translate HTTP to a service call and back. Queries live in the DAL, and services in the BLL sit between them.

### Models separate from entities

The API sends and receives BLL models, not database entities, so a database change does not silently change the API's JSON.

## Notes

- There is no authentication. Every endpoint is open, which is fine for a local project but not for a deployed service.
- The API connection string is intentionally omitted from tracked settings. Configure it with User Secrets before running locally; the project has a `UserSecretsId` for that purpose.
