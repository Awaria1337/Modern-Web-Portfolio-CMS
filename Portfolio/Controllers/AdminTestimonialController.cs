using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;

namespace Portfolio.Controllers
{
    public class AdminTestimonialController : Controller
    {
        private readonly SiteContext _context;

        public AdminTestimonialController(SiteContext context)
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
            var testimonials = _context.Testimonial.OrderBy(t => t.Sira).ToList();
            return View(testimonials);
        }

        public IActionResult Create()
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Admin");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Testimonial model)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Admin");
            if (ModelState.IsValid)
            {
                model.Tarih = DateTime.Now;
                _context.Testimonial.Add(model);
                _context.SaveChanges();
                TempData["Success"] = "Testimonial başarıyla eklendi!";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Admin");
            var testimonial = _context.Testimonial.Find(id);
            if (testimonial == null) return NotFound();
            return View(testimonial);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Testimonial model)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Admin");
            if (ModelState.IsValid)
            {
                _context.Testimonial.Update(model);
                _context.SaveChanges();
                TempData["Success"] = "Testimonial başarıyla güncellendi!";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        [Route("AdminTestimonial/Delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (!IsLoggedIn()) return Json(new { success = false });
            var testimonial = _context.Testimonial.Find(id);
            if (testimonial != null)
            {
                _context.Testimonial.Remove(testimonial);
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}
