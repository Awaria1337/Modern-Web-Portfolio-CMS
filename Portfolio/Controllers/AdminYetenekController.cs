using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;

namespace Portfolio.Controllers
{
    public class AdminYetenekController : Controller
    {
        private readonly SiteContext _context;

        public AdminYetenekController(SiteContext context)
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
            var yetenekler = _context.Yetenek.OrderBy(y => y.Sira).ToList();
            return View(yetenekler);
        }

        public IActionResult Create()
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Admin");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Yetenek model)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Admin");
            if (ModelState.IsValid)
            {
                _context.Yetenek.Add(model);
                _context.SaveChanges();
                TempData["Success"] = "Yetenek başarıyla eklendi!";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Admin");
            var yetenek = _context.Yetenek.Find(id);
            if (yetenek == null) return NotFound();
            return View(yetenek);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Yetenek model)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Admin");
            if (ModelState.IsValid)
            {
                _context.Yetenek.Update(model);
                _context.SaveChanges();
                TempData["Success"] = "Yetenek başarıyla güncellendi!";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        [Route("AdminYetenek/Delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (!IsLoggedIn()) return Json(new { success = false });
            var yetenek = _context.Yetenek.Find(id);
            if (yetenek != null)
            {
                _context.Yetenek.Remove(yetenek);
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}
