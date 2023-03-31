using DogGo.Models;
using DogGo.Models.ViewModels;
using DogGo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace DogGo.Controllers
{
	public class WalkersController : Controller
	{
		// GET: Walkers
		public ActionResult Index()
		{
			int userId = GetCurrentUserId();
			Owner owner = _ownerRepo.GetOwnerById(userId);
			List<Walker> walkers = _walkerRepo.GetAllWalkers();
			List<Walker> myWalkers;
			if (userId == 0)
			{
				myWalkers = walkers;
			}
			else
			{
				myWalkers = walkers.Where(walker => walker.NeighborhoodId == owner.NeighborhoodId).ToList();
			}

			return View(myWalkers);
		}

		// GET: Walkers/Details/5
		public ActionResult Details(int id)
		{
			Walker walker = _walkerRepo.GetWalkerById(id);
			List<Walk> walks = _walkerRepo.GetWalksByWalkerId(id);
			int walkingTime = _walkerRepo.GetWalkTimeByWalkerId(id);


			WalkerViewModel vm = new WalkerViewModel()
			{
				Walker = walker,
				Walks = walks,
				WalkTime = walkingTime
			};

			if (walker == null)
			{
				return NotFound();
			}

			return View(vm);
		}

		// GET: WalkersController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: WalkersController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(IFormCollection collection)
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

		// GET: WalkersController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: WalkersController/Edit/5
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

		// GET: WalkersController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: WalkersController/Delete/5
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

		private int GetCurrentUserId()
		{
			string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (id == null)
			{
				return 0;
			}

			return int.Parse(id);
		}

		private readonly IWalkerRepository _walkerRepo;
		private readonly IOwnerRepository _ownerRepo;

		// ASP.NET will give us an instance of our Walker Repository. This is called "Dependency Injection"
		public WalkersController(IWalkerRepository walkerRepository, IOwnerRepository ownerRepository)
		{
			_walkerRepo = walkerRepository;
			_ownerRepo = ownerRepository;
		}
	}
}
