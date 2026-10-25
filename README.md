# AntonOvesen.CV.GraphQL
Simple graph ql backend

## Links
- [Querying](./querying.README.md)
- [Seeding](./seeding.README.md)


## Running the application
Run aspire AppHost

## TODO
- Add all the mutations so you can setup the ressources
- Fill out the seeding and query sections to help testing
- Add section about hosting. (maybe add docker compose?)


## Notes to self:
```graphql
mutation user ($john:UserInput!){ // Decalre Variables reference at mutation level
  createUser(user:$john){ // Reference that model at mutation method type here
    id // Delcare what data it will return. Just like query.
  }
}
```