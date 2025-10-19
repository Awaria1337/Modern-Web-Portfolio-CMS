using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;

namespace Portfolio.Controllers
{
    public class AdminSosyalMedyaController : Controller
    {
        private readonly SiteContext _context;

        public AdminSosyalMedyaController(SiteContext context)
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
            var sosyalMedya = _context.SosyalMedya.OrderBy(s => s.Sira).ToList();
            return View(sosyalMedya);
        }

        public IActionResult Create()
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Admin");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SosyalMedya model)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Admin");
            if (ModelState.IsValid)
            {
                _context.SosyalMedya.Add(model);
                _context.SaveChanges();
                TempData["Success"] = "Sosyal medya başarıyla eklendi!";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Admin");
            var sosyal = _context.SosyalMedya.Find(id);
            if (sosyal == null) return NotFound();
            return View(sosyal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SosyalMedya model)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Admin");
            if (ModelState.IsValid)
            {
                _context.SosyalMedya.Update(model);
                _context.SaveChanges();
                TempData["Success"] = "Sosyal medya başarıyla güncellendi!";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        [Route("AdminSosyalMedya/Delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (!IsLoggedIn()) return Json(new { success = false });
            var sosyal = _context.SosyalMedya.Find(id);
            if (sosyal != null)
            {
                _context.SosyalMedya.Remove(sosyal);
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}
