using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models
{
    public class Proje
    {
        public int Id { get; set; }
        public string? Baslik { get; set; }
        public string? Kategori { get; set; } // Web design, Applications, Web development
        public string? Gorsel { get; set; }
        public string? Aciklama { get; set; }
        public string? Url { get; set; }
        public DateTime? Tarih { get; set; }
        public bool Aktif { get; set; } = true;
        public int Sira { get; set; }
        
        // SEO Fields
        [StringLength(100)]
        public string? Slug { get; set; }
        
        [StringLength(60)]
        public string? MetaTitle { get; set; }
        
        [StringLength(160)]
        public string? MetaDescription { get; set; }
        
        [StringLength(255)]
        public string? MetaKeywords { get; set; }
        
        // Navigation Properties
        public virtual ProjectSeo? ProjectSeo { get; set; }
    }
}
