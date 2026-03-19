# ObraFlow API

## Overview
ObraFlow is a backend API for construction site management, built with ASP.NET Core and PostgreSQL.

The goal of this project is to demonstrate professional backend development practices using .NET, including clean architecture, containerization, and database integration.

## Tech Stack
- .NET 10 (ASP.NET Core Web API)
- Entity Framework Core
- PostgreSQL
- Docker & Docker Compose
- Swagger / OpenAPI

## Architecture
The project follows a layered architecture:

- Domain → Business entities and core logic
- Application → Use cases, DTOs, service contracts
- Infrastructure → Database, EF Core, external services
- API → Controllers, HTTP layer, configuration

## Project Structure
```
src/
├── ObraFlow.Api
├── ObraFlow.Application
├── ObraFlow.Domain
└── ObraFlow.Infrastructure
```

## Getting Started

### Run with Docker
```bash
docker compose up --build
```

API will be available at:
http://localhost:5000/swagger

## Database
PostgreSQL is used as the primary database.

Migrations are handled via Entity Framework Core.

## Features (MVP)
- Workers management
- Daily reports
- Incidents tracking
- Materials tracking

## Roadmap
- Authentication (JWT)
- Role-based access
- File uploads
- Logging
- Tests
- CI/CD

## Author
Adrián Alcaraz Rodríguez
