namespace Portfolio.Models
{
    public class Hizmet
    {
        public int Id { get; set; }
        public string? Baslik { get; set; }
        public string? Aciklama { get; set; }
        public string? Icon { get; set; } // icon dosya yolu
        public bool Aktif { get; set; } = true;
        public int Sira { get; set; }
    }
}
