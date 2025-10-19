using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;

namespace Portfolio.Controllers
{
    public class AdminProfileController : Controller
    {
        private readonly SiteContext _context;

        public AdminProfileController(SiteContext context)
        {
            _context = context;
        }

        // Giriş kontrolü
        private bool IsLoggedIn()
        {
            return HttpContext.Session.GetString("AdminId") != null;
        }

        // GET: Index
        public IActionResult Index()
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Admin");

            var profile = _context.Profile.FirstOrDefault();
            return View(profile);
        }

        // GET: Edit
        public IActionResult Edit()
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Admin");

            var profile = _context.Profile.FirstOrDefault();
            if (profile == null)
            {
                profile = new Profile();
            }
            return View(profile);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Profile model)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Admin");

            if (ModelState.IsValid)
            {
                try
                {
                    var existing = _context.Profile.FirstOrDefault();
                    if (existing == null)
                    {
                        _context.Profile.Add(model);
                    }
                    else
                    {
                        existing.Ad = model.Ad;
                        existing.Soyad = model.Soyad;
                        existing.Unvan = model.Unvan;
                        existing.ProfilFoto = model.ProfilFoto;
                        existing.Email = model.Email;
                        existing.Telefon = model.Telefon;
                        existing.DogumTarihi = model.DogumTarihi;
                        existing.Lokasyon = model.Lokasyon;
                    }

                    _context.SaveChanges();
                    TempData["Success"] = "Profil bilgileri başarıyla güncellendi!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Bir hata oluştu: " + ex.Message);
                }
            }

            return View(model);
        }
    }
}
