# AntonOvesen.CV.GraphQL
Simple graph ql backend

## Links
- [Querying](./querying.README.md)
- [Seeding](./seeding.README.md)

## Running the application
Run aspire AppHost

## TODO
- Fill out the seeding and query sections to help testing


## Notes to self:
```graphql
mutation user ($john:UserInput!){ // Decalre Variables reference at mutation level
  createUser(user:$john){ // Reference that model at mutation method type here
    id // Delcare what data it will return. Just like query.
  }
}
```
