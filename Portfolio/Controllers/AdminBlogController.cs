using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using Portfolio.Models;

namespace Portfolio.Controllers
{
    public class AdminBlogController : Controller
    {
        private readonly SiteContext _context;

        public AdminBlogController(SiteContext context)
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
            var bloglar = _context.Blog.OrderByDescending(b => b.Tarih).ToList();
            return View(bloglar);
        }

        public IActionResult Create()
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Admin");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Blog model, IFormFile? GorselFile)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Admin");
            if (ModelState.IsValid)
            {
                // Görsel yükleme
                if (GorselFile != null && GorselFile.Length > 0)
                {
                    var uploadsRoot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "blog");
                    if (!Directory.Exists(uploadsRoot)) Directory.CreateDirectory(uploadsRoot);
                    var ext = Path.GetExtension(GorselFile.FileName);
                    var safeName = $"blog_{Guid.NewGuid():N}{ext}";
                    var filePath = Path.Combine(uploadsRoot, safeName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        GorselFile.CopyTo(stream);
                    }
                    model.Gorsel = $"/uploads/blog/{safeName}";
                }
                model.Tarih = DateTime.Now;
                model.GoruntulemeSayisi = 0;
                _context.Blog.Add(model);
                _context.SaveChanges();
                TempData["Success"] = "Blog başarıyla eklendi!";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Admin");
            var blog = _context.Blog.Find(id);
            if (blog == null) return NotFound();
            return View(blog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Blog model, IFormFile? GorselFile)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Admin");
            if (ModelState.IsValid)
            {
                // Yeni görsel yüklendiyse güncelle
                if (GorselFile != null && GorselFile.Length > 0)
                {
                    var uploadsRoot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "blog");
                    if (!Directory.Exists(uploadsRoot)) Directory.CreateDirectory(uploadsRoot);
                    var ext = Path.GetExtension(GorselFile.FileName);
                    var safeName = $"blog_{Guid.NewGuid():N}{ext}";
                    var filePath = Path.Combine(uploadsRoot, safeName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        GorselFile.CopyTo(stream);
                    }
                    model.Gorsel = $"/uploads/blog/{safeName}";
                }
                _context.Blog.Update(model);
                _context.SaveChanges();
                TempData["Success"] = "Blog başarıyla güncellendi!";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        [Route("AdminBlog/Delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (!IsLoggedIn()) return Json(new { success = false });
            var blog = _context.Blog.Find(id);
            if (blog != null)
            {
                _context.Blog.Remove(blog);
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}
