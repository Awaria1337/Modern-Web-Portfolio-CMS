using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models
{
    public class SeoKeywords
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string? Keyword { get; set; }
        
        [StringLength(50)]
        public string? ContentType { get; set; } // Blog, Project, Profile
        
        public int? ContentId { get; set; } // Blog ID, Project ID, etc.
        
        public int Density { get; set; } = 0; // Keyword density percentage
        
        public int Position { get; set; } = 0; // Position in content (1-based)
        
        public bool IsMainKeyword { get; set; } = false;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}