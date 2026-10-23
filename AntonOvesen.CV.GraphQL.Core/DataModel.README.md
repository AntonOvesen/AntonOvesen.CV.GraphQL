## Overview of data model

So i dont lose track.
Generated with mermaid AI tool :wicked:
[link](https://mermaid.ai/app/projects/7e6d8a9d-a531-432e-9410-293b1d09d52c/diagrams/0a67f334-282c-4e07-8eff-aa2b61ddaaf2/share/invite/eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJkb2N1bWVudElEIjoiMGE2N2YzMzQtMjgyYy00ZTA3LThlZmYtYWEyYjYxZGRhYWYyIiwiYWNjZXNzIjoiVmlldyIsInB1cnBvc2UiOiJzaGFyZS1pbnZpdGUiLCJpYXQiOjE3OTI3NjgxNTUsImV4cCI6MTc5NTM2MDE1NX0.tPTvr9lU_8qDQps3btI8PfLTvAMhhldDSa786Lg79dY?entryPoint=share-modal)
```mermaid
---
config:
  theme: mc
---
erDiagram
    USER }o--o{ SKILL : has

    USER ||--o{ COMPANY : works_at
    COMPANY }o--o{ SKILL : involves

    USER ||--o{ EDUCATION : has
    EDUCATION }o--o{ SKILL : develops
    INSTITUTION ||--o{ EDUCATION : provides

    USER ||--o{ PROJECT : has
    PROJECT }o--o{ SKILL : uses
```

This doesnt really make sense.
Skill needs to be more clearly defined or tied to a user maybe?
Or one-way relation between skill and their origin maybe.. :think:

Gotta think more about it but this is a good first scaffold.