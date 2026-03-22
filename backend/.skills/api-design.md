# API Design Skill

## Goal
Design clean, predictable and RESTful endpoints for a backend API.

## Standards

### Routes
- Use plural nouns: /workers
- Use resource-based URLs, not verbs
- Examples:
  - GET /workers
  - GET /workers/{id}
  - POST /workers
  - PUT /workers/{id}
  - DELETE /workers/{id}

### HTTP Status Codes
- 200 OK → successful GET/PUT
- 201 Created → successful POST
- 204 NoContent → successful DELETE
- 400 BadRequest → validation errors
- 404 NotFound → resource not found

### Controllers
- Keep controllers thin
- Do not include business logic
- Do not include EF queries directly if complexity grows

### Validation
- Validate input DTOs
- Never trust client input
- Return clear error messages

### DTO Rules
- Never expose domain entities directly
- Separate request and response models

### Naming
- Clear and consistent naming
- Avoid abbreviations

## Anti-patterns
- Fat controllers
- Returning entities directly
- Inconsistent status codes
- Mixed responsibilities

## Decision rule
If unsure:
- prioritize clarity over cleverness
- prioritize consistency over flexibility