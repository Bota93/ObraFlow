# API Reference

## Base URLs

Public demo:

- `https://api-obraflow.adrianalcaraz.es`
- `https://api-obraflow.adrianalcaraz.es/swagger`

Local development:

- `http://localhost:5250`
- `http://localhost:5250/swagger`

Docker Compose:

- `http://localhost:5000`
- `http://localhost:5000/swagger`

Operational endpoint:

- `GET /health`

## Implemented Resources

- `Dashboard`
- `Workers`
- `DailyReports`
- `Incidents`

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

## Health

Routes:

- `GET /health`

Behavior:

- returns `200 OK` with `{"status":"ok"}` when the API can reach the database
- returns `503 Service Unavailable` when database connectivity fails

## Workers

Routes:

- `GET /workers`
- `GET /workers/{id}`
- `POST /workers`
- `PUT /workers/{id}`
- `DELETE /workers/{id}`

Create and update payload:

```json
{
  "name": "Adrian Test",
  "role": "Electrician",
  "phoneNumber": "1234567",
  "hourlyRate": 25.5,
  "isActive": true
}
```

## Daily Reports

Routes:

- `GET /daily-reports`
- `GET /daily-reports/{id}`
- `POST /daily-reports`
- `PUT /daily-reports/{id}`
- `DELETE /daily-reports/{id}`

Create and update payload:

```json
{
  "date": "2026-03-21",
  "workerId": "00000000-0000-0000-0000-000000000000",
  "hoursWorked": 8.5,
  "description": "Daily report for trench inspection"
}
```

Behavior note:

- if the referenced worker does not exist, the API returns `400 Bad Request`

## Incidents

Routes:

- `GET /incidents`
- `GET /incidents/{id}`
- `POST /incidents`
- `PUT /incidents/{id}`
- `DELETE /incidents/{id}`

Create and update payload:

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

Behavior note:

- `reportedAtUtc` must be sent in UTC with an explicit `Z` suffix
- timestamps without timezone information, or with a non-UTC offset, are rejected with `400 Bad Request`

## Common Status Codes

- `200 OK` for successful reads and updates
- `201 Created` for successful creates
- `204 No Content` for successful deletes
- `400 Bad Request` for validation failures
- `404 Not Found` when the resource does not exist
- `500 Internal Server Error` for unexpected server-side failures
- `429 Too Many Requests` when demo write protection is enabled and the create limit is exceeded

## Unhandled Errors

For unexpected exceptions caught by the global middleware, the API returns JSON with:

- `message`
- `traceId`

## Demo Protection Notes

When `DemoMode:EnableWriteRateLimiting` is enabled, write rate limiting applies to:

- `POST /workers`
- `POST /daily-reports`
- `POST /incidents`

For details, see `demo-protection.md`.

## Notes

- All endpoints are designed for a stateless API environment
- No authentication is currently implemented (MVP scope)
- Demo environments may enforce write rate limiting

## Not Yet Implemented

`Materials` is still not exposed through the API.
