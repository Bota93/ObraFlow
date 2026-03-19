# Testing Skill

## Goal
Create reliable integration tests for the API without breaking architecture or contaminating development data.

## Principles
- Prefer integration tests for HTTP endpoints
- Use a dedicated test database
- Never run automated tests against the main development database
- Keep tests deterministic and easy to understand
- Use Arrange / Act / Assert

## Scope
Test:
- endpoint status codes
- basic request/response behavior
- persistence behavior when needed

Do not focus first on:
- trivial unit tests
- excessive mocking
- framework internals

## Conventions
- Test project: tests/ObraFlow.Api.IntegrationTests
- Test class names: <Feature>EndpointsTests
- Method names: <Action>_Should<ExpectedResult>
- Keep assertions explicit
- Prefer clean and isolated test data

## Rules
- If tests need the API entrypoint, expose Program with partial Program
- If tests need DB access, use obraflow_test
- Do not point tests to obraflowdb
- Keep test setup minimal and reproducible