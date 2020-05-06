# Tamagotchi

# Objectives

- Create an API that can CRUD against a Database
- Re-enforce SQL fundamentals
- One to many relationships

- [C#](https://docs.microsoft.com/en-us/dotnet/csharp/)
- [LINQ](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/)
- [EF CORE](https://docs.microsoft.com/en-us/ef/core/)
- [POSTGRESQL](https://www.postgresql.org/)
- [CONTROLLERS](https://docs.microsoft.com/en-us/dotnet/api/system.web.mvc.controller?view=aspnet-mvc-5.2)
- [POSTMAN](https://www.postman.com/)
- [MVC](https://dotnet.microsoft.com/apps/aspnet/mvc)

# Featured Code

## API Endpoint

```JSX
[HttpPut("feed/{id}")]
    public Pet FeedPet(int id)
    {
      int result = rnd.Next(1, 100);
      var petToFeed = db.Pets.FirstOrDefault(pet => pet.Id == id);
      petToFeed.IsDead = false;

      //   check if pet hasnt been interacted with in 3 days
      var daysSinceLastPlayed = DateTime.Now.Subtract(petToFeed.LastInteractedWithDate.Value).TotalDays;
      if (daysSinceLastPlayed >= 3)
      {
        // if so set death date to now - last interacted with
        petToFeed.DeathDate = DateTime.Now;
        petToFeed.IsDead = true;
      }

      if (result <= 10)
      {
        petToFeed.IsDead = true;
        petToFeed.DeathDate = DateTime.Now;
      }

      else
      {
        petToFeed.HungerLevel -= 5;
        petToFeed.HappinessLevel += 3;
        petToFeed.LastInteractedWithDate = DateTime.Now;
      }
      db.SaveChanges();
      return petToFeed;
    }
```

# User Actions

- GET /api/pet, this returns all pets in the database.
- GET /api/pet/all/alive, returns all the pets that are alive.
- GET /api/pet/{id}, This returns the pet with the corresponding Id.
- POST /api/pet, This creates a new pet. The body of the request should contain the name of the pet.
- PUT /api/pet/play/{id}, This finds the pet by id, and add 5 to its happiness level and add 3 to its hungry level
- PUT /api/pet/feed/{id}, This finds the pet by id, and remove 5 from its hungry level and add 3 to its happiness level.
- PUT /api/pet/scold/{id}, This finds the pet by id, and remove 5 from its happiness level
- DELETE /api/pet/{id}, This deletes a pet from the database by Id
