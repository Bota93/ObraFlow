# API Guidelines

## Purpose

These guidelines define how the HTTP layer should evolve as ObraFlow adds controllers and endpoints.

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

- `/api/workers`
- `/api/daily-reports`
- `/api/incidents`
- `/api/materials`

Use plural resource names consistently.

## Response Behavior

As features are added, aim for these baseline behaviors:

- `200 OK` for successful reads and updates when returning content
- `201 Created` for successful creates
- `204 No Content` for successful deletes or updates without response content
- `400 Bad Request` for validation failures
- `404 Not Found` when a resource does not exist

## DTO Usage

Use DTOs in Application for request and response contracts.

Avoid:

- exposing EF Core concerns through API contracts
- duplicating DTOs in multiple layers without a concrete reason

## Validation

Validation should become explicit as the API grows. The preferred long-term direction is:

- validate incoming requests consistently
- centralize error formatting
- avoid scattering validation rules across controllers

## Swagger

Swagger is already configured in the API project. As controllers are added:

- keep route names clear
- document request and response models through conventional ASP.NET Core patterns
- make sure example flows in documentation match the real endpoints
