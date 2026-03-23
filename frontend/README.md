# ObraFlow Frontend

Frontend workspace for ObraFlow, a construction site management system.

The frontend is a React application that consumes the backend API for the main product workflows:
- dashboard
- workers
- daily reports
- incidents

## Status

The frontend application is implemented with Vite, React, and TypeScript.

The current workspace already includes:

- application layouts and router
- shared HTTP client and environment configuration
- dashboard data integration
- workers, daily reports, and incidents data views
- create worker route and validated form flow

The next goal is to evolve this MVP without breaking the documented architecture.

## Documentation

Architecture, conventions, structure, and roadmap are defined in [docs/frontend-architecture.md](docs/frontend-architecture.md).
