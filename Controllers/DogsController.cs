﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using DogGo.Models;
using DogGo.Repositories;
using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace DogGo.Controllers
{
	public class DogsController : Controller
	{
		// GET: DogsController
		[Authorize]
		public ActionResult Index()
		{
			int ownerId = GetCurrentUserId();
			List<Dog> dogs = _dogRepo.GetDogsByOwnerId(ownerId);
			return View(dogs);
		}

		// GET: DogsController/Details/5
		public ActionResult Details(int id)
		{
			Dog dog = _dogRepo.GetDogById(id);
			if (dog == null)
			{
				return NotFound();
			}
			return View(dog);
		}

		// GET: DogsController/Create
		[Authorize]
		public ActionResult Create()
		{
			return View();
		}

		// POST: DogsController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Dog dog)
		{
			try
			{
				dog.OwnerId = GetCurrentUserId();
				_dogRepo.AddDog(dog);
				return RedirectToAction("Index");
			}
			catch (Exception)
			{
				return View(dog);
			}
		}

		// GET: DogsController/Edit/5
		public ActionResult Edit(int id)
		{
			Dog dog = _dogRepo.GetDogById(id);
			if (dog == null)
			{
				return NotFound();
			}

			return View(dog);

		}

		// POST: DogsController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, Dog dog)
		{
			try
			{
				_dogRepo.UpdateDog(dog);
				return RedirectToAction("Index");
			}
			catch (Exception)
			{
				return View(dog);
			}
		}

		// GET: DogsController/Delete/5
		public ActionResult Delete(int id)
		{
			Dog dog = _dogRepo.GetDogById(id);
			return View(dog);
		}

		// POST: DogsController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, Dog dog)
		{
			try
			{
				_dogRepo.DeleteDog(id);
				return RedirectToAction("Index");
			}
			catch (Exception)
			{
				return View(dog);
			}
		}

		private readonly IDogRepository _dogRepo;
		public DogsController(IDogRepository dogRepository)
		{
			_dogRepo = dogRepository;
		}

		private int GetCurrentUserId()
		{
			string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
			return int.Parse(id);
		}


	}
}
