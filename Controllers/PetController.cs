using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tamagotchi.Models;

namespace Tamagotchi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PetController : ControllerBase
  {
    public DatabaseContext db { get; set; } = new DatabaseContext();
    public Random rnd { get; set; } = new Random();

    [HttpGet]
    public List<Pet> GetAllPets()
    {
      var pets = db.Pets.OrderBy(pet => pet.Name);
      foreach (var pet in pets)
      {
        pet.LastInteractedWithDate = DateTime.Now;
      }
      return pets.ToList();
    }

    [HttpGet("/all/alive")]
    public List<Pet> GetAllAlivePets()
    {
      var pets = db.Pets.Where(pet => pet.IsDead == false);
      foreach (var pet in pets)
      {
        pet.LastInteractedWithDate = DateTime.Now;
      }
      return pets.ToList();
    }

    [HttpGet("{id}")]
    public Pet GetSinglePet(int id)
    {
      var pet = db.Pets.FirstOrDefault(pet => pet.Id == id);
      pet.LastInteractedWithDate = DateTime.Now;
      return pet;
    }

    [HttpPost]
    public Pet CreatePet(Pet pet)
    {
      db.Pets.Add(pet);
      db.SaveChanges();
      return pet;
    }

    [HttpPut("{id}/play")]
    public Pet PlayWithPet(int id)
    {
      int result = rnd.Next(1, 100);
      var petToPlayWith = db.Pets.FirstOrDefault(pet => pet.Id == id);
      petToPlayWith.IsDead = false;
      // check if pet hasnt been interacted with in 3 days
      var daysSinceLastPlayed = DateTime.Now.Subtract(petToPlayWith.LastInteractedWithDate.Value).TotalDays;
      if (daysSinceLastPlayed >= 3)
      {
        // if so set death date to now - last interacted with
        petToPlayWith.DeathDate = DateTime.Now;
        petToPlayWith.IsDead = true;
      }
      // has a 10% chance
      if (result <= 10)
      {
        petToPlayWith.IsDead = true;
        petToPlayWith.DeathDate = DateTime.Now;
      }
      else
      {
        petToPlayWith.HappinessLevel += 5;
        petToPlayWith.HungerLevel += 3;
        petToPlayWith.LastInteractedWithDate = DateTime.Now;
      }
      db.SaveChanges();
      return petToPlayWith;
    }

    [HttpPut("{id}/feed")]
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

    [HttpPut("{id}/scold")]
    public Pet ScoldPet(int id)
    {
      int result = rnd.Next(1, 100);
      var petToScold = db.Pets.FirstOrDefault(pet => pet.Id == id);
      petToScold.IsDead = false;
      // check if pet hasnt been interacted with in 3 days
      var daysSinceLastPlayed = DateTime.Now.Subtract(petToScold.LastInteractedWithDate.Value).TotalDays;
      if (daysSinceLastPlayed >= 3)
      {
        // if so set death date to now - last interacted with
        petToScold.DeathDate = DateTime.Now;
        petToScold.IsDead = true;
      }
      if (result <= 10)
      {
        petToScold.IsDead = true;
        petToScold.DeathDate = DateTime.Now;
      }
      else
      {
        petToScold.HappinessLevel -= 5;
        petToScold.LastInteractedWithDate = DateTime.Now;
      }
      db.SaveChanges();
      return petToScold;
    }


    [HttpDelete("{id}")]
    public ActionResult DeletePet(int id)
    {
      var pet = db.Pets.FirstOrDefault(pet => pet.Id == id);
      db.Pets.Remove(pet);
      db.SaveChanges();
      return Ok();
    }
  }
}

