# Frontend Architecture

## Overview

The ObraFlow frontend is a React MVP that consumes the backend API without duplicating backend business logic.

The frontend treats the backend as the single source of truth, with TanStack Query managing server state synchronization and caching.

It currently supports:

- dashboard view
- workers list and create flow
- daily reports list
- incidents list
- a persistent demo notice in the authenticated shell

## Structure

The frontend uses a feature-based structure inside `frontend/src`:

- `app/` for router, layouts, and providers
- `pages/` for route-level composition
- `features/` for domain-specific UI, hooks, API calls, and types
- `shared/` for cross-cutting client, config, constants, and reusable components

## Data Flow Pattern

The typical frontend flow is:

1. a page composes one or more feature components
2. the feature component uses a feature hook
3. the hook calls a feature API function
4. the API function uses the shared HTTP client

That keeps route composition, server-state access, and HTTP details separated.

## Server State

TanStack Query is used for server state:

- data fetching
- caching
- invalidation for write flows where needed

The shared Axios client lives in `shared/api`, and each feature keeps its own API calls close to the related UI code.

When `VITE_API_BASE_URL` is not set in development, the frontend tries `http://localhost:5250` first and automatically falls back to `http://localhost:5000` if the first backend target is unavailable.

## Forms

Current write flows use:

- React Hook Form for form state
- Zod for client-side validation close to the feature

The current implemented write flow is the worker creation route at `/workers/new`.

## Current Direction

The frontend should stay:

- simple
- feature-oriented
- API-driven
- easy to evolve during the MVP phase

It should not introduce unnecessary state libraries or frontend-side business logic.
