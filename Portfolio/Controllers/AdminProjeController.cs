using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.Models;

namespace Portfolio.Controllers
{
    public class AdminProjeController : Controller
    {
        private readonly SiteContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminProjeController(SiteContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        private bool IsLoggedIn()
        {
            return HttpContext.Session.GetString("AdminId") != null;
        }

        public IActionResult Index()
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Admin");
            var projeler = _context.Proje.OrderBy(p => p.Sira).ToList();
            return View(projeler);
        }

        public IActionResult Create()
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Admin");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Proje model, IFormFile? gorselFile)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Admin");
            
            if (gorselFile != null && gorselFile.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(gorselFile.FileName);
                var uploadsPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "projects");
                var filePath = Path.Combine(uploadsPath, fileName);
                
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await gorselFile.CopyToAsync(stream);
                }
                
                model.Gorsel = "/uploads/projects/" + fileName;
            }
            
            if (ModelState.IsValid)
            {
                _context.Proje.Add(model);
                _context.SaveChanges();
                TempData["Success"] = "Proje başarıyla eklendi!";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Admin");
            var proje = _context.Proje.Find(id);
            if (proje == null) return NotFound();
            return View(proje);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Proje model, IFormFile? gorselFile)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Admin");
            
            if (gorselFile != null && gorselFile.Length > 0)
            {
                // Eski dosyayı sil - AsNoTracking kullanarak tracking sorununu önle
                var existingProject = _context.Proje.AsNoTracking().FirstOrDefault(p => p.Id == model.Id);
                if (existingProject != null && !string.IsNullOrEmpty(existingProject.Gorsel))
                {
                    var oldFilePath = Path.Combine(_webHostEnvironment.WebRootPath, existingProject.Gorsel.TrimStart('/'));
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }
                
                // Yeni dosyayı yükle
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(gorselFile.FileName);
                var uploadsPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "projects");
                var filePath = Path.Combine(uploadsPath, fileName);
                
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await gorselFile.CopyToAsync(stream);
                }
                
                model.Gorsel = "/uploads/projects/" + fileName;
            }
            
            if (ModelState.IsValid)
            {
                _context.Proje.Update(model);
                _context.SaveChanges();
                TempData["Success"] = "Proje başarıyla güncellendi!";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        [Route("AdminProje/Delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (!IsLoggedIn()) return Json(new { success = false });
            var proje = _context.Proje.Find(id);
            if (proje != null)
            {
                _context.Proje.Remove(proje);
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}
