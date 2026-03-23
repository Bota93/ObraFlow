# API Reference

## Overview

Current implemented HTTP resources:

- `Dashboard`
- `Workers`
- `DailyReports`
- `Incidents`

Base URL examples in local development:

- `http://localhost:5250`
- `http://localhost:5250/swagger`

When running through Docker Compose, the API is exposed on:

- `http://localhost:5000`
- `http://localhost:5000/swagger`

## Dashboard

Routes:

- `GET /dashboard/summary`

Response fields:

- `totalWorkers`
- `activeWorkers`
- `totalDailyReports`
- `openIncidents`
- `inProgressIncidents`
- `resolvedIncidents`
- `hoursWorkedToday`
- `hoursWorkedLast7Days`

## Workers

Routes:

- `GET /workers`
- `GET /workers/{id}`
- `POST /workers`
- `PUT /workers/{id}`
- `DELETE /workers/{id}`

Create and update payload shape:

```json
{
  "name": "Adrian Test",
  "role": "Electrician",
  "phoneNumber": "1234567",
  "hourlyRate": 25.5,
  "isActive": true
}
```

Response fields:

- `id`
- `name`
- `role`
- `phoneNumber`
- `hourlyRate`
- `createdAtUtc`
- `isActive`

## DailyReports

Routes:

- `GET /daily-reports`
- `GET /daily-reports/{id}`
- `POST /daily-reports`
- `PUT /daily-reports/{id}`
- `DELETE /daily-reports/{id}`

Create and update payload shape:

```json
{
  "date": "2026-03-21",
  "workerId": "00000000-0000-0000-0000-000000000000",
  "hoursWorked": 8.5,
  "description": "Daily report for trench inspection"
}
```

Response fields:

- `id`
- `date`
- `workerId`
- `workerName`
- `hoursWorked`
- `description`

Behavior note:

- if the referenced worker does not exist, the API returns `400 Bad Request`

## Incidents

Routes:

- `GET /incidents`
- `GET /incidents/{id}`
- `POST /incidents`
- `PUT /incidents/{id}`
- `DELETE /incidents/{id}`

Create and update payload shape:

```json
{
  "title": "Water leak in floor 2",
  "description": "Water leak detected near the service corridor.",
  "status": 1,
  "reportedAtUtc": "2026-03-21T09:00:00Z"
}
```

`status` values:

- `1` = `Open`
- `2` = `InProgress`
- `3` = `Resolved`

Response fields:

- `id`
- `title`
- `description`
- `status`
- `reportedAtUtc`

## Common Status Codes

- `200 OK` for successful reads and updates
- `201 Created` for successful creates
- `204 No Content` for successful deletes
- `400 Bad Request` for validation failures
- `404 Not Found` when the resource does not exist

## Not Yet Implemented

The `Materials` resource is not exposed yet through the API.
