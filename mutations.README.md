# Mutation examples
Every ressource consists of a list-search, write, & delete.

## Adding user
```graphql
mutation userAdd($anton: UserInput!) {
  user {
    create(user: $anton) {
      id
      firstName
    }
  }
}
```
```json
# Variables
{
  "anton": {
    "firstName": "John",
    "lastName": "Doe",
    "email": "john.doe@gmail.com",
    "phone": {
      "number": "00000000",
      "countryCode": "+45"
    }
  }
}
```

## Adding education
### Institutions
```graphql
mutation addInstitutions($dania: InstitutionInput!, $videndjurs: InstitutionInput!) {
  dania: institution {
    create(institution: $dania) {
      id
      name
    }
  }
  videndjurs: institution {
    create(institution: $videndjurs) {
      id
      name
    }
  }
}
```
```json
# Variables
{
  "dania": {
    "name": "EA Dania",
    "website": "https://eadania.dk/"
  },
  "videndjurs": {
    "name": "Viden Djurs",
    "website": "https://videndjurs.dk/"
  }
}
```
### Educations

```graphql
mutation addEducation($datamatiker: EducationInput!, $htx: EducationInput!) {
  datamatiker: education {
    create(education: $datamatiker) {
      name
    }
  }
  htx: education {
    create(education: $htx) {
      name
    }
  }
}

query findInstitutions {
  institutions {
    id
    name
  }
  users {
    id
    firstName
    lastName
  }
}
```
```json
# Variables
{
  "datamatiker": {
    "name": "Datamatiker",
    "level": "Erhvervsakademiuddannelse",
    "institutionId": "<take from findInstitutions>",
    "userId": "<take from findInstitutions>"
  },
  "htx": {
    "name": "HTX",
    "level": "Gymnasiel Uddannelse",
    "institutionId": "<take from findInstitutions>",
    "userId": "<take from findInstitutions>"
  }
}
```