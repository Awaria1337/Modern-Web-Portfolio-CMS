using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;

namespace Portfolio.Controllers
{
    public class AdminHakkimdaController : Controller
    {
        private readonly SiteContext _context;

        public AdminHakkimdaController(SiteContext context)
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
            var hakkimda = _context.Hakkimda.FirstOrDefault();
            return View(hakkimda);
        }

        public IActionResult Edit()
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Admin");
            var hakkimda = _context.Hakkimda.FirstOrDefault();
            if (hakkimda == null)
            {
                hakkimda = new Hakkimda();
            }
            return View(hakkimda);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Hakkimda model)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Admin");
            if (ModelState.IsValid)
            {
                var existing = _context.Hakkimda.FirstOrDefault();
                if (existing == null)
                {
                    _context.Hakkimda.Add(model);
                }
                else
                {
                    existing.Baslik = model.Baslik;
                    existing.Aciklama1 = model.Aciklama1;
                    existing.Aciklama2 = model.Aciklama2;
                }
                _context.SaveChanges();
                TempData["Success"] = "Hakkımda bilgileri başarıyla güncellendi!";
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
