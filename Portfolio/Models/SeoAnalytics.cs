using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models
{
    public class SeoAnalytics
    {
        public int Id { get; set; }
        
        [StringLength(50)]
        public string? ContentType { get; set; } // Blog, Project, Profile
        
        public int? ContentId { get; set; }
        
        [StringLength(255)]
        public string? Url { get; set; }
        
        [StringLength(100)]
        public string? Title { get; set; }
        
        public int SeoScore { get; set; } = 0;
        
        public int TitleLength { get; set; } = 0;
        public int DescriptionLength { get; set; } = 0;
        public int KeywordCount { get; set; } = 0;
        public int ContentLength { get; set; } = 0;
        
        // SEO Issues (JSON format)
        public string? SeoIssues { get; set; }
        
        // SEO Recommendations (JSON format)
        public string? SeoRecommendations { get; set; }
        
        public DateTime AnalyzedAt { get; set; } = DateTime.Now;
        
        // Performance Metrics
        public int ViewCount { get; set; } = 0;
        public int ClickCount { get; set; } = 0;
        public decimal ClickThroughRate { get; set; } = 0;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}