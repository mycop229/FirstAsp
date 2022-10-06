using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

		[HttpPost]
		public async Task<IActionResult> Sorting(string sort, string brand, int gender, int category, string size)
		{
			HomeWM homeWM = new();

			//Сортировка по возрастанию стоимости
			if (sort == "Ascending" & brand == null & gender == 0 & category == 0 & size == null)
			{
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).OrderBy(u => u.Price).ToListAsync();
				homeWM.Categories = _db.Category;
				return View(homeWM);
			}
			//Сортировка по убыванию стоимости
			if (sort == "Descending" & brand == null & gender == 0 & category == 0 & size == null)
			{
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).OrderByDescending(u => u.Price).ToListAsync();
				homeWM.Categories = _db.Category;
				return View(homeWM);
			}
			//Сортировка по стоимости + пол
			if (sort != null & gender != 0 & brand == null & category == 0 & size == null)
			{
				if (sort == "Ascending")
				{
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.ApplicationTypeId == gender).OrderBy(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
					return View(homeWM);
				}
				if (sort == "Descending")
				{
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.ApplicationTypeId == gender).OrderByDescending(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
					return View(homeWM);
				}
			}

			//Сортировка по бренду
			if (brand != null & sort == null & gender == 0 & category == 0 & size == null)
			{
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).ToListAsync();
				homeWM.Categories = _db.Category;
			}
			//Сортировка по бренду + стоимости
			if (brand != null & sort != null & gender == 0 & category == 0 & size == null)
			{
				if (sort == "Ascending")
				{
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).OrderBy(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
					return View(homeWM);
				}
				if (sort == "Descending")
				{
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).OrderByDescending(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
					return View(homeWM);
				}
			}
			//Сортировка по бренду + пол
			if (brand != null & gender != 0 & sort == null & category == 0 & size == null)
			{
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => u.ApplicationTypeId == gender).ToListAsync();
				homeWM.Categories = _db.Category;
			}

			//Сортировка по полу
			if (gender != 0 & brand == null & sort == null & category == 0 & size == null)
			{
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.ApplicationTypeId == gender).ToListAsync();
				homeWM.Categories = _db.Category;
				return View(homeWM);
			}
			//Сортировка по полу + стоимость
			if (gender != 0 & sort != null & brand == null & category == 0 & size == null)
			{
				if (sort == "Ascending")
				{
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.ApplicationTypeId == gender).OrderBy(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
					return View(homeWM);
				}
				if (sort == "Descending")
				{
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.ApplicationTypeId == gender).OrderByDescending(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
					return View(homeWM);
				}
			}
			//Сортировка по полу + стоимость + бренд
			if (gender != 0 & sort != null & brand != null & category == 0 & size == null)
			{
				if (sort == "Ascending")
				{
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.ApplicationTypeId == gender).Where(u => u.Brand == brand).OrderBy(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
					return View(homeWM);
				}
				if (sort == "Descending")
				{
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.ApplicationTypeId == gender).Where(u => u.Brand == brand).OrderByDescending(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
					return View(homeWM);
				}
			}
			//Сортировка по категории
			if (category != 0 & gender == 0 & sort == null & brand == null & size == null)
			{
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.CategoryId == category).ToListAsync();
				homeWM.Categories = _db.Category;
				return View(homeWM);
			}
			//Сортировка по категории + cтоиомсть
			if (category != 0 & sort != null & gender == 0 & brand == null & size == null)
			{
				if (sort == "Ascending")
				{
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.CategoryId == category).OrderBy(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
					return View(homeWM);
				}
				if (sort == "Descending")
				{
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.CategoryId == category).OrderByDescending(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
					return View(homeWM);
				}
			}
			//Сортировка по бренду + категория
			if (category != 0 & brand != null & gender == 0 & sort == null & size == null)
			{
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => u.CategoryId == category).ToListAsync();
				homeWM.Categories = _db.Category;
			}
			//Сортировка по полу + категория
			if (category != 0 & gender != 0 & sort == null & brand == null & size == null)
			{
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.CategoryId == category).Where(u => u.ApplicationTypeId == gender).ToListAsync();
				homeWM.Categories = _db.Category;
			}
			//Сортировка по стоимости + категории + бренду
			if (sort != null & category != 0 & gender == 0 & brand != null & size == null)
			{
				if (sort == "Ascending")
				{
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.CategoryId == category).Where(u => u.Brand == brand).OrderBy(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
					return View(homeWM);
				}
				if (sort == "Descending")
				{
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.CategoryId == category).Where(u => u.Brand == brand).OrderByDescending(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
					return View(homeWM);
				}
			}
			//Сортировка по стоимость + категория + пол
			if (sort != null & category != 0 & gender != 0 & brand == null & size == null)
			{
				if (sort == "Ascending")
				{
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.CategoryId == category).Where(u => u.ApplicationTypeId == gender).OrderBy(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
					return View(homeWM);
				}
				if (sort == "Descending")
				{
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.CategoryId == category).Where(u => u.ApplicationTypeId == gender).OrderByDescending(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
					return View(homeWM);
				}
			}
			//Сортировка по категории + стоимости + бренду + полу
			if (sort != null & category != 0 & gender != 0 & brand != null & size == null)
			{
				if (sort == "Ascending")
				{
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.CategoryId == category).Where(u => u.Brand == brand).Where(u => u.ApplicationTypeId == gender).OrderBy(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
					return View(homeWM);
				}
				if (sort == "Descending")
				{
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.CategoryId == category).Where(u => u.Brand == brand).Where(u => u.ApplicationTypeId == gender).OrderByDescending(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
					return View(homeWM);
				}
			}
			//Сортировка по категории + полу + бренду
			if (category != 0 & brand != null & gender != 0 & sort == null & size == null)
			{
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.CategoryId == category).Where(u => u.Brand == brand).Where(u => u.ApplicationTypeId == gender).ToListAsync();
				homeWM.Categories = _db.Category;
				return View(homeWM);
			}
			//Сортировка по категории + стоимости + бренду
			if (category != 0 & brand != null & sort != null & gender == 0 & size == null)
			{
				if (sort == "Ascending")
				{
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.CategoryId == category).Where(u => u.Brand == brand).OrderBy(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
					return View(homeWM);
				}
				if (sort == "Descending")
				{
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.CategoryId == category).Where(u => u.Brand == brand).OrderByDescending(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
					return View(homeWM);
				}
			}




			//Сортировка по размеру
			if (category == 0 & brand == null & sort == null & gender == 0 & size != null)
			{
				return View(await SortWithSizeAsync(size));
			}
			//Сортировка по стоимости + размер
			if(category == 0 & brand == null & sort != null & gender == 0 & size != null)
            {
				return (View(await SortWithSizeSecondAsync(sort, size)));
			}
			//Сортировка по категории + размер
			if(category != 0 & brand == null & sort == null & gender == 0 & size != null)
            {
				return (View(await SortWithSizeSecondAsynс(category, size)));
			}
			//Сортировка по размеру + бренду
			if(category == 0 & brand != null & sort == null & gender == 0 & size != null)
            {
				return View(await SortWithSizeAsync(size, brand));
            }
			//Сортировка по размеру и полу
			if(category == 0 & brand == null & sort == null & gender != 0 & size != null)
            {
				return View(await SortWithSizeAsync(size, gender));
			}
			//Сортировка по размеру + полу + бренду
			if(category == 0 & brand != null & sort == null & gender != 0 & size != null)
            {
				return (View(await SortWithSizeAsync(size,gender,brand)));
            }
			//Сортировка по бренду + стоимости + размеру + полу
			if(category == 0 & brand != null & sort != null & gender != 0 & size != null)
            {
				return (View(await SortWithSizeAsync(brand, sort, gender, size)));
			}
			//Сортировка по стоимости + бренду + полу + размеру + категории
			if(category != 0 & brand != null & sort != null & gender != 0 & size != null)
            {
				return (View(await SortWithSizeAsync(brand, sort, gender, size, category)));
			}
			//Сортировка по стоимости + бренду + размеру
			if(category == 0 & brand != null & sort != null & size != null & gender == 0)
            {
				return (View(await SortWithSizeAsync(brand, sort, size)));
			}
			//Сортировка по полу + категории + размеру
			if(category != 0 & brand == null & sort == null & size != null & gender != 0)
            {
				return (View(await SortWithSizeSecondAsynс(gender, category, size)));
			}
			//Сортировка по полу + категории + размеру + бренду
			if(category != 0 & brand != null & sort == null & size != null & gender != 0)
            {
				return (View(await SortWithSizeSecondAsynс(gender, category, size, brand)));
			}
			//Сортировка по полу + размеру + бренду
			if(category == 0 & brand != null & sort == null & size != null & gender != 0)
            {
				return (View(await SortWithSizeSecondAsynс(gender, size, brand)));
			}
			//Сортировка по размеру + категории + бренду
			if(category != 0 & brand != null & sort == null & size != null & gender == 0)
            {
				return (View(await SortWithSizeThirdAsync(category, brand, size)));
			}
			//Сортировка по стоимости + размер + категория
			if(category != 0 & brand == null & sort != null & size != null & gender == 0)
            {
				return (View(await SortWithSizeThirdAsync(sort, size, category)));
			}
			//Сортировка по стоимости + полу + размеру
			if(category == 0 & brand == null & sort != null & size != null & gender != 0)
            {
				return (View(await SortWithSizeFourthAsync(sort, size, gender)));
			}
			//Сортировка по стоимости + размеру + бренду + категории
			if(category != 0 & brand != null & sort != null & size != null & gender == 0)
            {
				return (View(await SortWithSizeFourthAsync(category, brand, sort, size)));
			}




			//Нет сортировки
			if(category == 0 & brand == null & sort == null & size == null & gender == 0)
            {
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).OrderBy(u => u.Price).ToListAsync();
				homeWM.Categories = _db.Category;
				return View(homeWM);
			}


			return View(homeWM);
		
		}

		public async Task<HomeWM> SortWithSizeAsync(string size)
        {
			HomeWM homeWM = new();
			if (size == "XS")
			{
				var searchIds = _db.Article.Where(u => u.XS > 0).Select(u => u.ArticleName).ToList();
				List<int> IdArt = searchIds.Select(int.Parse).ToList();
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
				homeWM.Categories = _db.Category;
				
			}
			if (size == "S")
			{
				var searchIds = _db.Article.Where(u => u.S > 0).Select(u => u.ArticleName).ToList();
				List<int> IdArt = searchIds.Select(int.Parse).ToList();
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
				homeWM.Categories = _db.Category;
				
			}
			if (size == "M")
			{
				var searchIds = _db.Article.Where(u => u.M > 0).Select(u => u.ArticleName).ToList();
				List<int> IdArt = searchIds.Select(int.Parse).ToList();
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
				homeWM.Categories = _db.Category;
			}
			if (size == "L")
			{
				var searchIds = _db.Article.Where(u => u.L > 0).Select(u => u.ArticleName).ToList();
				List<int> IdArt = searchIds.Select(int.Parse).ToList();
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
				homeWM.Categories = _db.Category;
			}
			if (size == "XL")
			{
				var searchIds = _db.Article.Where(u => u.XL > 0).Select(u => u.ArticleName).ToList();
				List<int> IdArt = searchIds.Select(int.Parse).ToList();
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
				homeWM.Categories = _db.Category;
			}
			if (size == "XXL")
			{
				var searchIds = _db.Article.Where(u => u.XXL > 0).Select(u => u.ArticleName).ToList();
				List<int> IdArt = searchIds.Select(int.Parse).ToList();
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
				homeWM.Categories = _db.Category;
			}
			return homeWM;
		} //Сортировка по размеру 
		public async Task<HomeWM> SortWithSizeAsync(string size, string brand)
		{
			HomeWM homeWM = new();
			if (size == "XS")
			{
				var searchIds = _db.Article.Where(u => u.XS > 0).Select(u => u.ArticleName).ToList();
				List<int> IdArt = searchIds.Select(int.Parse).ToList();
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
				homeWM.Categories = _db.Category;

			}
			if (size == "S")
			{
				var searchIds = _db.Article.Where(u => u.S > 0).Select(u => u.ArticleName).ToList();
				List<int> IdArt = searchIds.Select(int.Parse).ToList();
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
				homeWM.Categories = _db.Category;

			}
			if (size == "M")
			{
				var searchIds = _db.Article.Where(u => u.M > 0).Select(u => u.ArticleName).ToList();
				List<int> IdArt = searchIds.Select(int.Parse).ToList();
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
				homeWM.Categories = _db.Category;
			}
			if (size == "L")
			{
				var searchIds = _db.Article.Where(u => u.L > 0).Select(u => u.ArticleName).ToList();
				List<int> IdArt = searchIds.Select(int.Parse).ToList();
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
				homeWM.Categories = _db.Category;
			}
			if (size == "XL")
			{
				var searchIds = _db.Article.Where(u => u.XL > 0).Select(u => u.ArticleName).ToList();
				List<int> IdArt = searchIds.Select(int.Parse).ToList();
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
				homeWM.Categories = _db.Category;
			}
			if (size == "XXL")
			{
				var searchIds = _db.Article.Where(u => u.XXL > 0).Select(u => u.ArticleName).ToList();
				List<int> IdArt = searchIds.Select(int.Parse).ToList();
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
				homeWM.Categories = _db.Category;
			}
			return homeWM;
		} //Сортировка по размеру + бренду
		public async Task<HomeWM> SortWithSizeAsync(string size, int gender)
		{
			HomeWM homeWM = new();
			if (size == "XS")
			{
				var searchIds = _db.Article.Where(u => u.XS > 0).Select(u => u.ArticleName).ToList();
				List<int> IdArt = searchIds.Select(int.Parse).ToList();
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
				homeWM.Categories = _db.Category;

			}
			if (size == "S")
			{
				var searchIds = _db.Article.Where(u => u.S > 0).Select(u => u.ArticleName).ToList();
				List<int> IdArt = searchIds.Select(int.Parse).ToList();
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
				homeWM.Categories = _db.Category;

			}
			if (size == "M")
			{
				var searchIds = _db.Article.Where(u => u.M > 0).Select(u => u.ArticleName).ToList();
				List<int> IdArt = searchIds.Select(int.Parse).ToList();
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
				homeWM.Categories = _db.Category;
			}
			if (size == "L")
			{
				var searchIds = _db.Article.Where(u => u.L > 0).Select(u => u.ArticleName).ToList();
				List<int> IdArt = searchIds.Select(int.Parse).ToList();
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
				homeWM.Categories = _db.Category;
			}
			if (size == "XL")
			{
				var searchIds = _db.Article.Where(u => u.XL > 0).Select(u => u.ArticleName).ToList();
				List<int> IdArt = searchIds.Select(int.Parse).ToList();
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
				homeWM.Categories = _db.Category;
			}
			if (size == "XXL")
			{
				var searchIds = _db.Article.Where(u => u.XXL > 0).Select(u => u.ArticleName).ToList();
				List<int> IdArt = searchIds.Select(int.Parse).ToList();
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
				homeWM.Categories = _db.Category;
			}
			return homeWM;
		} //Сортировка по размеру + полу
		public async Task<HomeWM> SortWithSizeAsync(string size, int gender, string brand)
		{
			HomeWM homeWM = new();
			if (size == "XS")
			{
				var searchIds = _db.Article.Where(u => u.XS > 0).Select(u => u.ArticleName).ToList();
				List<int> IdArt = searchIds.Select(int.Parse).ToList();
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
				homeWM.Categories = _db.Category;

			}
			if (size == "S")
			{
				var searchIds = _db.Article.Where(u => u.S > 0).Select(u => u.ArticleName).ToList();
				List<int> IdArt = searchIds.Select(int.Parse).ToList();
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
				homeWM.Categories = _db.Category;

			}
			if (size == "M")
			{
				var searchIds = _db.Article.Where(u => u.M > 0).Select(u => u.ArticleName).ToList();
				List<int> IdArt = searchIds.Select(int.Parse).ToList();
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
				homeWM.Categories = _db.Category;
			}
			if (size == "L")
			{
				var searchIds = _db.Article.Where(u => u.L > 0).Select(u => u.ArticleName).ToList();
				List<int> IdArt = searchIds.Select(int.Parse).ToList();
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
				homeWM.Categories = _db.Category;
			}
			if (size == "XL")
			{
				var searchIds = _db.Article.Where(u => u.XL > 0).Select(u => u.ArticleName).ToList();
				List<int> IdArt = searchIds.Select(int.Parse).ToList();
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
				homeWM.Categories = _db.Category;
			}
			if (size == "XXL")
			{
				var searchIds = _db.Article.Where(u => u.XXL > 0).Select(u => u.ArticleName).ToList();
				List<int> IdArt = searchIds.Select(int.Parse).ToList();
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
				homeWM.Categories = _db.Category;
			}
			return homeWM;
		} //Сортировка по размеру + полу + бренду
		public async Task<HomeWM> SortWithSizeAsync(string brand, string sort, string size)
		{
			HomeWM homeWM = new();
			if (sort == "Ascending")
			{
				if (size == "XS")
				{
					var searchIds = _db.Article.Where(u => u.XS > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => IdArt.Contains(u.ArticleId)).OrderBy(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;

				}
				if (size == "S")
				{
					var searchIds = _db.Article.Where(u => u.S > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => IdArt.Contains(u.ArticleId)).OrderBy(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;

				}
				if (size == "M")
				{
					var searchIds = _db.Article.Where(u => u.M > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => IdArt.Contains(u.ArticleId)).OrderBy(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				if (size == "L")
				{
					var searchIds = _db.Article.Where(u => u.L > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => IdArt.Contains(u.ArticleId)).OrderBy(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				if (size == "XL")
				{
					var searchIds = _db.Article.Where(u => u.XL > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => IdArt.Contains(u.ArticleId)).OrderBy(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				if (size == "XXL")
				{
					var searchIds = _db.Article.Where(u => u.XXL > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => IdArt.Contains(u.ArticleId)).OrderBy(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
			}
			if (sort == "Descending")
			{
				if (size == "XS")
				{
					var searchIds = _db.Article.Where(u => u.XS > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => IdArt.Contains(u.ArticleId)).OrderByDescending(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;

				}
				if (size == "S")
				{
					var searchIds = _db.Article.Where(u => u.S > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => IdArt.Contains(u.ArticleId)).OrderByDescending(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;

				}
				if (size == "M")
				{
					var searchIds = _db.Article.Where(u => u.M > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => IdArt.Contains(u.ArticleId)).OrderByDescending(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				if (size == "L")
				{
					var searchIds = _db.Article.Where(u => u.L > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => IdArt.Contains(u.ArticleId)).OrderByDescending(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				if (size == "XL")
				{
					var searchIds = _db.Article.Where(u => u.XL > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => IdArt.Contains(u.ArticleId)).OrderByDescending(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				if (size == "XXL")
				{
					var searchIds = _db.Article.Where(u => u.XXL > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => IdArt.Contains(u.ArticleId)).OrderByDescending(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
			}
			return homeWM;
		} //Сортировка по стоимости + бренду + размеру
		public async Task<HomeWM> SortWithSizeAsync(string brand, string sort, int gender, string size)
		{
			HomeWM homeWM = new();
			if (sort == "Ascending")
			{
				if (size == "XS")
				{
					var searchIds = _db.Article.Where(u => u.XS > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).OrderBy(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;

				}
				if (size == "S")
				{
					var searchIds = _db.Article.Where(u => u.S > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).OrderBy(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;

				}
				if (size == "M")
				{
					var searchIds = _db.Article.Where(u => u.M > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).OrderBy(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				if (size == "L")
				{
					var searchIds = _db.Article.Where(u => u.L > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).OrderBy(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				if (size == "XL")
				{
					var searchIds = _db.Article.Where(u => u.XL > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).OrderBy(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				if (size == "XXL")
				{
					var searchIds = _db.Article.Where(u => u.XXL > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).OrderBy(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
			}
			if (sort == "Descending")
            {
				if (size == "XS")
				{
					var searchIds = _db.Article.Where(u => u.XS > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).OrderByDescending(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;

				}
				if (size == "S")
				{
					var searchIds = _db.Article.Where(u => u.S > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).OrderByDescending(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;

				}
				if (size == "M")
				{
					var searchIds = _db.Article.Where(u => u.M > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).OrderByDescending(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				if (size == "L")
				{
					var searchIds = _db.Article.Where(u => u.L > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).OrderByDescending(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				if (size == "XL")
				{
					var searchIds = _db.Article.Where(u => u.XL > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).OrderByDescending(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				if (size == "XXL")
				{
					var searchIds = _db.Article.Where(u => u.XXL > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).OrderByDescending(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
			}
			return homeWM;
		} //Сортировка по по бренду + стоимости + размеру + полу
		public async Task<HomeWM> SortWithSizeAsync(string brand, string sort, int gender, string size, int category)
		{
			HomeWM homeWM = new();
			if (sort == "Ascending")
			{
				if (size == "XS")
				{
					var searchIds = _db.Article.Where(u => u.XS > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.CategoryId == category).Where(u => u.Brand == brand).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).OrderBy(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;

				}
				if (size == "S")
				{
					var searchIds = _db.Article.Where(u => u.S > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.CategoryId == category).Where(u => u.Brand == brand).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).OrderBy(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;

				}
				if (size == "M")
				{
					var searchIds = _db.Article.Where(u => u.M > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.CategoryId == category).Where(u => u.Brand == brand).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).OrderBy(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				if (size == "L")
				{
					var searchIds = _db.Article.Where(u => u.L > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.CategoryId == category).Where(u => u.Brand == brand).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).OrderBy(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				if (size == "XL")
				{
					var searchIds = _db.Article.Where(u => u.XL > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.CategoryId == category).Where(u => u.Brand == brand).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).OrderBy(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				if (size == "XXL")
				{
					var searchIds = _db.Article.Where(u => u.XXL > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.CategoryId == category).Where(u => u.Brand == brand).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).OrderBy(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
			}
			if (sort == "Descending")
			{
				if (size == "XS")
				{
					var searchIds = _db.Article.Where(u => u.XS > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.CategoryId == category).Where(u => u.Brand == brand).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).OrderByDescending(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;

				}
				if (size == "S")
				{
					var searchIds = _db.Article.Where(u => u.S > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.CategoryId == category).Where(u => u.Brand == brand).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).OrderByDescending(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;

				}
				if (size == "M")
				{
					var searchIds = _db.Article.Where(u => u.M > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.CategoryId == category).Where(u => u.Brand == brand).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).OrderByDescending(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				if (size == "L")
				{
					var searchIds = _db.Article.Where(u => u.L > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.CategoryId == category).Where(u => u.Brand == brand).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).OrderByDescending(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				if (size == "XL")
				{
					var searchIds = _db.Article.Where(u => u.XL > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.CategoryId == category).Where(u => u.Brand == brand).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).OrderByDescending(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				if (size == "XXL")
				{
					var searchIds = _db.Article.Where(u => u.XXL > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.CategoryId == category).Where(u => u.Brand == brand).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).OrderByDescending(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
			}
			return homeWM;
		} //Сортировка по стоимости + бренду + полу + размеру + категории 


		public async Task<HomeWM> SortWithSizeSecondAsync(string sort, string size)
        {
			HomeWM homeWM = new();

			if (sort == "Ascending")
            {
				if (size == "XS")
				{
					var searchIds = _db.Article.Where(u => u.XS > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => IdArt.Contains(u.ArticleId)).OrderBy(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;

				}
				if (size == "S")
				{
					var searchIds = _db.Article.Where(u => u.S > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => IdArt.Contains(u.ArticleId)).OrderBy(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;

				}
				if (size == "M")
				{
					var searchIds = _db.Article.Where(u => u.M > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => IdArt.Contains(u.ArticleId)).OrderBy(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				if (size == "L")
				{
					var searchIds = _db.Article.Where(u => u.L > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => IdArt.Contains(u.ArticleId)).OrderBy(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				if (size == "XL")
				{
					var searchIds = _db.Article.Where(u => u.XL > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => IdArt.Contains(u.ArticleId)).OrderBy(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				if (size == "XXL")
				{
					var searchIds = _db.Article.Where(u => u.XXL > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => IdArt.Contains(u.ArticleId)).OrderBy(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				return homeWM;
			}
            if(sort == "Descending")
            {
				if (size == "XS")
				{
					var searchIds = _db.Article.Where(u => u.XS > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => IdArt.Contains(u.ArticleId)).OrderByDescending(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;

				}
				if (size == "S")
				{
					var searchIds = _db.Article.Where(u => u.S > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => IdArt.Contains(u.ArticleId)).OrderByDescending(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;

				}
				if (size == "M")
				{
					var searchIds = _db.Article.Where(u => u.M > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => IdArt.Contains(u.ArticleId)).OrderByDescending(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				if (size == "L")
				{
					var searchIds = _db.Article.Where(u => u.L > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => IdArt.Contains(u.ArticleId)).OrderByDescending(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				if (size == "XL")
				{
					var searchIds = _db.Article.Where(u => u.XL > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => IdArt.Contains(u.ArticleId)).OrderByDescending(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				if (size == "XXL")
				{
					var searchIds = _db.Article.Where(u => u.XXL > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => IdArt.Contains(u.ArticleId)).OrderByDescending(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				return homeWM;
			}
			return homeWM;
		} // Сортировка по стоимости + размеру
		public async Task<HomeWM> SortWithSizeSecondAsynс(int category, string size)
		{
			HomeWM homeWM = new();

				if (size == "XS")
				{
					var searchIds = _db.Article.Where(u => u.XS > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.CategoryId == category).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
					homeWM.Categories = _db.Category;

				}
				if (size == "S")
				{
					var searchIds = _db.Article.Where(u => u.S > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.CategoryId == category).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
					homeWM.Categories = _db.Category;

				}
				if (size == "M")
				{
					var searchIds = _db.Article.Where(u => u.M > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.CategoryId == category).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				if (size == "L")
				{
					var searchIds = _db.Article.Where(u => u.L > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.CategoryId == category).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				if (size == "XL")
				{
					var searchIds = _db.Article.Where(u => u.XL > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.CategoryId == category).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				if (size == "XXL")
				{
					var searchIds = _db.Article.Where(u => u.XXL > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.CategoryId == category).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				return homeWM;
		} // Сортировка по категория + размер
		public async Task<HomeWM> SortWithSizeSecondAsynс(int gender, int category, string size)
		{
			HomeWM homeWM = new();

			if (size == "XS")
			{
				var searchIds = _db.Article.Where(u => u.XS > 0).Select(u => u.ArticleName).ToList();
				List<int> IdArt = searchIds.Select(int.Parse).ToList();
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u=>u.ApplicationTypeId == gender).Where(u => u.CategoryId == category).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
				homeWM.Categories = _db.Category;

			}
			if (size == "S")
			{
				var searchIds = _db.Article.Where(u => u.S > 0).Select(u => u.ArticleName).ToList();
				List<int> IdArt = searchIds.Select(int.Parse).ToList();
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.ApplicationTypeId == gender).Where(u => u.CategoryId == category).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
				homeWM.Categories = _db.Category;

			}
			if (size == "M")
			{
				var searchIds = _db.Article.Where(u => u.M > 0).Select(u => u.ArticleName).ToList();
				List<int> IdArt = searchIds.Select(int.Parse).ToList();
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.ApplicationTypeId == gender).Where(u => u.CategoryId == category).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
				homeWM.Categories = _db.Category;
			}
			if (size == "L")
			{
				var searchIds = _db.Article.Where(u => u.L > 0).Select(u => u.ArticleName).ToList();
				List<int> IdArt = searchIds.Select(int.Parse).ToList();
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.ApplicationTypeId == gender).Where(u => u.CategoryId == category).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
				homeWM.Categories = _db.Category;
			}
			if (size == "XL")
			{
				var searchIds = _db.Article.Where(u => u.XL > 0).Select(u => u.ArticleName).ToList();
				List<int> IdArt = searchIds.Select(int.Parse).ToList();
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.ApplicationTypeId == gender).Where(u => u.CategoryId == category).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
				homeWM.Categories = _db.Category;
			}
			if (size == "XXL")
			{
				var searchIds = _db.Article.Where(u => u.XXL > 0).Select(u => u.ArticleName).ToList();
				List<int> IdArt = searchIds.Select(int.Parse).ToList();
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.ApplicationTypeId == gender).Where(u => u.CategoryId == category).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
				homeWM.Categories = _db.Category;
			}
			return homeWM;
		} // Сортировка по категория + размер + полу
		public async Task<HomeWM> SortWithSizeSecondAsynс(int gender, int category, string size, string brand)
		{
			HomeWM homeWM = new();

			if (size == "XS")
			{
				var searchIds = _db.Article.Where(u => u.XS > 0).Select(u => u.ArticleName).ToList();
				List<int> IdArt = searchIds.Select(int.Parse).ToList();
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => u.ApplicationTypeId == gender).Where(u => u.CategoryId == category).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
				homeWM.Categories = _db.Category;

			}
			if (size == "S")
			{
				var searchIds = _db.Article.Where(u => u.S > 0).Select(u => u.ArticleName).ToList();
				List<int> IdArt = searchIds.Select(int.Parse).ToList();
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => u.ApplicationTypeId == gender).Where(u => u.CategoryId == category).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
				homeWM.Categories = _db.Category;

			}
			if (size == "M")
			{
				var searchIds = _db.Article.Where(u => u.M > 0).Select(u => u.ArticleName).ToList();
				List<int> IdArt = searchIds.Select(int.Parse).ToList();
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => u.ApplicationTypeId == gender).Where(u => u.CategoryId == category).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
				homeWM.Categories = _db.Category;
			}
			if (size == "L")
			{
				var searchIds = _db.Article.Where(u => u.L > 0).Select(u => u.ArticleName).ToList();
				List<int> IdArt = searchIds.Select(int.Parse).ToList();
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => u.ApplicationTypeId == gender).Where(u => u.CategoryId == category).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
				homeWM.Categories = _db.Category;
			}
			if (size == "XL")
			{
				var searchIds = _db.Article.Where(u => u.XL > 0).Select(u => u.ArticleName).ToList();
				List<int> IdArt = searchIds.Select(int.Parse).ToList();
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => u.ApplicationTypeId == gender).Where(u => u.CategoryId == category).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
				homeWM.Categories = _db.Category;
			}
			if (size == "XXL")
			{
				var searchIds = _db.Article.Where(u => u.XXL > 0).Select(u => u.ArticleName).ToList();
				List<int> IdArt = searchIds.Select(int.Parse).ToList();
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => u.ApplicationTypeId == gender).Where(u => u.CategoryId == category).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
				homeWM.Categories = _db.Category;
			}
			return homeWM;
		} // Сортировка по категория + размер + полу + бренду
		public async Task<HomeWM> SortWithSizeSecondAsynс(int gender, string size, string brand)
		{
			HomeWM homeWM = new();

			if (size == "XS")
			{
				var searchIds = _db.Article.Where(u => u.XS > 0).Select(u => u.ArticleName).ToList();
				List<int> IdArt = searchIds.Select(int.Parse).ToList();
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
				homeWM.Categories = _db.Category;

			}
			if (size == "S")
			{
				var searchIds = _db.Article.Where(u => u.S > 0).Select(u => u.ArticleName).ToList();
				List<int> IdArt = searchIds.Select(int.Parse).ToList();
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
				homeWM.Categories = _db.Category;

			}
			if (size == "M")
			{
				var searchIds = _db.Article.Where(u => u.M > 0).Select(u => u.ArticleName).ToList();
				List<int> IdArt = searchIds.Select(int.Parse).ToList();
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
				homeWM.Categories = _db.Category;
			}
			if (size == "L")
			{
				var searchIds = _db.Article.Where(u => u.L > 0).Select(u => u.ArticleName).ToList();
				List<int> IdArt = searchIds.Select(int.Parse).ToList();
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
				homeWM.Categories = _db.Category;
			}
			if (size == "XL")
			{
				var searchIds = _db.Article.Where(u => u.XL > 0).Select(u => u.ArticleName).ToList();
				List<int> IdArt = searchIds.Select(int.Parse).ToList();
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
				homeWM.Categories = _db.Category;
			}
			if (size == "XXL")
			{
				var searchIds = _db.Article.Where(u => u.XXL > 0).Select(u => u.ArticleName).ToList();
				List<int> IdArt = searchIds.Select(int.Parse).ToList();
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
				homeWM.Categories = _db.Category;
			}
			return homeWM;
		} // Сортировка по полу + размер + бренду


		public async Task<HomeWM> SortWithSizeThirdAsync(int category, string brand, string size)
        {
			HomeWM homeWM = new();

			if (size == "XS")
			{
				var searchIds = _db.Article.Where(u => u.XS > 0).Select(u => u.ArticleName).ToList();
				List<int> IdArt = searchIds.Select(int.Parse).ToList();
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => u.CategoryId == category).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
				homeWM.Categories = _db.Category;

			}
			if (size == "S")
			{
				var searchIds = _db.Article.Where(u => u.S > 0).Select(u => u.ArticleName).ToList();
				List<int> IdArt = searchIds.Select(int.Parse).ToList();
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => u.CategoryId == category).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
				homeWM.Categories = _db.Category;

			}
			if (size == "M")
			{
				var searchIds = _db.Article.Where(u => u.M > 0).Select(u => u.ArticleName).ToList();
				List<int> IdArt = searchIds.Select(int.Parse).ToList();
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => u.CategoryId == category).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
				homeWM.Categories = _db.Category;
			}
			if (size == "L")
			{
				var searchIds = _db.Article.Where(u => u.L > 0).Select(u => u.ArticleName).ToList();
				List<int> IdArt = searchIds.Select(int.Parse).ToList();
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => u.CategoryId == category).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
				homeWM.Categories = _db.Category;
			}
			if (size == "XL")
			{
				var searchIds = _db.Article.Where(u => u.XL > 0).Select(u => u.ArticleName).ToList();
				List<int> IdArt = searchIds.Select(int.Parse).ToList();
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => u.CategoryId == category).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
				homeWM.Categories = _db.Category;
			}
			if (size == "XXL")
			{
				var searchIds = _db.Article.Where(u => u.XXL > 0).Select(u => u.ArticleName).ToList();
				List<int> IdArt = searchIds.Select(int.Parse).ToList();
				homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => u.CategoryId == category).Where(u => IdArt.Contains(u.ArticleId)).ToListAsync();
				homeWM.Categories = _db.Category;
			}
			return homeWM;
		}//Сортировка по размеру + категории + бренду
		public async Task<HomeWM> SortWithSizeThirdAsync(string sort, string size, int category)
		{
			HomeWM homeWM = new();

			if(sort == "Ascending")
            {
				if (size == "XS")
				{
					var searchIds = _db.Article.Where(u => u.XS > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.CategoryId == category).Where(u => IdArt.Contains(u.ArticleId)).OrderBy(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;

				}
				if (size == "S")
				{
					var searchIds = _db.Article.Where(u => u.S > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.CategoryId == category).Where(u => IdArt.Contains(u.ArticleId)).OrderBy(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;

				}
				if (size == "M")
				{
					var searchIds = _db.Article.Where(u => u.M > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.CategoryId == category).Where(u => IdArt.Contains(u.ArticleId)).OrderBy(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				if (size == "L")
				{
					var searchIds = _db.Article.Where(u => u.L > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.CategoryId == category).Where(u => IdArt.Contains(u.ArticleId)).OrderBy(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				if (size == "XL")
				{
					var searchIds = _db.Article.Where(u => u.XL > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.CategoryId == category).Where(u => IdArt.Contains(u.ArticleId)).OrderBy(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				if (size == "XXL")
				{
					var searchIds = _db.Article.Where(u => u.XXL > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.CategoryId == category).Where(u => IdArt.Contains(u.ArticleId)).OrderBy(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				return homeWM;
			}
			if(sort == "Descending")
            {
				if (size == "XS")
				{
					var searchIds = _db.Article.Where(u => u.XS > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.CategoryId == category).Where(u => IdArt.Contains(u.ArticleId)).OrderByDescending(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;

				}
				if (size == "S")
				{
					var searchIds = _db.Article.Where(u => u.S > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.CategoryId == category).Where(u => IdArt.Contains(u.ArticleId)).OrderByDescending(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;

				}
				if (size == "M")
				{
					var searchIds = _db.Article.Where(u => u.M > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.CategoryId == category).Where(u => IdArt.Contains(u.ArticleId)).OrderByDescending(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				if (size == "L")
				{
					var searchIds = _db.Article.Where(u => u.L > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.CategoryId == category).Where(u => IdArt.Contains(u.ArticleId)).OrderByDescending(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				if (size == "XL")
				{
					var searchIds = _db.Article.Where(u => u.XL > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.CategoryId == category).Where(u => IdArt.Contains(u.ArticleId)).OrderByDescending(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				if (size == "XXL")
				{
					var searchIds = _db.Article.Where(u => u.XXL > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.CategoryId == category).Where(u => IdArt.Contains(u.ArticleId)).OrderByDescending(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				return homeWM;
			}
			return homeWM;

		}//Сортировка по стоимости + размер + категория


		public async Task<HomeWM> SortWithSizeFourthAsync(string sort, string size, int gender)
        {
			HomeWM homeWM = new();

			if (sort == "Ascending")
			{
				if (size == "XS")
				{
					var searchIds = _db.Article.Where(u => u.XS > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).OrderBy(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;

				}
				if (size == "S")
				{
					var searchIds = _db.Article.Where(u => u.S > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).OrderBy(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;

				}
				if (size == "M")
				{
					var searchIds = _db.Article.Where(u => u.M > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).OrderBy(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				if (size == "L")
				{
					var searchIds = _db.Article.Where(u => u.L > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).OrderBy(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				if (size == "XL")
				{
					var searchIds = _db.Article.Where(u => u.XL > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).OrderBy(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				if (size == "XXL")
				{
					var searchIds = _db.Article.Where(u => u.XXL > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).OrderBy(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				return homeWM;
			}
			if (sort == "Descending")
			{
				if (size == "XS")
				{
					var searchIds = _db.Article.Where(u => u.XS > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).OrderByDescending(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;

				}
				if (size == "S")
				{
					var searchIds = _db.Article.Where(u => u.S > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).OrderByDescending(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;

				}
				if (size == "M")
				{
					var searchIds = _db.Article.Where(u => u.M > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).OrderByDescending(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				if (size == "L")
				{
					var searchIds = _db.Article.Where(u => u.L > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).OrderByDescending(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				if (size == "XL")
				{
					var searchIds = _db.Article.Where(u => u.XL > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).OrderByDescending(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				if (size == "XXL")
				{
					var searchIds = _db.Article.Where(u => u.XXL > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.ApplicationTypeId == gender).Where(u => IdArt.Contains(u.ArticleId)).OrderByDescending(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				return homeWM;
			}
			return homeWM;
		}//Сортировка по стоимости + полу + размеру
		public async Task<HomeWM> SortWithSizeFourthAsync(int category, string brand, string sort, string size)
		{
			HomeWM homeWM = new();
			if (sort == "Ascending")
			{
				if (size == "XS")
				{
					var searchIds = _db.Article.Where(u => u.XS > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => u.CategoryId == category).Where(u => IdArt.Contains(u.ArticleId)).OrderBy(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;

				}
				if (size == "S")
				{
					var searchIds = _db.Article.Where(u => u.S > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => u.CategoryId == category).Where(u => IdArt.Contains(u.ArticleId)).OrderBy(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;

				}
				if (size == "M")
				{
					var searchIds = _db.Article.Where(u => u.M > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => u.CategoryId == category).Where(u => IdArt.Contains(u.ArticleId)).OrderBy(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				if (size == "L")
				{
					var searchIds = _db.Article.Where(u => u.L > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => u.CategoryId == category).Where(u => IdArt.Contains(u.ArticleId)).OrderBy(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				if (size == "XL")
				{
					var searchIds = _db.Article.Where(u => u.XL > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => u.CategoryId == category).Where(u => IdArt.Contains(u.ArticleId)).OrderBy(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				if (size == "XXL")
				{
					var searchIds = _db.Article.Where(u => u.XXL > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => u.CategoryId == category).Where(u => IdArt.Contains(u.ArticleId)).OrderBy(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
			}
			if (sort == "Descending")
			{
				if (size == "XS")
				{
					var searchIds = _db.Article.Where(u => u.XS > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => u.CategoryId == category).Where(u => IdArt.Contains(u.ArticleId)).OrderByDescending(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;

				}
				if (size == "S")
				{
					var searchIds = _db.Article.Where(u => u.S > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => u.CategoryId == category).Where(u => IdArt.Contains(u.ArticleId)).OrderByDescending(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;

				}
				if (size == "M")
				{
					var searchIds = _db.Article.Where(u => u.M > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => u.CategoryId == category).Where(u => IdArt.Contains(u.ArticleId)).OrderByDescending(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				if (size == "L")
				{
					var searchIds = _db.Article.Where(u => u.L > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => u.CategoryId == category).Where(u => IdArt.Contains(u.ArticleId)).OrderByDescending(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				if (size == "XL")
				{
					var searchIds = _db.Article.Where(u => u.XL > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => u.CategoryId == category).Where(u => IdArt.Contains(u.ArticleId)).OrderByDescending(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
				if (size == "XXL")
				{
					var searchIds = _db.Article.Where(u => u.XXL > 0).Select(u => u.ArticleName).ToList();
					List<int> IdArt = searchIds.Select(int.Parse).ToList();
					homeWM.Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Brand == brand).Where(u => u.CategoryId == category).Where(u => IdArt.Contains(u.ArticleId)).OrderByDescending(u => u.Price).ToListAsync();
					homeWM.Categories = _db.Category;
				}
			}
			return homeWM;
		} //Сортировка по стоимости + размеру + бренду + категории


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
