using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models
{
    public class GlobalSeo
    {
        public int Id { get; set; }
        
        [StringLength(100)]
        public string? SiteName { get; set; }
        
        [StringLength(255)]
        public string? SiteUrl { get; set; }
        
        [StringLength(160)]
        public string? SiteDescription { get; set; }
        
        [StringLength(255)]
        public string? SiteKeywords { get; set; }
        
        [StringLength(10)]
        public string? DefaultLanguage { get; set; } = "tr";
        
        [StringLength(255)]
        public string? DefaultImage { get; set; }
        
        // Social Media Settings
        [StringLength(100)]
        public string? FacebookAppId { get; set; }
        
        [StringLength(100)]
        public string? TwitterSite { get; set; }
        
        [StringLength(100)]
        public string? TwitterCreator { get; set; }
        
        [StringLength(255)]
        public string? LinkedinPage { get; set; }
        
        // Analytics & Tracking
        [StringLength(50)]
        public string? GoogleAnalyticsId { get; set; }
        
        [StringLength(50)]
        public string? GoogleTagManagerId { get; set; }
        
        [StringLength(100)]
        public string? GoogleSearchConsoleId { get; set; }
        
        [StringLength(100)]
        public string? BingWebmasterToolsId { get; set; }
        
        // Advanced SEO Settings
        public string? RobotsTxt { get; set; }
        public string? CustomHeadTags { get; set; }
        
        // Feature Toggles
        public bool EnableSeoAnalysis { get; set; } = true;
        public bool EnableCanonicalUrls { get; set; } = true;
        public bool EnableOpenGraph { get; set; } = true;
        public bool EnableTwitterCards { get; set; } = true;
        public bool EnableSitemap { get; set; } = true;
        public bool EnableJsonLd { get; set; } = true;
        
        // Legacy fields for backward compatibility
        [StringLength(60)]
        public string? DefaultMetaTitle { get; set; }
        
        [StringLength(160)]
        public string? DefaultMetaDescription { get; set; }
        
        [StringLength(255)]
        public string? DefaultMetaKeywords { get; set; }
        
        [StringLength(255)]
        public string? DefaultOgImage { get; set; }
        
        [StringLength(100)]
        public string? TwitterHandle { get; set; }
        
        [StringLength(100)]
        public string? YandexWebmasterToolsId { get; set; }
        
        public string? RobotsTxtContent { get; set; }
        public DateTime? LastSitemapGenerated { get; set; }
        public string? GlobalSchemaMarkup { get; set; }
        public bool EnableAutoSlug { get; set; } = true;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}