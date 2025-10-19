namespace Portfolio.Models
{
    public class SosyalMedya
    {
        public int Id { get; set; }
        public string? Platform { get; set; } // facebook, twitter, instagram, linkedin, github
        public string? Url { get; set; }
        public string? Icon { get; set; } // ionicon name
        public bool Aktif { get; set; } = true;
        public int Sira { get; set; }
    }
}
