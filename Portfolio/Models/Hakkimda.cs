using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models
{
    public class Hakkimda
    {
        public int Id { get; set; }
        public string? Baslik { get; set; }
        public string? Aciklama1 { get; set; }
        public string? Aciklama2 { get; set; }
        
        // SEO Fields
        [StringLength(60)]
        public string? MetaTitle { get; set; }
        
        [StringLength(160)]
        public string? MetaDescription { get; set; }
        
        [StringLength(255)]
        public string? MetaKeywords { get; set; }
    }
}
