namespace Portfolio.Models
{
    public class Egitim
    {
        public int Id { get; set; }
        public string? Okul { get; set; }
        public string? Bolum { get; set; }
        public string? BaslangicTarihi { get; set; }
        public string? BitisTarihi { get; set; }
        public string? Aciklama { get; set; }
        public bool Aktif { get; set; } = true;
        public int Sira { get; set; }
    }
}
