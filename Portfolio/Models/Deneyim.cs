namespace Portfolio.Models
{
    public class Deneyim
    {
        public int Id { get; set; }
        public string? Sirket { get; set; }
        public string? Pozisyon { get; set; }
        public string? BaslangicTarihi { get; set; }
        public string? BitisTarihi { get; set; }
        public string? Aciklama { get; set; }
        public bool Aktif { get; set; } = true;
        public int Sira { get; set; }
    }
}
