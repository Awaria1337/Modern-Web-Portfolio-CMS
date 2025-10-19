# 🎨 Portfolio - Modern Web Portfolio & CMS

[![.NET](https://img.shields.io/badge/.NET-6.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-MVC-512BD4)](https://docs.microsoft.com/aspnet/core/)
[![Entity Framework](https://img.shields.io/badge/Entity%20Framework-Core%206.0-512BD4)](https://docs.microsoft.com/ef/core/)
[![Bootstrap](https://img.shields.io/badge/Bootstrap-5-7952B3?logo=bootstrap)](https://getbootstrap.com/)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

> Modern, SEO-optimized portfolio website with a powerful admin panel built on ASP.NET Core MVC

![Portfolio Dashboard](https://via.placeholder.com/1200x400/007bff/ffffff?text=Portfolio+Admin+Dashboard)

## 📋 İçindekiler

- [Özellikler](#-özellikler)
- [Teknolojiler](#-teknolojiler)
- [Kurulum](#-kurulum)
- [Kullanım](#-kullanım)
- [Proje Yapısı](#-proje-yapısı)
- [Ekran Görüntüleri](#-ekran-görüntüleri)
- [Veritabanı](#-veritabanı)
- [API Referansı](#-api-referansı)
- [Güvenlik](#-güvenlik)
- [Katkıda Bulunma](#-katkıda-bulunma)
- [Lisans](#-lisans)

## ✨ Özellikler

### 🎯 Ana Özellikler

- **📊 Modern Admin Paneli**: Kullanıcı dostu, responsive admin arayüzü
- **📝 Blog Yönetimi**: WYSIWYG editör ile blog yazıları yönetimi
- **💼 Proje Portfolyosu**: Projelerinizi kategorize ederek sergileyin
- **🔍 Gelişmiş SEO Sistemi**: Her sayfa için özelleştirilebilir meta taglar
- **📈 SEO Analiz Araçları**: Real-time SEO skoru ve öneriler
- **👥 Müşteri Görüşleri**: Testimonial yönetimi
- **📧 İletişim Yönetimi**: Form mesajlarını admin panelden görüntüleme
- **🎓 CV/Resume Modülü**: Eğitim ve deneyim yönetimi
- **🛠️ Hizmet Yönetimi**: Sunduğunuz hizmetleri tanıtın
- **🌐 Sosyal Medya Entegrasyonu**: Tüm sosyal medya hesaplarınız tek yerden

### 🚀 SEO Özellikleri

- ✅ Otomatik **slug oluşturma** (Türkçe karakter desteği)
- ✅ **Meta Title, Description, Keywords** yönetimi
- ✅ **Open Graph** tagları (Facebook, LinkedIn)
- ✅ **Twitter Card** optimizasyonu
- ✅ **Schema.org JSON-LD** yapılandırılmış veri
- ✅ **Canonical URL** yönetimi
- ✅ **XML Sitemap** otomasyonu
- ✅ **Robots.txt** editörü
- ✅ **Google Analytics** entegrasyonu
- ✅ **SEO Skoru** hesaplama (0-100)
- ✅ **Anahtar kelime yoğunluğu** analizi

### 🎨 Öne Çıkan Teknolojiler

- **Backend**: ASP.NET Core 6.0 MVC
- **ORM**: Entity Framework Core 6.0
- **Database**: SQL Server
- **Frontend**: Razor Pages + Bootstrap 5
- **Admin Theme**: Techmin v1.0
- **Icons**: Remix Icons, Font Awesome
- **Charts**: Chart.js
- **Tables**: DataTables.net

## 🛠️ Teknolojiler

### Backend Stack

\\\
- .NET 6.0
- ASP.NET Core MVC
- Entity Framework Core 6.0.36
- C# 10
- LINQ
\\\

### Frontend Stack

\\\
- Razor Pages
- Bootstrap 5
- jQuery 3.x
- DataTables
- Chart.js
- Remix Icons
- Font Awesome
\\\

### Veritabanı

\\\
- Microsoft SQL Server
- Entity Framework Migrations
\\\

## 📦 Kurulum

### Ön Gereksinimler

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [SQL Server](https://www.microsoft.com/sql-server/sql-server-downloads) (Express veya üzeri)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) veya [VS Code](https://code.visualstudio.com/)

### Adım Adım Kurulum

1. **Projeyi Klonlayın**

\\\ash
git clone https://github.com/Awaria1337/Portfolio.git
cd Portfolio
\\\

2. **Veritabanı Bağlantısını Yapılandırın**

\ppsettings.json\ dosyasını düzenleyin:

\\\json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=PortfolioDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
\\\

3. **Veritabanı Migrations Uygulayın**

\\\ash
cd Portfolio
dotnet ef database update
\\\

4. **Admin Kullanıcısı Oluşturun**

SQL Server'da aşağıdaki scripti çalıştırın:

\\\sql
INSERT INTO AdminKullanici (Adi, Soyadi, KullaniciAdi, Sifre)
VALUES ('Admin', 'User', 'admin', 'admin123');
\\\

> ⚠️ **Güvenlik Notu**: İlk girişten sonra şifrenizi mutlaka değiştirin!

5. **Projeyi Çalıştırın**

\\\ash
dotnet run
\\\

Uygulama şu adreste çalışacaktır: **https://localhost:5001**

### Docker ile Kurulum (Opsiyonel)

\\\ash
docker-compose up -d
\\\

## 🎯 Kullanım

### Admin Paneline Giriş

1. Tarayıcınızda \https://localhost:5001/Admin/Login\ adresine gidin
2. Varsayılan kullanıcı adı: **admin**
3. Varsayılan şifre: **admin123**

### Temel İşlemler

#### Blog Yazısı Ekleme

1. Admin Panel → **Blog Yazıları**
2. **Yeni Ekle** butonuna tıklayın
3. Başlık, içerik, kategori bilgilerini girin
4. Görsel yükleyin
5. SEO ayarlarını yapın (opsiyonel)
6. **Kaydet** butonuna tıklayın

#### Proje Ekleme

1. Admin Panel → **Projeler**
2. **Yeni Proje Ekle** butonuna tıklayın
3. Proje detaylarını doldurun
4. Kategori seçin (Web Design, Application, Web Development)
5. Görsel ve URL ekleyin
6. **Kaydet**

#### SEO Optimizasyonu

1. Admin Panel → **SEO Yönetimi** → **Blog SEO**
2. Düzenlemek istediğiniz içeriği seçin
3. **SEO Analiz** butonuna tıklayın
4. Önerileri uygulayın
5. Meta tagları optimize edin
6. **Kaydet ve Yayınla**

## 📂 Proje Yapısı

\\\
Portfolio/
├── Controllers/              # MVC Controllers
│   ├── AdminController.cs              # Auth & Dashboard
│   ├── AdminBlogController.cs          # Blog CRUD
│   ├── AdminProjeController.cs         # Project CRUD
│   ├── AdminSeoController.cs           # SEO Management
│   └── ...                             # Other admin controllers
├── Models/                   # Data Models
│   ├── Blog.cs, BlogSeo.cs
│   ├── Proje.cs, ProjectSeo.cs
│   ├── SiteContext.cs                  # DbContext
│   └── ViewModels/
├── Views/                    # Razor Views
│   ├── Admin/
│   │   ├── Login.cshtml
│   │   └── Index.cshtml
│   ├── AdminBlog/, AdminProje/, AdminSeo/
│   └── Shared/
│       └── _AdminLayout.cshtml
├── Services/                 # Business Logic
│   ├── SeoService.cs
│   └── SeoAnalysisService.cs
├── wwwroot/                  # Static Files
│   ├── admin-assets/         # Admin theme
│   ├── assets/               # Frontend assets
│   └── uploads/              # User uploads
├── Migrations/               # EF Migrations
├── appsettings.json
├── Program.cs
└── Portfolio.csproj
\\\

## 📸 Ekran Görüntüleri

### Admin Dashboard
![Dashboard](https://via.placeholder.com/800x500/007bff/ffffff?text=Admin+Dashboard)

### Blog Yönetimi
![Blog Management](https://via.placeholder.com/800x500/28a745/ffffff?text=Blog+Management)

### SEO Dashboard
![SEO Dashboard](https://via.placeholder.com/800x500/ffc107/333333?text=SEO+Dashboard)

### Proje Listesi
![Projects](https://via.placeholder.com/800x500/17a2b8/ffffff?text=Projects+List)

## 🗄️ Veritabanı

### Ana Tablolar

| Tablo | Açıklama |
|-------|----------|
| \AdminKullanici\ | Admin kullanıcı bilgileri |
| \Profile\ | Kişisel profil bilgileri |
| \Blog\ | Blog yazıları |
| \BlogSeo\ | Blog SEO ayarları (1:1) |
| \Proje\ | Portfolio projeleri |
| \ProjectSeo\ | Proje SEO ayarları (1:1) |
| \Hizmet\ | Sunulan hizmetler |
| \Yetenek\ | Teknik yetenekler |
| \Egitim\ | Eğitim geçmişi |
| \Deneyim\ | İş deneyimi |
| \Testimonial\ | Müşteri görüşleri |
| \Client\ | Client logoları |
| \SosyalMedya\ | Sosyal medya linkleri |
| \Mesaj\ | İletişim formu mesajları |
| \GlobalSeo\ | Global SEO ayarları |
| \SeoAnalytics\ | SEO analiz sonuçları |
| \SeoKeywords\ | Anahtar kelime listesi |

### Entity İlişkileri

\\\
Blog 1 ──── 1 BlogSeo (Cascade Delete)
Proje 1 ──── 1 ProjectSeo (Cascade Delete)
\\\

### Migration Komutları

\\\ash
# Yeni migration oluştur
dotnet ef migrations add MigrationName

# Database'i güncelle
dotnet ef database update

# Son migration'ı geri al
dotnet ef database update PreviousMigrationName

# Migration'ı sil
dotnet ef migrations remove
\\\

## 📡 API Referansı

Bu proje MVC pattern kullandığından REST API endpoint'leri yoktur. Ancak AJAX istekleri için kullanılan endpoint'ler:

### Admin AJAX Endpoints

| Method | Endpoint | Açıklama |
|--------|----------|----------|
| POST | \/AdminBlog/Delete/{id}\ | Blog silme |
| POST | \/AdminProje/Delete/{id}\ | Proje silme |
| POST | \/AdminSeo/AnalyzeSeo\ | SEO analizi |
| GET | \/AdminMesaj/MarkAsRead/{id}\ | Mesaj okundu işaretleme |

## 🔐 Güvenlik

### Mevcut Güvenlik Özellikleri

✅ **Session-based Authentication**: 30 dakika timeout
✅ **CSRF Protection**: ValidateAntiForgeryToken kullanımı
✅ **SQL Injection**: Entity Framework ile korunuyor
✅ **XSS Prevention**: Razor automatic encoding

### ⚠️ Güvenlik Uyarıları

🔴 **KRİTİK**: Şifreler düz metin olarak saklanıyor! Production ortamında mutlaka şifre hashleme (BCrypt, ASP.NET Identity) kullanın.

### Önerilen Güvenlik İyileştirmeleri

1. **Şifre Hashleme**: BCrypt veya ASP.NET Core Identity
2. **HTTPS Zorunluluğu**: Production'da SSL/TLS
3. **Rate Limiting**: Brute force saldırı koruması
4. **Input Validation**: Daha katı validasyon kuralları
5. **File Upload Security**: Dosya tipi ve boyut kısıtlamaları
6. **Logging & Monitoring**: Serilog + Application Insights

## 🧪 Test

\\\ash
# Unit testleri çalıştır
dotnet test

# Coverage raporu oluştur
dotnet test /p:CollectCoverage=true
\\\

## 🚀 Deployment

### IIS'e Deploy

1. \dotnet publish -c Release\
2. IIS'de yeni site oluşturun
3. Application Pool → .NET CLR Version: **No Managed Code**
4. \publish\ klasörünü site dizinine kopyalayın

### Azure App Service'e Deploy

\\\ash
# Azure CLI ile deploy
az webapp up --name your-portfolio-app --resource-group PortfolioRG
\\\

## 🤝 Katkıda Bulunma

Katkılarınızı bekliyoruz! Lütfen aşağıdaki adımları takip edin:

1. Fork'layın (https://github.com/Awaria1337/Portfolio/fork)
2. Feature branch oluşturun (\git checkout -b feature/amazing-feature\)
3. Değişikliklerinizi commit edin (\git commit -m 'feat: Add amazing feature'\)
4. Branch'inizi push edin (\git push origin feature/amazing-feature\)
5. Pull Request açın

### Commit Mesaj Formatı

\\\
feat: Yeni özellik ekleme
fix: Bug düzeltme
docs: Dokümantasyon güncellemesi
style: Kod formatı değişikliği
refactor: Kod yeniden yapılandırma
test: Test ekleme/güncelleme
chore: Build/config değişiklikleri
\\\

## 📝 Changelog

### [1.1.0] - 2025-10-19

#### Added
- Kapsamlı SEO yönetim sistemi
- SEO analiz araçları ve puanlama
- Open Graph ve Twitter Card desteği
- Otomatik slug oluşturma (Türkçe karakter desteği)
- Global SEO ayarları
- SEO Analytics dashboard

#### Changed
- Admin panel UI iyileştirmeleri
- Dashboard istatistikleri güncellendi

### [1.0.0] - 2025-10-01

#### Added
- İlk sürüm yayınlandı
- Admin panel
- Blog yönetimi
- Proje portfolyosu
- İletişim formu
- Testimonial sistemi

## 📄 Lisans

Bu proje MIT Lisansı altında lisanslanmıştır. Detaylar için [LICENSE](LICENSE) dosyasına bakın.

## 👨‍💻 Geliştirici

**Awaria1337**

- GitHub: [@Awaria1337](https://github.com/Awaria1337)
- LinkedIn: [Profiliniz](https://linkedin.com/in/yourprofile)
- Website: [portfolio-demo.com](https://portfolio-demo.com)

## 🙏 Teşekkürler

- [ASP.NET Core](https://docs.microsoft.com/aspnet/core/) - Framework
- [Bootstrap](https://getbootstrap.com/) - CSS Framework
- [Techmin](https://themeforest.net/) - Admin Template
- [Remix Icon](https://remixicon.com/) - İkonlar
- [DataTables](https://datatables.net/) - Tablo eklentisi

## 📞 İletişim & Destek

Sorularınız veya önerileriniz için:

- 📧 Email: [your.email@example.com](mailto:your.email@example.com)
- 🐛 Issues: [GitHub Issues](https://github.com/Awaria1337/Portfolio/issues)
- 💬 Discussions: [GitHub Discussions](https://github.com/Awaria1337/Portfolio/discussions)

---

⭐ **Bu projeyi beğendiyseniz yıldız vermeyi unutmayın!**

Made with ❤️ by [Awaria1337](https://github.com/Awaria1337)
