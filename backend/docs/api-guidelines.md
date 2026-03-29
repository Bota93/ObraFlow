# API Guidelines

## Purpose

These guidelines define how the HTTP layer should evolve as ObraFlow grows beyond the currently implemented `Workers`, `DailyReports`, and `Incidents` endpoints.

## Controller Rules

Controllers should:

- stay thin
- receive HTTP requests
- delegate use-case work
- return proper HTTP status codes
- avoid direct persistence logic

Controllers should not:

- contain EF Core queries
- hold business rules that belong in Domain or Application
- become the place where module orchestration lives

## Route Design

Prefer resource-based routes such as:

- `/workers`
- `/daily-reports`
- `/incidents`
- `/materials`

Use plural resource names consistently.

## Response Behavior

Current baseline behaviors in the project:

- `200 OK` for successful reads and updates when returning content
- `201 Created` for successful creates
- `204 No Content` for successful deletes
- `400 Bad Request` for validation failures
- `404 Not Found` when a resource does not exist

## DTO Usage

Use DTOs in Application for request and response contracts.

Avoid:

- exposing EF Core concerns through API contracts
- duplicating DTOs in multiple layers without a concrete reason

## Validation

Validation is currently handled with DTO data annotations and `IValidatableObject` where needed. Unhandled exceptions already flow through the global exception middleware in the API project.

The preferred next step is:

- validate incoming requests consistently
- standardize error formatting across validation, domain, and application failures
- avoid scattering validation rules across controllers

## Swagger

Swagger is already configured in the API project. For new endpoints:

- keep route names clear
- document request and response models through conventional ASP.NET Core patterns
- make sure example flows in documentation match the real endpoints
