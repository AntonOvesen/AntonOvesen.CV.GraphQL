# AntonOvesen.CV.GraphQL
Simple graph ql backend

## TODO
- Add all the mutations so you can setup the ressources
- Fill out the seeding and query sections to help testing
- Add section about hosting. (maybe add docker compose?)

# Seeding
```graphql

mutation ($userOne:UserInput!) {
  createUser(user: $userOne) {
    firstName
    lastName
    email
    phone {number countryCode}
  }
}

mutation 

# Variables
{
    "userOne": {
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