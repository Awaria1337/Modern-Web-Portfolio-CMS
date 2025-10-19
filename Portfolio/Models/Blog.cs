using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string? Baslik { get; set; }
        public string? Icerik { get; set; }
        public string? Ozet { get; set; }
        public string? Gorsel { get; set; }
        public string? Kategori { get; set; }
        public DateTime? Tarih { get; set; }
        public int GoruntulemeSayisi { get; set; } = 0;
        public bool Aktif { get; set; } = true;
        
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
        public virtual BlogSeo? BlogSeo { get; set; }
    }
}
