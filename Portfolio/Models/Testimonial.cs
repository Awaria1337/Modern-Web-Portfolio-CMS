namespace Portfolio.Models
{
    public class Testimonial
    {
        public int Id { get; set; }
        public string? Ad { get; set; }
        public string? Unvan { get; set; }
        public string? Foto { get; set; }
        public string? Yorum { get; set; }
        public DateTime? Tarih { get; set; }
        public bool Aktif { get; set; } = true;
        public int Sira { get; set; }
    }
}
