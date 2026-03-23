# Frontend Architecture - ObraFlow

## Purpose

This document defines the architecture, conventions, and implementation strategy for the ObraFlow frontend.

Its goals are to:
- ensure consistency during development
- prevent overengineering
- guide AI-assisted development
- provide a clear execution path

This is a working document, not theoretical documentation.

## Current Status

The frontend workspace is no longer just initialized.

It already includes:
- Vite + React + TypeScript
- feature-oriented folder structure
- React Router with app and public layouts
- TanStack Query provider setup
- centralized Axios client
- dashboard, workers, daily reports, and incidents views connected to the backend API

The feature architecture described in this document remains the target shape for the next iterations as the codebase evolves.

## Planned Tech Stack

- React
- Vite
- TypeScript
- Tailwind CSS
- React Router
- TanStack Query
- React Hook Form
- Zod

## Architectural Principles

- No overengineering
- Prefer simplicity over abstraction
- Clear separation of concerns
- Feature-oriented structure
- Avoid premature global state
- Keep components focused and predictable
- Backend is the source of truth

## Project Structure

```text
src/
  app/
    layouts/
    providers/
    router/

  pages/
    dashboard/
    workers/
    daily-reports/
    incidents/
    not-found/

  features/
    dashboard/
      api/
      components/
      hooks/
      types/
    workers/
      api/
      components/
      hooks/
      schemas/
      types/
    daily-reports/
      api/
      components/
      hooks/
      schemas/
      types/
    incidents/
      api/
      components/
      hooks/
      schemas/
      types/

  shared/
    api/
    components/
    config/
    constants/
    hooks/
    lib/
    types/

  assets/
  styles/

  main.tsx
```

## Layer Responsibilities

### `app/`

Global application setup:
- routing
- providers
- layouts

### `pages/`

Route-level components:
- compose features
- avoid heavy business logic
- define page structure

### `features/`

Domain-oriented frontend modules.

Each feature contains only the code needed for its use cases, such as:
- API calls
- hooks for data and behavior
- UI components
- types and schemas

Features should stay isolated and cohesive.

### `shared/`

Reusable and cross-cutting code:
- API client
- base UI components
- utilities
- configuration
- constants
- shared types when truly cross-feature

## Routing Strategy

Use React Router with two main layouts:
- `PublicLayout` for future authentication-related flows
- `AppLayout` for the main application UI

All core product modules should live under `AppLayout`.

## State Management Strategy

### Server State

Handled with TanStack Query for:
- fetching
- caching
- invalidation

### UI State

Handled locally with:
- `useState`
- `useReducer` only when the state shape justifies it

### Global State

Use React Context only when necessary, for example:
- authentication in the future
- app-level settings

Do not introduce global state prematurely.

## API Strategy

- No direct API calls inside components
- Centralized HTTP client in `shared/api`
- Feature-specific API functions inside each feature

Examples:
- `features/workers/api/get-workers.ts`
- `features/workers/api/create-worker.ts`

## Data Contracts

The frontend should define its own types.

Do not blindly mirror backend entities. Model only what the UI needs.

Examples:
- `WorkerListItem`
- `WorkerDetail`
- `CreateWorkerRequest`

## Forms and Validation

Use:
- React Hook Form
- Zod

Forms should include:
- validation
- error feedback
- loading state

## UI Direction

The interface should follow a practical admin-panel approach:
- sidebar navigation
- topbar
- tables for data-heavy views
- metric cards for summaries
- simple and clear forms

Avoid:
- unnecessary animations
- visual complexity without product value

## Naming Conventions

- components: `kebab-case.tsx`
- hooks: `use-*.ts`
- pages: `*-page.tsx`
- schemas: `*.schema.ts`
- types: `*.types.ts`
- API files: one function per use case when practical

## Development Rules

- No business logic in presentation-only components
- Avoid unnecessary abstractions
- Keep features self-contained
- Do not introduce tools without justification
- Prioritize readability and maintainability

## Backend Integration

Planned environment variable:

```env
VITE_API_BASE_URL=http://localhost:5250
```

This should match the backend local development URL from `launchSettings.json`. When running through Docker Compose, the exposed API URL is `http://localhost:5000`.

All requests should go through a centralized API client.

## Core Modules

- Dashboard
- Workers
- Daily Reports
- Incidents

## Implementation Roadmap

### Phase 1 - Foundation

- Vite setup
- TypeScript
- Tailwind CSS
- Router
- Layout
- API client

Status: completed

### Phase 2 - Dashboard

- integrate dashboard summary endpoint
- build metrics view

Status: implemented as a read-focused MVP view

### Phase 3 - Workers

- list
- detail
- create

Status: list-style API view implemented, detail and create still pending

### Phase 4 - Daily Reports

- list
- detail
- create

Status: list-style API view implemented, detail and create still pending

### Phase 5 - Incidents

- list
- detail
- create or update

Status: list-style API view implemented, detail/create/update still pending

### Phase 6 - Polish

- error handling
- empty states
- UI consistency
- basic responsiveness

## Working With Codex

Codex is an assistant, not an architect.

Use Codex for:
- scaffolding
- repetitive code
- small refactors

Do not use Codex for:
- architectural decisions
- introducing new dependencies without justification
- adding complexity without clear need

Every AI-generated change should satisfy these checks:
- Is it necessary?
- Is it simple?
- Does it follow this architecture?
- Can it be explained clearly?

If not, reject it.

## Final Note

This document defines how the frontend should evolve.

If implementation diverges from this document, either:
- update this document
- or refactor the code

Do not allow silent inconsistency between the documented architecture and the real implementation.
