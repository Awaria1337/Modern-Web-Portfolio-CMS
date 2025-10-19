using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;

namespace Portfolio.Controllers
{
    public class AdminClientController : Controller
    {
        private readonly SiteContext _context;

        public AdminClientController(SiteContext context)
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
            var clients = _context.Client.OrderBy(c => c.Sira).ToList();
            return View(clients);
        }

        public IActionResult Create()
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Admin");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Client model)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Admin");
            if (ModelState.IsValid)
            {
                _context.Client.Add(model);
                _context.SaveChanges();
                TempData["Success"] = "Client başarıyla eklendi!";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Admin");
            var client = _context.Client.Find(id);
            if (client == null) return NotFound();
            return View(client);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Client model)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Admin");
            if (ModelState.IsValid)
            {
                _context.Client.Update(model);
                _context.SaveChanges();
                TempData["Success"] = "Client başarıyla güncellendi!";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        [Route("AdminClient/Delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (!IsLoggedIn()) return Json(new { success = false });
            var client = _context.Client.Find(id);
            if (client != null)
            {
                _context.Client.Remove(client);
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}
