# AntonOvesen.CV.GraphQL
Simple graph ql backend

## Examples
- [Querying](./querying.README.md)
- [Mutating](./seeding.README.md)

## Running the application
Run aspire AppHost

## Notes to self:
```graphql
mutation user ($john:UserInput!){ // Decalre Variables reference at mutation level
  createUser(user:$john){ // Reference that model at mutation method type here
    id // Delcare what data it will return. Just like query.
  }
}
```
