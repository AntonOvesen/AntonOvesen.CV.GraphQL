# Seeding data query: (WIP)
Everything ressource is writable in the api

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