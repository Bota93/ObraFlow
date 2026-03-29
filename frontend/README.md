# ObraFlow Frontend

Frontend workspace for ObraFlow, a construction site management system.

The frontend is a React application that consumes the backend API for the main product workflows:
- dashboard
- workers
- daily reports
- incidents

## Live Demo

- App: `https://obraflow.adrianalcaraz.es`
- API: `https://api-obraflow.adrianalcaraz.es`
- Swagger: `https://api-obraflow.adrianalcaraz.es/swagger`

## Status

The frontend application is implemented with Vite, React, and TypeScript.

The current workspace already includes:

- application layouts and router
- persistent demo notice in the application shell
- shared HTTP client and environment configuration
- development API fallback from `http://localhost:5250` to `http://localhost:5000` when `VITE_API_BASE_URL` is not set
- dashboard data integration
- workers, daily reports, and incidents data views
- create worker route at `/workers/new` with React Hook Form and Zod validation

The next goal is to evolve this MVP without breaking the documented architecture.

## Documentation

Architecture, conventions, structure, and roadmap are defined in [docs/frontend-architecture.md](docs/frontend-architecture.md).
