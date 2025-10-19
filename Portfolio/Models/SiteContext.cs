using Microsoft.EntityFrameworkCore;

namespace Portfolio.Models
{
    public class SiteContext : DbContext
    {
        public SiteContext(DbContextOptions<SiteContext> options) : base(options)
        {
        }
        
        // Admin
        public DbSet<AdminKullanici> AdminKullanici { get; set; }
        
        // Profile & Contact
        public DbSet<Profile> Profile { get; set; }
        public DbSet<SosyalMedya> SosyalMedya { get; set; }
        
        // About
        public DbSet<Hakkimda> Hakkimda { get; set; }
        public DbSet<Hizmet> Hizmet { get; set; }
        public DbSet<Testimonial> Testimonial { get; set; }
        public DbSet<Client> Client { get; set; }
        
        // Resume
        public DbSet<Egitim> Egitim { get; set; }
        public DbSet<Deneyim> Deneyim { get; set; }
        public DbSet<Yetenek> Yetenek { get; set; }
        
        // Portfolio
        public DbSet<Proje> Proje { get; set; }
        
        // Blog
        public DbSet<Blog> Blog { get; set; }
        
        // Contact Messages
        public DbSet<Mesaj> Mesaj { get; set; }
        
        // SEO Tables
        public DbSet<BlogSeo> BlogSeo { get; set; }
        public DbSet<ProjectSeo> ProjectSeo { get; set; }
        public DbSet<ProfileSeo> ProfileSeo { get; set; }
        public DbSet<GlobalSeo> GlobalSeo { get; set; }
        public DbSet<SeoKeywords> SeoKeywords { get; set; }
        public DbSet<SeoAnalytics> SeoAnalytics { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Blog - BlogSeo ilişkisi
            modelBuilder.Entity<Blog>()
                .HasOne(b => b.BlogSeo)
                .WithOne(bs => bs.Blog)
                .HasForeignKey<BlogSeo>(bs => bs.BlogId)
                .OnDelete(DeleteBehavior.Cascade);
                
            // Proje - ProjectSeo ilişkisi
            modelBuilder.Entity<Proje>()
                .HasOne(p => p.ProjectSeo)
                .WithOne(ps => ps.Proje)
                .HasForeignKey<ProjectSeo>(ps => ps.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
                
            // Unique constraints
            modelBuilder.Entity<Blog>()
                .HasIndex(b => b.Slug)
                .IsUnique();
                
            modelBuilder.Entity<Proje>()
                .HasIndex(p => p.Slug)
                .IsUnique();
                
            base.OnModelCreating(modelBuilder);
        }
    }
}
