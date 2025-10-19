using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.Models;

namespace Portfolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SiteContext _context;

        public HomeController(ILogger<HomeController> logger, SiteContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            // Profile bilgileri (Sol sidebar)
            ViewBag.Profile = _context.Profile.FirstOrDefault();
            ViewBag.SosyalMedya = _context.SosyalMedya.Where(s => s.Aktif).OrderBy(s => s.Sira).ToList();
            
            // About section
            ViewBag.Hakkimda = _context.Hakkimda.FirstOrDefault();
            ViewBag.Hizmetler = _context.Hizmet.Where(h => h.Aktif).OrderBy(h => h.Sira).ToList();
            ViewBag.Testimonials = _context.Testimonial.Where(t => t.Aktif).OrderBy(t => t.Sira).ToList();
            ViewBag.Clients = _context.Client.Where(c => c.Aktif).OrderBy(c => c.Sira).ToList();
            
            // Resume section
            ViewBag.Egitimler = _context.Egitim.Where(e => e.Aktif).OrderBy(e => e.Sira).ToList();
            ViewBag.Deneyimler = _context.Deneyim.Where(d => d.Aktif).OrderBy(d => d.Sira).ToList();
            ViewBag.Yetenekler = _context.Yetenek.Where(y => y.Aktif).OrderBy(y => y.Sira).ToList();
            
            // Portfolio section
            ViewBag.Projeler = _context.Proje.Where(p => p.Aktif).OrderBy(p => p.Sira).ToList();
            
            // Blog section
            ViewBag.Bloglar = _context.Blog.Where(b => b.Aktif).OrderByDescending(b => b.Tarih).Take(6).ToList();
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult BlogDetail(int id)
        {
            var blog = _context.Blog.FirstOrDefault(b => b.Id == id && b.Aktif);
            if (blog == null) return NotFound();
            
            // SEO ViewBag ayarları
            ViewBag.ContentType = "blog";
            ViewBag.ContentId = id;
            ViewBag.SeoTitle = blog.MetaTitle ?? blog.Baslik;
            ViewBag.SeoDescription = blog.MetaDescription ?? blog.Ozet;
            ViewBag.SeoKeywords = blog.MetaKeywords;
            ViewBag.SeoImage = blog.Gorsel;
            ViewBag.SeoUrl = $"{Request.Scheme}://{Request.Host}/blog/{blog.Slug ?? id.ToString()}";
            
            try
            {
                blog.GoruntulemeSayisi++;
                _context.SaveChanges();
            }
            catch { /* view count update best-effort */ }
            return View(blog);
        }

        [HttpGet]
        public IActionResult ProjectDetail(int id)
        {
            var proje = _context.Proje.FirstOrDefault(p => p.Id == id && p.Aktif);
            if (proje == null) return NotFound();
            
            // SEO ViewBag ayarları
            ViewBag.ContentType = "project";
            ViewBag.ContentId = id;
            ViewBag.SeoTitle = proje.MetaTitle ?? proje.Baslik;
            ViewBag.SeoDescription = proje.MetaDescription ?? proje.Aciklama;
            ViewBag.SeoKeywords = proje.MetaKeywords;
            ViewBag.SeoImage = proje.Gorsel;
            ViewBag.SeoUrl = $"{Request.Scheme}://{Request.Host}/proje/{proje.Slug ?? id.ToString()}";
            
            return View(proje);
        }

        [HttpPost]
        public IActionResult SaveMessage(string name, string email, string subject, string message)
        {
            try
            {
                var mesaj = new Mesaj
                {
                    Ad = name,
                    Email = email,
                    Konu = subject,
                    MesajMetni = message,
                    Tarih = DateTime.Now,
                    Okundu = false
                };

                _context.Mesaj.Add(mesaj);
                _context.SaveChanges();

                return Json(new { success = true, message = "Mesajınız başarıyla gönderildi!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Bir hata oluştu: " + ex.Message });
            }
        }

        // XML Sitemap
        [Route("sitemap.xml")]
        public async Task<IActionResult> Sitemap()
        {
            var globalSeo = await _context.GlobalSeo.FirstOrDefaultAsync();
            if (globalSeo?.EnableSitemap != true)
            {
                return NotFound();
            }

            var siteUrl = globalSeo.SiteUrl ?? "https://localhost";
            var sitemap = new System.Text.StringBuilder();
            
            sitemap.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            sitemap.AppendLine("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">");
            
            // Ana sayfa
            sitemap.AppendLine("<url>");
            sitemap.AppendLine($"<loc>{siteUrl}</loc>");
            sitemap.AppendLine($"<lastmod>{DateTime.Now:yyyy-MM-dd}</lastmod>");
            sitemap.AppendLine("<changefreq>daily</changefreq>");
            sitemap.AppendLine("<priority>1.0</priority>");
            sitemap.AppendLine("</url>");
            
            // Blog sayfaları
            var blogs = await _context.Blog.Where(b => b.Aktif).ToListAsync();
            foreach (var blog in blogs)
            {
                sitemap.AppendLine("<url>");
                sitemap.AppendLine($"<loc>{siteUrl}/blog/{blog.Slug ?? blog.Id.ToString()}</loc>");
                sitemap.AppendLine($"<lastmod>{blog.Tarih:yyyy-MM-dd}</lastmod>");
                sitemap.AppendLine("<changefreq>weekly</changefreq>");
                sitemap.AppendLine("<priority>0.8</priority>");
                sitemap.AppendLine("</url>");
            }
            
            // Proje sayfaları
            var projeler = await _context.Proje.Where(p => p.Aktif).ToListAsync();
            foreach (var proje in projeler)
            {
                sitemap.AppendLine("<url>");
                sitemap.AppendLine($"<loc>{siteUrl}/proje/{proje.Slug ?? proje.Id.ToString()}</loc>");
                sitemap.AppendLine($"<lastmod>{DateTime.Now:yyyy-MM-dd}</lastmod>");
                sitemap.AppendLine("<changefreq>monthly</changefreq>");
                sitemap.AppendLine("<priority>0.7</priority>");
                sitemap.AppendLine("</url>");
            }
            
            sitemap.AppendLine("</urlset>");
            
            return Content(sitemap.ToString(), "application/xml");
        }

        // Robots.txt
        [Route("robots.txt")]
        public async Task<IActionResult> RobotsTxt()
        {
            var globalSeo = await _context.GlobalSeo.FirstOrDefaultAsync();
            var robotsTxt = new System.Text.StringBuilder();
            
            if (!string.IsNullOrEmpty(globalSeo?.RobotsTxtContent))
            {
                robotsTxt.AppendLine(globalSeo.RobotsTxtContent);
            }
            else
            {
                // Varsayılan robots.txt
                robotsTxt.AppendLine("User-agent: *");
                robotsTxt.AppendLine("Allow: /");
                robotsTxt.AppendLine("");
                
                if (globalSeo?.EnableSitemap == true)
                {
                    var siteUrl = globalSeo.SiteUrl ?? "https://localhost";
                    robotsTxt.AppendLine($"Sitemap: {siteUrl}/sitemap.xml");
                }
            }
            
            return Content(robotsTxt.ToString(), "text/plain");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
