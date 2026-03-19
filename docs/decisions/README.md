# Architecture Decision Records

## Purpose

Architecture Decision Records, or ADRs, capture the important technical decisions that shape the repository over time.

They are especially useful once multiple contributors are working in parallel, because they reduce hidden context and make tradeoffs visible.

## When To Add An ADR

Create an ADR when a change affects:

- architectural boundaries
- persistence strategy
- module organization
- error-handling approach
- authentication or security direction
- deployment or runtime structure

## ADR Format

Each ADR should include:

- status
- context
- decision
- consequences

## Naming

Use incremental names such as:

- `0001-layered-architecture.md`
- `0002-validation-strategy.md`
- `0003-authentication-approach.md`

## Rule

ADRs should describe real decisions already made or formally accepted, not rough brainstorms.
