using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tor.Data;

namespace Tor.Controllers.ApplicationType
{
    public class ApplicationTypeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ApplicationTypeController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> ApplicationIndexAsync()
        {
            IEnumerable<Models.ApplicationType> response = await _db.ApplicationType.ToListAsync();
            return View(response);
        }

        [HttpGet]
        public IActionResult CreateApplication()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateApplication(Models.ApplicationType obj)
        {
            if (ModelState.IsValid)
            {
                await _db.ApplicationType.AddAsync(obj);
                await _db.SaveChangesAsync();
                return RedirectToAction("ApplicationIndex");
            }
            return View(obj);
        }
        
    }
}
