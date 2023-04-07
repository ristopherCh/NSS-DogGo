using DogGo.Models;
using DogGo.Models.ViewModels;
using DogGo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DogGo.Controllers
{
	public class WalksController : Controller
	{
		// GET: WalksController
		public ActionResult Index()
		{
			return View();
		}

		// GET: WalksController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: WalksController/Create
		public ActionResult Create()
		{
			List<Walker> walkers = _walkerRepo.GetAllWalkers();
			List<Dog> dogs = _dogRepo.GetAllDogs();

			WalkViewModel wvm = new WalkViewModel()
			{
				walkers = walkers,
				dogs = dogs
			};
			return View(wvm);
		}

		// POST: WalksController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Walk Walk)
		{
			try
			{

				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: WalksController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: WalksController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: WalksController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: WalksController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		private readonly IWalkerRepository _walkerRepo;
		private readonly IOwnerRepository _ownerRepo;
		private readonly IDogRepository _dogRepo;

		// ASP.NET will give us an instance of our Walker Repository. This is called "Dependency Injection"
		public WalksController(IWalkerRepository walkerRepository, IOwnerRepository ownerRepository, IDogRepository dogRepository)
		{
			_walkerRepo = walkerRepository;
			_ownerRepo = ownerRepository;
			_dogRepo = dogRepository;
		}
	}
}
