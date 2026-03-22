# Testing And Quality

## Purpose

This document defines the quality expectations for ObraFlow as it evolves from a foundation project into a larger backend maintained by multiple contributors.

## Current State

Right now, the repository has:

- a layered solution structure
- PostgreSQL integration
- EF Core mappings
- migrations
- Docker-based local runtime
- API integration tests for `Workers`, `DailyReports`, and `Incidents`

What is still missing:

- application-level tests
- infrastructure-focused persistence tests
- centralized error handling
- health and quality gates in CI

## Quality Priorities

The quality order should follow the project delivery order from `AGENTS.md`:

1. Keep the solution buildable.
2. Keep PostgreSQL connectivity working.
3. Keep Docker Compose working.
4. Keep migrations correct.
5. Add feature behavior safely.

## Minimum Verification For Changes

Before merging a branch, contributors should verify the parts affected by their work.

Typical checks:

- solution builds
- migrations apply cleanly when schema changes
- Docker Compose still starts when runtime or infrastructure changes
- updated endpoints return the expected status codes
- integration tests pass for affected HTTP modules
- documentation matches the implemented behavior

## Expected Test Layers As The Project Grows

### Domain Tests

Add these when domain behavior becomes richer than simple data holders.

Examples:

- enum-driven rules
- domain exceptions
- invariant protection

### Application Tests

These should validate:

- use-case orchestration
- DTO mapping behavior
- business flow outcomes

### Infrastructure Tests

These should validate:

- EF Core mappings
- query behavior
- migration safety
- provider-specific persistence expectations

### API Tests

These should validate:

- routing
- response codes
- request validation behavior
- error response shape

## Recommended Near-Term Quality Improvements

The best next additions are:

- an application test project aligned with the current layers
- infrastructure tests that exercise PostgreSQL behavior and migrations
- consistent validation and exception handling
- a shared API error contract

## Documentation Rule

If a contributor introduces:

- a new verification command
- a new test project
- a new validation approach
- a new quality gate

they should update this document in the same branch.
