using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;

namespace Portfolio.Controllers
{
    public class AdminMesajController : Controller
    {
        private readonly SiteContext _context;

        public AdminMesajController(SiteContext context)
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
            var mesajlar = _context.Mesaj.OrderByDescending(m => m.Tarih).ToList();
            return View(mesajlar);
        }

        public IActionResult Details(int id)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Admin");
            var mesaj = _context.Mesaj.Find(id);
            if (mesaj == null) return NotFound();
            
            // Okundu işaretle
            if (!mesaj.Okundu)
            {
                mesaj.Okundu = true;
                _context.SaveChanges();
            }
            
            return View(mesaj);
        }

        [HttpPost]
        public IActionResult ToggleRead(int id)
        {
            if (!IsLoggedIn()) return Json(new { success = false });
            var mesaj = _context.Mesaj.Find(id);
            if (mesaj != null)
            {
                mesaj.Okundu = !mesaj.Okundu;
                _context.SaveChanges();
                return Json(new { success = true, okundu = mesaj.Okundu });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        [Route("AdminMesaj/Delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (!IsLoggedIn()) return Json(new { success = false });
            var mesaj = _context.Mesaj.Find(id);
            if (mesaj != null)
            {
                _context.Mesaj.Remove(mesaj);
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}
