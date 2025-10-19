using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models
{
    public class ProfileSeo
    {
        public int Id { get; set; }
        
        [StringLength(60)]
        public string? MetaTitle { get; set; }
        
        [StringLength(160)]
        public string? MetaDescription { get; set; }
        
        [StringLength(255)]
        public string? MetaKeywords { get; set; }
        
        [StringLength(255)]
        public string? CanonicalUrl { get; set; }
        
        [StringLength(20)]
        public string? MetaRobots { get; set; } = "index,follow";
        
        // Open Graph
        [StringLength(100)]
        public string? OgTitle { get; set; }
        
        [StringLength(200)]
        public string? OgDescription { get; set; }
        
        [StringLength(255)]
        public string? OgImage { get; set; }
        
        [StringLength(20)]
        public string? OgType { get; set; } = "profile";
        
        // Twitter Card
        [StringLength(100)]
        public string? TwitterTitle { get; set; }
        
        [StringLength(200)]
        public string? TwitterDescription { get; set; }
        
        [StringLength(255)]
        public string? TwitterImage { get; set; }
        
        [StringLength(20)]
        public string? TwitterCard { get; set; } = "summary";
        
        // Schema.org
        public string? SchemaMarkup { get; set; }
        
        // SEO Analysis
        public int SeoScore { get; set; } = 0;
        public DateTime? LastAnalyzed { get; set; }
        public string? SeoIssues { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        
        // Page Type (Home, About, Contact, etc.)
        [StringLength(50)]
        public string? PageType { get; set; } = "Home";
    }
}