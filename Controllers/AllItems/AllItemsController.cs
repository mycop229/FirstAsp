using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using Tor.Data;
using Tor.Models;
using Tor.Models.ViewModels;

namespace Tor.Controllers.ListItems
{
	
	public class AllItemsController : Controller
	{
		private readonly ApplicationDbContext _db;

		public AllItemsController(ApplicationDbContext db)
		{
			_db = db;
		}

		[HttpGet]
		public IActionResult Hodie()
		{
			HomeWM homeWM = new()
			{
				Products = _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Category.Name == "Hodie"),
				Categories = _db.Category
			};

			return View(homeWM);
		}

		[HttpGet]
		public IActionResult TShirt()
		{
			HomeWM homeWM = new()
			{
				Products = _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Category.Name == "TShirt"),
				Categories = _db.Category
			};

			return View(homeWM);
		}

		[HttpGet]
		public IActionResult Sneakers()
		{
			HomeWM homeWM = new()
			{
				Products = _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Category.Name == "Sneakers"),
				Categories = _db.Category
			};

			return View(homeWM);
		}

		[HttpGet]
		public IActionResult Bags()
		{
			HomeWM homeWM = new()
			{
				Products = _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Category.Name == "Bags"),
				Categories = _db.Category
			};

			return View(homeWM);
		}
	}
}
