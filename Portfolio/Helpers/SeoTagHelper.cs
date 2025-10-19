using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Portfolio.Models;
using System.Text;

namespace Portfolio.Helpers
{
    [HtmlTargetElement("seo-meta")]
    public class SeoMetaTagHelper : TagHelper
    {
        private readonly SiteContext _context;

        public SeoMetaTagHelper(SiteContext context)
        {
            _context = context;
        }

        [HtmlAttributeName("content-type")]
        public string? ContentType { get; set; }

        [HtmlAttributeName("content-id")]
        public int? ContentId { get; set; }

        [HtmlAttributeName("title")]
        public string? Title { get; set; }

        [HtmlAttributeName("description")]
        public string? Description { get; set; }

        [HtmlAttributeName("keywords")]
        public string? Keywords { get; set; }

        [HtmlAttributeName("image")]
        public string? Image { get; set; }

        [HtmlAttributeName("url")]
        public string? Url { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = null; // Remove the wrapper tag

            var globalSeo = await _context.GlobalSeo.FirstOrDefaultAsync();
            var siteUrl = globalSeo?.SiteUrl ?? "https://localhost";
            var siteName = globalSeo?.SiteName ?? "Portfolio";

            string metaTitle = Title ?? globalSeo?.DefaultMetaTitle ?? siteName;
            string metaDescription = Description ?? globalSeo?.DefaultMetaDescription ?? "";
            string metaKeywords = Keywords ?? globalSeo?.DefaultMetaKeywords ?? "";
            string metaImage = Image ?? $"{siteUrl}/assets/images/default-og.jpg";
            string canonicalUrl = Url ?? siteUrl;

            // Content-specific SEO
            if (!string.IsNullOrEmpty(ContentType) && ContentId.HasValue)
            {
                if (ContentType == "blog")
                {
                    var blogSeo = await _context.BlogSeo
                        .Include(bs => bs.Blog)
                        .FirstOrDefaultAsync(bs => bs.BlogId == ContentId.Value);
                    
                    if (blogSeo != null)
                    {
                        metaTitle = blogSeo.MetaTitle ?? blogSeo.Blog?.MetaTitle ?? blogSeo.Blog?.Baslik ?? metaTitle;
                        metaDescription = blogSeo.MetaDescription ?? blogSeo.Blog?.MetaDescription ?? blogSeo.Blog?.Ozet ?? metaDescription;
                        metaKeywords = blogSeo.MetaKeywords ?? blogSeo.Blog?.MetaKeywords ?? metaKeywords;
                        metaImage = blogSeo.OgImage ?? blogSeo.Blog?.Gorsel ?? metaImage;
                        canonicalUrl = blogSeo.CanonicalUrl ?? $"{siteUrl}/blog/{blogSeo.Blog?.Slug ?? ContentId.ToString()}";
                    }
                }
                else if (ContentType == "proje")
                {
                    var projectSeo = await _context.ProjectSeo
                        .Include(ps => ps.Proje)
                        .FirstOrDefaultAsync(ps => ps.ProjectId == ContentId.Value);
                    
                    if (projectSeo != null)
                    {
                        metaTitle = projectSeo.MetaTitle ?? projectSeo.Proje?.MetaTitle ?? projectSeo.Proje?.Baslik ?? metaTitle;
                        metaDescription = projectSeo.MetaDescription ?? projectSeo.Proje?.MetaDescription ?? projectSeo.Proje?.Aciklama ?? metaDescription;
                        metaKeywords = projectSeo.MetaKeywords ?? projectSeo.Proje?.MetaKeywords ?? metaKeywords;
                        metaImage = projectSeo.OgImage ?? projectSeo.Proje?.Gorsel ?? metaImage;
                        canonicalUrl = projectSeo.CanonicalUrl ?? $"{siteUrl}/proje/{projectSeo.Proje?.Slug ?? ContentId.ToString()}";
                    }
                }
            }

            var html = new StringBuilder();

            // Basic Meta Tags
            html.AppendLine($"<title>{metaTitle}</title>");
            html.AppendLine($"<meta name=\"description\" content=\"{metaDescription}\" />");
            if (!string.IsNullOrEmpty(metaKeywords))
            {
                html.AppendLine($"<meta name=\"keywords\" content=\"{metaKeywords}\" />");
            }
            html.AppendLine($"<link rel=\"canonical\" href=\"{canonicalUrl}\" />");

            // Open Graph Tags
            html.AppendLine($"<meta property=\"og:title\" content=\"{metaTitle}\" />");
            html.AppendLine($"<meta property=\"og:description\" content=\"{metaDescription}\" />");
            html.AppendLine($"<meta property=\"og:image\" content=\"{metaImage}\" />");
            html.AppendLine($"<meta property=\"og:url\" content=\"{canonicalUrl}\" />");
            html.AppendLine($"<meta property=\"og:site_name\" content=\"{siteName}\" />");
            html.AppendLine("<meta property=\"og:type\" content=\"website\" />");

            // Twitter Card Tags
            html.AppendLine("<meta name=\"twitter:card\" content=\"summary_large_image\" />");
            html.AppendLine($"<meta name=\"twitter:title\" content=\"{metaTitle}\" />");
            html.AppendLine($"<meta name=\"twitter:description\" content=\"{metaDescription}\" />");
            html.AppendLine($"<meta name=\"twitter:image\" content=\"{metaImage}\" />");

            if (!string.IsNullOrEmpty(globalSeo?.TwitterSite))
            {
                html.AppendLine($"<meta name=\"twitter:site\" content=\"@{globalSeo.TwitterSite}\" />");
            }

            if (!string.IsNullOrEmpty(globalSeo?.TwitterCreator))
            {
                html.AppendLine($"<meta name=\"twitter:creator\" content=\"@{globalSeo.TwitterCreator}\" />");
            }

            // Facebook App ID
            if (!string.IsNullOrEmpty(globalSeo?.FacebookAppId))
            {
                html.AppendLine($"<meta property=\"fb:app_id\" content=\"{globalSeo.FacebookAppId}\" />");
            }

            // Schema.org JSON-LD
            if (!string.IsNullOrEmpty(globalSeo?.GlobalSchemaMarkup))
            {
                html.AppendLine($"<script type=\"application/ld+json\">{globalSeo.GlobalSchemaMarkup}</script>");
            }

            output.Content.SetHtmlContent(html.ToString());
        }
    }

    [HtmlTargetElement("seo-analytics")]
    public class SeoAnalyticsTagHelper : TagHelper
    {
        private readonly SiteContext _context;

        public SeoAnalyticsTagHelper(SiteContext context)
        {
            _context = context;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = null; // Remove the wrapper tag

            var globalSeo = await _context.GlobalSeo.FirstOrDefaultAsync();
            var html = new StringBuilder();

            // Google Analytics
            if (!string.IsNullOrEmpty(globalSeo?.GoogleAnalyticsId))
            {
                html.AppendLine($@"
<!-- Google Analytics -->
<script async src=""https://www.googletagmanager.com/gtag/js?id={globalSeo.GoogleAnalyticsId}""></script>
<script>
  window.dataLayer = window.dataLayer || [];
  function gtag(){{dataLayer.push(arguments);}}
  gtag('js', new Date());
  gtag('config', '{globalSeo.GoogleAnalyticsId}');
</script>");
            }

            // Google Tag Manager
            if (!string.IsNullOrEmpty(globalSeo?.GoogleTagManagerId))
            {
                html.AppendLine($@"
                    <!-- Google Tag Manager -->
                    <script>(function(w,d,s,l,i){{w[l]=w[l]||[];w[l].push({{'gtm.start':
                    new Date().getTime(),event:'gtm.js'}});var f=d.getElementsByTagName(s)[0],
                    j=d.createElement(s),dl=l!='dataLayer'?'&l='+l:'';j.async=true;j.src=
                    'https://www.googletagmanager.com/gtm.js?id='+i+dl;f.parentNode.insertBefore(j,f);
                    }})(window,document,'script','dataLayer','{globalSeo.GoogleTagManagerId}');</script>");
            }

            // Google Search Console
            if (!string.IsNullOrEmpty(globalSeo?.GoogleSearchConsoleId))
            {
                html.AppendLine($@"<meta name=""google-site-verification"" content=""{globalSeo.GoogleSearchConsoleId}"" />");
            }

            // Bing Webmaster Tools
            if (!string.IsNullOrEmpty(globalSeo?.BingWebmasterToolsId))
            {
                html.AppendLine($@"<meta name=""msvalidate.01"" content=""{globalSeo.BingWebmasterToolsId}"" />");
            }

            output.Content.SetHtmlContent(html.ToString());
        }
    }
}