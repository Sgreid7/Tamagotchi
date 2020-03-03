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

    [HttpGet]
    public List<Pet> GetAllPets()
    {
      var pets = db.Pets.OrderBy(pet => pet.Name);
      return pets.ToList();
    }

    [HttpGet("{id}")]
    public Pet GetSinglePet(int id)
    {
      var pet = db.Pets.FirstOrDefault(pet => pet.Id == id);
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
      var petToPlayWith = db.Pets.FirstOrDefault(pet => pet.Id == id);
      petToPlayWith.HappinessLevel += 5;
      petToPlayWith.HungerLevel += 3;
      db.SaveChanges();
      return petToPlayWith;
    }

    [HttpPut("{id}/feed")]
    public Pet FeedPet(int id)
    {
      var petToFeed = db.Pets.FirstOrDefault(pet => pet.Id == id);
      petToFeed.HungerLevel -= 5;
      petToFeed.HappinessLevel += 3;
      db.SaveChanges();
      return petToFeed;
    }

    [HttpPut("{id}/scold")]
    public Pet ScoldPet(int id)
    {
      var petToScold = db.Pets.FirstOrDefault(pet => pet.Id == id);
      petToScold.HappinessLevel -= 5;
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