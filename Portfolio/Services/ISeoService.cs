using Portfolio.Models;

namespace Portfolio.Services
{
    public interface ISeoService
    {
        string GenerateSlug(string title);
        Task<string> GenerateUniqueSlugAsync(string title, string contentType, int? excludeId = null);
        Task<bool> IsSlugExistsAsync(string slug, string contentType, int? excludeId = null);
        Task<GlobalSeo> GetGlobalSeoAsync();
        Task<BlogSeo> GetOrCreateBlogSeoAsync(int blogId);
        Task<ProjectSeo> GetOrCreateProjectSeoAsync(int projeId);
        Task SaveSeoAnalyticsAsync(string contentType, int contentId, string url, string title, int seoScore, string seoIssues, string seoRecommendations);
        Task SaveKeywordsAsync(string contentType, int contentId, List<string> keywords);
    }
}