using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.Models;
using Portfolio.Models.ViewModels;
using Portfolio.Services;
using System.Text.Json;

namespace Portfolio.Controllers
{
    public class AdminSeoController : Controller
    {
        private readonly SiteContext _context;
        private readonly ISeoService _seoService;
        private readonly ISeoAnalysisService _seoAnalysisService;

        public AdminSeoController(SiteContext context, ISeoService seoService, ISeoAnalysisService seoAnalysisService)
        {
            _context = context;
            _seoService = seoService;
            _seoAnalysisService = seoAnalysisService;
        }

        // SEO Dashboard
        public async Task<IActionResult> Index()
        {
            var model = new SeoIndexViewModel
            {
                BlogCount = await _context.Blog.CountAsync(),
                ProjectCount = await _context.Proje.CountAsync(),
                BlogSeoCount = await _context.BlogSeo.CountAsync(),
                ProjectSeoCount = await _context.ProjectSeo.CountAsync(),
                RecentAnalytics = await _context.SeoAnalytics
                    .OrderByDescending(sa => sa.AnalyzedAt)
                    .Take(10)
                    .ToListAsync(),
                GlobalSeo = await _seoService.GetGlobalSeoAsync()
            };

            return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> AnalyzeSeo(string contentType, int contentId)
    {
        try
        {
            string title = "", description = "", content = "", keywords = "", url = "";

            if (contentType == "blog")
            {
                var blog = await _context.Blog.FindAsync(contentId);
                if (blog != null)
                {
                    title = blog.MetaTitle ?? blog.Baslik ?? "";
                    description = blog.MetaDescription ?? blog.Ozet ?? "";
                    content = blog.Icerik ?? "";
                    keywords = blog.MetaKeywords ?? "";
                    url = $"/blog/{blog.Slug ?? blog.Id.ToString()}";
                }
            }
            else if (contentType == "project")
            {
                var proje = await _context.Proje.FindAsync(contentId);
                if (proje != null)
                {
                    title = proje.MetaTitle ?? proje.Baslik ?? "";
                    description = proje.MetaDescription ?? proje.Aciklama ?? "";
                    content = proje.Aciklama ?? "";
                    keywords = proje.MetaKeywords ?? "";
                    url = $"/project/{proje.Slug ?? proje.Id.ToString()}";
                }
            }

            var result = _seoAnalysisService.AnalyzeContent(title, description, content, keywords);

            // Analiz sonuçlarını kaydet
            await _seoService.SaveSeoAnalyticsAsync(
                contentType, 
                contentId, 
                url, 
                title, 
                result.Score,
                System.Text.Json.JsonSerializer.Serialize(result.Issues),
                System.Text.Json.JsonSerializer.Serialize(result.Recommendations)
            );
            
            return Json(new
            {
                success = true,
                score = result.Score,
                scoreText = GetScoreText(result.Score),
                scoreColor = GetScoreColor(result.Score),
                metrics = result.Metrics,
                issues = result.Issues,
                recommendations = result.Recommendations
            });
        }
        catch (Exception ex)
        {
            return Json(new
            {
                success = false,
                message = ex.Message
            });
        }
    }

    private string GetScoreText(int score)
    {
        return score switch
        {
            >= 80 => "Mükemmel",
            >= 60 => "İyi",
            >= 40 => "Orta",
            >= 20 => "Zayıf",
            _ => "Çok Zayıf"
        };
    }

    private string GetScoreColor(int score)
    {
        return score switch
        {
            >= 80 => "bg-success",
            >= 60 => "bg-info",
            >= 40 => "bg-warning",
            >= 20 => "bg-orange",
            _ => "bg-danger"
        };
    }

    // Blog SEO Yönetimi
        public async Task<IActionResult> BlogSeo()
        {
            var blogs = await _context.Blog
                .Include(b => b.BlogSeo)
                .OrderByDescending(b => b.Tarih)
                .ToListAsync();

            return View(blogs);
        }

        // Blog SEO Düzenleme
        public async Task<IActionResult> EditBlogSeo(int id)
        {
            var blog = await _context.Blog
                .Include(b => b.BlogSeo)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (blog == null)
                return NotFound();

            if (blog.BlogSeo == null)
            {
                blog.BlogSeo = await _seoService.GetOrCreateBlogSeoAsync(id);
            }

            return View(blog);
        }

        [HttpPost]
        public async Task<IActionResult> EditBlogSeo(int id, BlogSeo blogSeo)
        {
            var blog = await _context.Blog.FindAsync(id);
            if (blog == null)
                return NotFound();

            var existingSeo = await _context.BlogSeo.FirstOrDefaultAsync(bs => bs.BlogId == id);
            
            if (existingSeo == null)
            {
                blogSeo.BlogId = id;
                blogSeo.CreatedAt = DateTime.Now;
                blogSeo.UpdatedAt = DateTime.Now;
                _context.BlogSeo.Add(blogSeo);
            }
            else
            {
                existingSeo.MetaTitle = blogSeo.MetaTitle;
                existingSeo.MetaDescription = blogSeo.MetaDescription;
                existingSeo.MetaKeywords = blogSeo.MetaKeywords;
                existingSeo.OgTitle = blogSeo.OgTitle;
                existingSeo.OgDescription = blogSeo.OgDescription;
                existingSeo.OgImage = blogSeo.OgImage;
                existingSeo.TwitterTitle = blogSeo.TwitterTitle;
                existingSeo.TwitterDescription = blogSeo.TwitterDescription;
                existingSeo.TwitterImage = blogSeo.TwitterImage;
                existingSeo.SchemaMarkup = blogSeo.SchemaMarkup;
                existingSeo.CanonicalUrl = blogSeo.CanonicalUrl;
                existingSeo.UpdatedAt = DateTime.Now;
            }

            // Blog modelindeki SEO alanlarını da güncelle
            blog.MetaTitle = blogSeo.MetaTitle;
            blog.MetaDescription = blogSeo.MetaDescription;
            blog.MetaKeywords = blogSeo.MetaKeywords;

            // Slug güncelleme
            if (string.IsNullOrEmpty(blog.Slug))
            {
                blog.Slug = await _seoService.GenerateUniqueSlugAsync(blog.Baslik, "blog", blog.Id);
            }

            await _context.SaveChangesAsync();

            TempData["Success"] = "Blog SEO bilgileri başarıyla güncellendi.";
            return RedirectToAction(nameof(BlogSeo));
        }

        // Proje SEO Yönetimi
        public async Task<IActionResult> ProjectSeo()
        {
            var projects = await _context.Proje
                .Include(p => p.ProjectSeo)
                .OrderByDescending(p => p.Id)
                .ToListAsync();

            return View(projects);
        }

        // Proje SEO Düzenleme
        public async Task<IActionResult> EditProjectSeo(int id)
        {
            var project = await _context.Proje
                .Include(p => p.ProjectSeo)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
                return NotFound();

            if (project.ProjectSeo == null)
            {
                project.ProjectSeo = await _seoService.GetOrCreateProjectSeoAsync(id);
            }

            return View(project);
        }

        [HttpPost]
        public async Task<IActionResult> EditProjectSeo(int id, ProjectSeo projectSeo)
        {
            var project = await _context.Proje.FindAsync(id);
            if (project == null)
                return NotFound();

            var existingSeo = await _context.ProjectSeo.FirstOrDefaultAsync(ps => ps.ProjectId == id);
            
            if (existingSeo == null)
            {
                projectSeo.ProjectId = id;
                projectSeo.CreatedAt = DateTime.Now;
                projectSeo.UpdatedAt = DateTime.Now;
                _context.ProjectSeo.Add(projectSeo);
            }
            else
            {
                existingSeo.MetaTitle = projectSeo.MetaTitle;
                existingSeo.MetaDescription = projectSeo.MetaDescription;
                existingSeo.MetaKeywords = projectSeo.MetaKeywords;
                existingSeo.OgTitle = projectSeo.OgTitle;
                existingSeo.OgDescription = projectSeo.OgDescription;
                existingSeo.OgImage = projectSeo.OgImage;
                existingSeo.TwitterTitle = projectSeo.TwitterTitle;
                existingSeo.TwitterDescription = projectSeo.TwitterDescription;
                existingSeo.TwitterImage = projectSeo.TwitterImage;
                existingSeo.SchemaMarkup = projectSeo.SchemaMarkup;
                existingSeo.CanonicalUrl = projectSeo.CanonicalUrl;
                existingSeo.UpdatedAt = DateTime.Now;
            }

            // Proje modelindeki SEO alanlarını da güncelle
            project.MetaTitle = projectSeo.MetaTitle;
            project.MetaDescription = projectSeo.MetaDescription;
            project.MetaKeywords = projectSeo.MetaKeywords;

            // Slug güncelleme
            if (string.IsNullOrEmpty(project.Slug))
            {
                project.Slug = await _seoService.GenerateUniqueSlugAsync(project.Baslik, "proje", project.Id);
            }

            await _context.SaveChangesAsync();

            TempData["Success"] = "Proje SEO bilgileri başarıyla güncellendi.";
            return RedirectToAction(nameof(ProjectSeo));
        }

        // Global SEO Ayarları
        public async Task<IActionResult> GlobalSeo()
        {
            var globalSeo = await _seoService.GetGlobalSeoAsync();
            return View(globalSeo);
        }

        [HttpPost]
        public async Task<IActionResult> GlobalSeo(GlobalSeo globalSeo)
        {
            var existing = await _context.GlobalSeo.FirstOrDefaultAsync();
            
            if (existing == null)
            {
                globalSeo.CreatedAt = DateTime.Now;
                globalSeo.UpdatedAt = DateTime.Now;
                _context.GlobalSeo.Add(globalSeo);
            }
            else
            {
                existing.SiteName = globalSeo.SiteName;
                existing.DefaultMetaTitle = globalSeo.DefaultMetaTitle;
                existing.DefaultMetaDescription = globalSeo.DefaultMetaDescription;
                existing.DefaultMetaKeywords = globalSeo.DefaultMetaKeywords;
                existing.SiteUrl = globalSeo.SiteUrl;
                existing.FacebookAppId = globalSeo.FacebookAppId;
                existing.TwitterSite = globalSeo.TwitterSite;
                existing.TwitterCreator = globalSeo.TwitterCreator;
                existing.LinkedinPage = globalSeo.LinkedinPage;
                existing.GoogleAnalyticsId = globalSeo.GoogleAnalyticsId;
                existing.GoogleTagManagerId = globalSeo.GoogleTagManagerId;
                existing.GoogleSearchConsoleId = globalSeo.GoogleSearchConsoleId;
                existing.BingWebmasterToolsId = globalSeo.BingWebmasterToolsId;
                existing.RobotsTxtContent = globalSeo.RobotsTxtContent;
                existing.EnableSitemap = globalSeo.EnableSitemap;
                existing.GlobalSchemaMarkup = globalSeo.GlobalSchemaMarkup;
                existing.EnableSeoAnalysis = globalSeo.EnableSeoAnalysis;
                existing.EnableAutoSlug = globalSeo.EnableAutoSlug;
                existing.EnableCanonicalUrls = globalSeo.EnableCanonicalUrls;
                existing.UpdatedAt = DateTime.Now;
            }

            await _context.SaveChangesAsync();

            TempData["Success"] = "Global SEO ayarları başarıyla güncellendi.";
            return RedirectToAction(nameof(GlobalSeo));
        }



        // SEO Analytics
        public async Task<IActionResult> Analytics()
        {
            var analytics = await _context.SeoAnalytics
                .OrderByDescending(sa => sa.AnalyzedAt)
                .ToListAsync();

            return View(analytics);
        }

        // Slug Generator API
        [HttpPost]
        public async Task<IActionResult> GenerateSlug(string title, string contentType, int? excludeId = null)
        {
            try
            {
                var slug = await _seoService.GenerateUniqueSlugAsync(title, contentType, excludeId);
                return Json(new { success = true, slug = slug });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}