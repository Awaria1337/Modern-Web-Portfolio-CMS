namespace Portfolio.Models
{
    public class Mesaj
    {
        public int Id { get; set; }
        public string? Ad { get; set; }
        public string? Email { get; set; }
        public string? Konu { get; set; }
        public string? MesajMetni { get; set; }
        public DateTime? Tarih { get; set; }
        public bool Okundu { get; set; } = false;
    }
}
