# Querying 
A list of example queries

```graphql

{
  users(searchTerm: "john") {
    firstName
    educations {
      name
      institution {
        name
        website
      }
    }
  }
}
```