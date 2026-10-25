# Seeding data query: (WIP)
Everything ressource is writable in the api

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
    "firstName": "Anton",
    "lastName": "Ovesen",
    "email": "anton@ovese-mail.dk",
    "phone": {
      "number": "51929765",
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