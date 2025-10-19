using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;

namespace Portfolio.Controllers
{
    public class AdminController : Controller
    {
        private readonly SiteContext _context;

        public AdminController(SiteContext context)
        {
            _context = context;
        }

        // GET: /Admin/Login
        [HttpGet]
        public IActionResult Login()
        {
            // Eğer zaten giriş yapılmışsa dashboard'a yönlendir
            if (HttpContext.Session.GetString("AdminId") != null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        // POST: /Admin/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var admin = _context.AdminKullanici
                    .FirstOrDefault(a => a.KullaniciAdi == model.KullaniciAdi && a.Sifre == model.Sifre);

                if (admin != null)
                {
                    // Session'a kullanıcı bilgilerini kaydet
                    HttpContext.Session.SetString("AdminId", admin.Id.ToString());
                    HttpContext.Session.SetString("AdminAdi", admin.Adi ?? "");
                    HttpContext.Session.SetString("AdminSoyadi", admin.Soyadi ?? "");
                    HttpContext.Session.SetString("AdminKullaniciAdi", admin.KullaniciAdi ?? "");

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı!");
                }
            }
            return View(model);
        }

        // GET: /Admin/Index - Dashboard
        public IActionResult Index()
        {
            // Giriş kontrolü
            if (HttpContext.Session.GetString("AdminId") == null)
            {
                return RedirectToAction("Login");
            }

            ViewBag.AdminAdi = HttpContext.Session.GetString("AdminAdi");
            ViewBag.AdminSoyadi = HttpContext.Session.GetString("AdminSoyadi");
            
            // Dashboard istatistikleri
            ViewBag.ToplamProje = _context.Proje.Count(p => p.Aktif);
            ViewBag.ToplamYetenek = _context.Yetenek.Count(y => y.Aktif);
            ViewBag.ToplamMesaj = _context.Mesaj.Count();
            ViewBag.OkunmamisMesaj = _context.Mesaj.Count(m => !m.Okundu);
            ViewBag.ToplamBlog = _context.Blog.Count(b => b.Aktif);
            ViewBag.ToplamTestimonial = _context.Testimonial.Count(t => t.Aktif);
            ViewBag.ToplamHizmet = _context.Hizmet.Count(h => h.Aktif);
            ViewBag.ToplamClient = _context.Client.Count(c => c.Aktif);
            ViewBag.ToplamSosyalMedya = _context.SosyalMedya.Count(s => s.Aktif);

            return View();
        }

        // GET: /Admin/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
