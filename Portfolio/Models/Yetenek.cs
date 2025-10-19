namespace Portfolio.Models
{
    public class Yetenek
    {
        public int Id { get; set; }
        public string? Ad { get; set; }
        public int Yuzde { get; set; } // 0-100 arası
        public bool Aktif { get; set; } = true;
        public int Sira { get; set; }
    }
}
