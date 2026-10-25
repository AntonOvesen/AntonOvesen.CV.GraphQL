# Querying 
A list of example queries

```graphql
{
  users(searchTerm: "") {
    id
    firstName
    lastName
    email
    phone {
      number
      countryCode
    }
    educations {
      name
      level
      user {id}
      institution {
        id
        name
        website
      }
    }
    companies {
      id
      name
      founded
      website
      from
      to
      user {id}
    }
    skills {
      id
      title
      description
      user {id}
    }
    projects {
    	id
      title
      description
      ref
      user {id}
    }
  }
}
```