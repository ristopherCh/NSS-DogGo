﻿using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using DogGo.Models;

namespace DogGo.Repositories
{
	public interface IDogRepository
	{
		List<Dog> GetAllDogs();
		Dog GetDogById(int id);
		void AddDog(Dog dog);
		void UpdateDog(Dog dog);
		void DeleteDog(int dogId);
		List<Dog> GetDogsByOwnerId(int ownerId);
	}
}
