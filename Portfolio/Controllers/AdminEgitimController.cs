using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;

namespace Portfolio.Controllers
{
    public class AdminEgitimController : Controller
    {
        private readonly SiteContext _context;

        public AdminEgitimController(SiteContext context)
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
            var egitimler = _context.Egitim.OrderBy(e => e.Sira).ToList();
            return View(egitimler);
        }

        public IActionResult Create()
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Admin");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Egitim model)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Admin");
            if (ModelState.IsValid)
            {
                _context.Egitim.Add(model);
                _context.SaveChanges();
                TempData["Success"] = "Eğitim başarıyla eklendi!";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Admin");
            var egitim = _context.Egitim.Find(id);
            if (egitim == null) return NotFound();
            return View(egitim);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Egitim model)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Admin");
            if (ModelState.IsValid)
            {
                _context.Egitim.Update(model);
                _context.SaveChanges();
                TempData["Success"] = "Eğitim başarıyla güncellendi!";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        [Route("AdminEgitim/Delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (!IsLoggedIn()) return Json(new { success = false });
            var egitim = _context.Egitim.Find(id);
            if (egitim != null)
            {
                _context.Egitim.Remove(egitim);
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}
