namespace Portfolio.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string? Ad { get; set; }
        public string? Logo { get; set; }
        public string? Url { get; set; }
        public bool Aktif { get; set; } = true;
        public int Sira { get; set; }
    }
}
