# AntonOvesen.CV.GraphQL
Simple graph ql backend

# Seeding
```graphql

mutation ($user:UserInput!) {
  createUser(user: $user) {
    firstName
    lastName
    email
    phone {number countryCode}
  }
}

{
    "user": {
        "firstName": "John",
        "lastName": "Doe",
        "email": "john.doe@gmail.com",
        "phone": {
            "number": "88888888",
            "countryCode": "+45"
        }
    }
}
```

# Querying
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