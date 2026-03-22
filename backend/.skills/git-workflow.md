# Git Workflow Skill

## Goal
Keep a professional commit history that reflects meaningful progress.

## Branch model
- main: stable
- develop: integration
- feature/<name>: isolated work

## Commit rules
Commit when:
- the solution builds
- a feature slice is complete
- a bug fix is verified
- a structural change is coherent and finished

Do not commit:
- broken experiments unless explicitly creating a checkpoint branch
- generated files like bin/obj
- secrets or local environment files

## Commit style
Use conventional-style messages:
- chore:
- feat:
- fix:
- docs:
- refactor:

## Examples
- chore: bootstrap solution structure
- feat: add worker entity and ef configuration
- fix: correct dbcontext registration for postgres
- docs: add architecture decision notes

## Review rule
Before each commit:
1. run build
2. review git diff
3. check no generated files are included
4. ensure message matches the real change