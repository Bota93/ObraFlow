# Debugging Skill

## Goal
Fix issues systematically without random rewriting.

## Debugging order
When an error appears, classify it first:

1. build error
2. package/dependency error
3. namespace/reference error
4. EF Core migration error
5. Docker/build container error
6. runtime configuration error
7. PostgreSQL connection error

## Process
1. read the exact error
2. identify file/project involved
3. find whether the issue is structural or local
4. apply the smallest safe fix
5. rebuild
6. document the reason for the fix

## Rules
- Never fix by rewriting half the repo blindly
- Never remove layers just to “make it work”
- Never change namespaces inconsistently
- Never change several architectural decisions at once unless necessary

## Common checks

### Build errors
- wrong namespace
- missing using
- missing project reference
- wrong target framework
- package installed in wrong project

### EF Core errors
- DbContext not discoverable
- startup project incorrect
- provider not registered
- migrations assembly not specified
- entity config mismatch

### Docker errors
- wrong build context
- wrong Dockerfile path
- port collision
- environment variable mismatch
- API starting before db is really ready

### PostgreSQL errors
- wrong host
- wrong password
- database not created
- port busy
- container unhealthy

## Expected output from an AI assistant
For each bug:
- explain root cause
- explain exact fix
- provide precise file-level change
- keep the rest of the architecture intact