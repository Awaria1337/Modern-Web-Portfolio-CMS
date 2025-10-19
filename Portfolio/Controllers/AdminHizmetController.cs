using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;

namespace Portfolio.Controllers
{
    public class AdminHizmetController : Controller
    {
        private readonly SiteContext _context;

        public AdminHizmetController(SiteContext context)
        {
            _context = context;
        }

        private bool IsLoggedIn()
        {
            return HttpContext.Session.GetString("AdminId") != null;
        }

        // GET: Index
        public IActionResult Index()
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Admin");
            
            var hizmetler = _context.Hizmet.OrderBy(h => h.Sira).ToList();
            return View(hizmetler);
        }

        // GET: Create
        public IActionResult Create()
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Admin");
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Hizmet model)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Admin");

            if (ModelState.IsValid)
            {
                _context.Hizmet.Add(model);
                _context.SaveChanges();
                TempData["Success"] = "Hizmet başarıyla eklendi!";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Edit/5
        public IActionResult Edit(int id)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Admin");
            
            var hizmet = _context.Hizmet.Find(id);
            if (hizmet == null) return NotFound();
            return View(hizmet);
        }

        // POST: Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Hizmet model)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Admin");

            if (ModelState.IsValid)
            {
                _context.Hizmet.Update(model);
                _context.SaveChanges();
                TempData["Success"] = "Hizmet başarıyla güncellendi!";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // POST: Delete/5
        [HttpPost]
        [Route("AdminHizmet/Delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (!IsLoggedIn()) return Json(new { success = false });

            var hizmet = _context.Hizmet.Find(id);
            if (hizmet != null)
            {
                _context.Hizmet.Remove(hizmet);
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}
