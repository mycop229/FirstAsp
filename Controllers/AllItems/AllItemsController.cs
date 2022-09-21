using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Tor.Data;
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

		public async Task<IActionResult> Items(string name, string category)
        {
			HomeWM homeWM = new()
			{
				Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Category.Name == name).Where(u => u.ApplicationType.Name == category).ToListAsync(),
				Categories = _db.Category
			};

			return View(homeWM);
		}

		[ActionName("Brand")]
		public async Task<IActionResult> Items(string name)
        {
			HomeWM homeWM = new()
			{
				Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == name).ToListAsync(),
				Categories = _db.Category
			};

			return View(homeWM);
		}


































		//BRANDS
		public IActionResult Carhartt()
		{
			HomeWM homeWM = new()
			{
				Products = _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == "Carhartt"),
				Categories = _db.Category
			};

			return View(homeWM);
		}
		public IActionResult Nike()
		{
			HomeWM homeWM = new()
			{
				Products = _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == "Nike"),
				Categories = _db.Category
			};

			return View(homeWM);
		}
		public IActionResult Adidas()
		{
			HomeWM homeWM = new()
			{
				Products = _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == "Adidas"),
				Categories = _db.Category
			};

			return View(homeWM);
		}
		public IActionResult TheNorthFace()
		{
			HomeWM homeWM = new()
			{
				Products = _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == "The North Face"),
				Categories = _db.Category
			};

			return View(homeWM);
		}
		public IActionResult Puma()
		{
			HomeWM homeWM = new()
			{
				Products = _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == "Puma"),
				Categories = _db.Category
			};

			return View(homeWM);
		}
		public IActionResult Barbour()
		{
			HomeWM homeWM = new()
			{
				Products = _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == "Barbour"),
				Categories = _db.Category
			};

			return View(homeWM);
		}
		public IActionResult CalvinKlein()
		{
			HomeWM homeWM = new()
			{
				Products = _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == "CalvinKlein"),
				Categories = _db.Category
			};

			return View(homeWM);
		}
		public IActionResult Dickies()
		{
			HomeWM homeWM = new()
			{
				Products = _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == "Dickies"),
				Categories = _db.Category
			};

			return View(homeWM);
		}
	}
}
