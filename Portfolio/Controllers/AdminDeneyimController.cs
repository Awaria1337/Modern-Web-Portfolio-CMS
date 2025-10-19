using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;

namespace Portfolio.Controllers
{
    public class AdminDeneyimController : Controller
    {
        private readonly SiteContext _context;

        public AdminDeneyimController(SiteContext context)
        {
            _context = context;
        }

        private bool IsLoggedIn()
        {
            return HttpContext.Session.GetString("AdminId") != null;
        }

        public IActionResult Index()
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Admin");
            var deneyimler = _context.Deneyim.OrderBy(d => d.Sira).ToList();
            return View(deneyimler);
        }

        public IActionResult Create()
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Admin");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Deneyim model)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Admin");
            if (ModelState.IsValid)
            {
                _context.Deneyim.Add(model);
                _context.SaveChanges();
                TempData["Success"] = "Deneyim başarıyla eklendi!";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Admin");
            var deneyim = _context.Deneyim.Find(id);
            if (deneyim == null) return NotFound();
            return View(deneyim);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Deneyim model)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Admin");
            if (ModelState.IsValid)
            {
                _context.Deneyim.Update(model);
                _context.SaveChanges();
                TempData["Success"] = "Deneyim başarıyla güncellendi!";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        [Route("AdminDeneyim/Delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (!IsLoggedIn()) return Json(new { success = false });
            var deneyim = _context.Deneyim.Find(id);
            if (deneyim != null)
            {
                _context.Deneyim.Remove(deneyim);
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}
