# Done Criteria Skill

## A task is done only if:
- code is in the correct layer
- solution builds successfully
- namespaces are correct
- no generated files are tracked
- no placeholder pseudo-code remains
- the change is understandable to another developer
- commit can be made cleanly

## A feature is done only if:
- build passes
- relevant runtime flow works
- swagger still loads if API was touched
- database changes have migration coverage if needed
- documentation is updated if setup changed

## A fix is not done if:
- it only hides the symptom
- it breaks layering
- it introduces a new unexplained dependency
- it compiles locally but leaves docker or migrations broken