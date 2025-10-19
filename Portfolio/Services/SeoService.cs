using Portfolio.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Portfolio.Services
{
    public class SeoService : ISeoService
    {
        private readonly SiteContext _context;

        public SeoService(SiteContext context)
        {
            _context = context;
        }

        // Slug oluşturma
        public string GenerateSlug(string title)
        {
            if (string.IsNullOrEmpty(title))
                return string.Empty;

            // Türkçe karakterleri değiştir
            title = title.Replace("ç", "c").Replace("Ç", "C")
                        .Replace("ğ", "g").Replace("Ğ", "G")
                        .Replace("ı", "i").Replace("İ", "I")
                        .Replace("ö", "o").Replace("Ö", "O")
                        .Replace("ş", "s").Replace("Ş", "S")
                        .Replace("ü", "u").Replace("Ü", "U");

            // Küçük harfe çevir
            title = title.ToLowerInvariant();

            // Özel karakterleri kaldır ve boşlukları tire ile değiştir
            title = Regex.Replace(title, @"[^a-z0-9\s-]", "");
            title = Regex.Replace(title, @"\s+", "-");
            title = Regex.Replace(title, @"-+", "-");
            title = title.Trim('-');

            return title;
        }

        // Benzersiz slug oluşturma
        public async Task<string> GenerateUniqueSlugAsync(string title, string contentType, int? excludeId = null)
        {
            var baseSlug = GenerateSlug(title);
            var slug = baseSlug;
            var counter = 1;

            while (await IsSlugExistsAsync(slug, contentType, excludeId))
            {
                slug = $"{baseSlug}-{counter}";
                counter++;
            }

            return slug;
        }

        // Slug varlığını kontrol etme
        public async Task<bool> IsSlugExistsAsync(string slug, string contentType, int? excludeId = null)
        {
            switch (contentType.ToLower())
            {
                case "blog":
                    return await _context.Blog.AnyAsync(b => b.Slug == slug && (excludeId == null || b.Id != excludeId));
                case "proje":
                    return await _context.Proje.AnyAsync(p => p.Slug == slug && (excludeId == null || p.Id != excludeId));
                default:
                    return false;
            }
        }

        // Global SEO ayarlarını getir
        public async Task<GlobalSeo> GetGlobalSeoAsync()
        {
            var globalSeo = await _context.GlobalSeo.FirstOrDefaultAsync();
            if (globalSeo == null)
            {
                globalSeo = new GlobalSeo
                {
                    SiteName = "Portfolio",
                    DefaultMetaTitle = "Portfolio - Web Developer",
                    DefaultMetaDescription = "Professional web developer portfolio showcasing projects and skills",
                    SiteUrl = "https://localhost",
                    EnableSeoAnalysis = true,
                    EnableAutoSlug = true,
                    EnableCanonicalUrls = true,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                _context.GlobalSeo.Add(globalSeo);
                await _context.SaveChangesAsync();
            }
            return globalSeo;
        }

        // Blog SEO bilgilerini getir veya oluştur
        public async Task<BlogSeo> GetOrCreateBlogSeoAsync(int blogId)
        {
            var blogSeo = await _context.BlogSeo.FirstOrDefaultAsync(bs => bs.BlogId == blogId);
            if (blogSeo == null)
            {
                var blog = await _context.Blog.FindAsync(blogId);
                if (blog != null)
                {
                    blogSeo = new BlogSeo
                    {
                        BlogId = blogId,
                        MetaTitle = blog.MetaTitle ?? blog.Baslik,
                        MetaDescription = blog.MetaDescription ?? blog.Ozet,
                        MetaKeywords = blog.MetaKeywords,
                        OgTitle = blog.Baslik,
                        OgDescription = blog.Ozet,
                        OgImage = blog.Gorsel,
                        TwitterTitle = blog.Baslik,
                        TwitterDescription = blog.Ozet,
                        TwitterImage = blog.Gorsel,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };
                    _context.BlogSeo.Add(blogSeo);
                    await _context.SaveChangesAsync();
                }
            }
            return blogSeo;
        }

        // Proje SEO bilgilerini getir veya oluştur
        public async Task<ProjectSeo> GetOrCreateProjectSeoAsync(int projeId)
        {
            var projectSeo = await _context.ProjectSeo.FirstOrDefaultAsync(ps => ps.ProjectId == projeId);
            if (projectSeo == null)
            {
                var proje = await _context.Proje.FindAsync(projeId);
                if (proje != null)
                {
                    projectSeo = new ProjectSeo
                    {
                        ProjectId = projeId,
                        MetaTitle = proje.MetaTitle ?? proje.Baslik,
                        MetaDescription = proje.MetaDescription ?? proje.Aciklama,
                        MetaKeywords = proje.MetaKeywords,
                        OgTitle = proje.Baslik,
                        OgDescription = proje.Aciklama,
                        OgImage = proje.Gorsel,
                        TwitterTitle = proje.Baslik,
                        TwitterDescription = proje.Aciklama,
                        TwitterImage = proje.Gorsel,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };
                    _context.ProjectSeo.Add(projectSeo);
                    await _context.SaveChangesAsync();
                }
            }
            return projectSeo;
        }

        // SEO Analytics kaydetme
        public async Task SaveSeoAnalyticsAsync(string contentType, int contentId, string url, string title, int seoScore, string seoIssues, string seoRecommendations)
        {
            var analytics = await _context.SeoAnalytics
                .FirstOrDefaultAsync(sa => sa.ContentType == contentType && sa.ContentId == contentId);

            if (analytics == null)
            {
                analytics = new SeoAnalytics
                {
                    ContentType = contentType,
                    ContentId = contentId,
                    Url = url,
                    Title = title,
                    CreatedAt = DateTime.Now
                };
                _context.SeoAnalytics.Add(analytics);
            }

            analytics.SeoScore = seoScore;
            analytics.SeoIssues = seoIssues;
            analytics.SeoRecommendations = seoRecommendations;
            analytics.AnalyzedAt = DateTime.Now;
            analytics.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        // Anahtar kelime kaydetme
        public async Task SaveKeywordsAsync(string contentType, int contentId, List<string> keywords)
        {
            // Mevcut anahtar kelimeleri sil
            var existingKeywords = await _context.SeoKeywords
                .Where(sk => sk.ContentType == contentType && sk.ContentId == contentId)
                .ToListAsync();
            _context.SeoKeywords.RemoveRange(existingKeywords);

            // Yeni anahtar kelimeleri ekle
            for (int i = 0; i < keywords.Count; i++)
            {
                var keyword = new SeoKeywords
                {
                    ContentType = contentType,
                    ContentId = contentId,
                    Keyword = keywords[i],
                    Position = i + 1,
                    IsMainKeyword = i == 0,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                _context.SeoKeywords.Add(keyword);
            }

            await _context.SaveChangesAsync();
        }
    }
}