## Overview of data model

So i dont lose track.

```
User
{
  Name: John
  Email: jonh.doe@gmail.com
  Phone: {
    countryCode: +45
    number: 000000000
  }
  Skills [
    { Title: Asp.Net core },
    { Title: MySql }
  ]
  Educations [
    { Level: HTX },
    { Level: Computer Science (AP) }
  ],
  Projects [
    { Title: Roo Wrath of gilgamesh },
    { Title: Chroma },
  ],
  Companies [
    { Organization: Tryg, Role: Backend Developer },
    { Organization: Viden Djurs, Role: Teacher }
  ]
}
```

This doesnt really make sense.
Skill needs to be more clearly defined or tied to a user maybe?
Or one-way relation between skill and their origin maybe.. :think:

Gotta think more about it but this is a good first scaffold.