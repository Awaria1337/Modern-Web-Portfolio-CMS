using System.Text.RegularExpressions;
using System.Text.Json;

namespace Portfolio.Services
{
    public class SeoAnalysisService : ISeoAnalysisService
    {
        public class SeoAnalysisResult
        {
            public int Score { get; set; }
            public List<string> Issues { get; set; } = new List<string>();
            public List<string> Recommendations { get; set; } = new List<string>();
            public Dictionary<string, object> Metrics { get; set; } = new Dictionary<string, object>();
        }

        public SeoAnalysisResult AnalyzeContent(string title, string description, string content, string keywords = "")
        {
            var result = new SeoAnalysisResult();
            var issues = new List<string>();
            var recommendations = new List<string>();
            var score = 100;

            // Title analizi
            if (string.IsNullOrEmpty(title))
            {
                issues.Add("Başlık eksik");
                recommendations.Add("SEO için başlık eklemelisiniz");
                score -= 20;
            }
            else
            {
                if (title.Length < 30)
                {
                    issues.Add("Başlık çok kısa (30 karakterden az)");
                    recommendations.Add("Başlığı 30-60 karakter arasında tutun");
                    score -= 10;
                }
                else if (title.Length > 60)
                {
                    issues.Add("Başlık çok uzun (60 karakterden fazla)");
                    recommendations.Add("Başlığı 30-60 karakter arasında tutun");
                    score -= 10;
                }

                result.Metrics["titleLength"] = title.Length;
            }

            // Description analizi
            if (string.IsNullOrEmpty(description))
            {
                issues.Add("Meta açıklama eksik");
                recommendations.Add("SEO için meta açıklama eklemelisiniz");
                score -= 15;
            }
            else
            {
                if (description.Length < 120)
                {
                    issues.Add("Meta açıklama çok kısa (120 karakterden az)");
                    recommendations.Add("Meta açıklamayı 120-160 karakter arasında tutun");
                    score -= 8;
                }
                else if (description.Length > 160)
                {
                    issues.Add("Meta açıklama çok uzun (160 karakterden fazla)");
                    recommendations.Add("Meta açıklamayı 120-160 karakter arasında tutun");
                    score -= 8;
                }

                result.Metrics["descriptionLength"] = description.Length;
            }

            // Content analizi
            if (!string.IsNullOrEmpty(content))
            {
                var wordCount = CountWords(content);
                result.Metrics["wordCount"] = wordCount;

                if (wordCount < 300)
                {
                    issues.Add("İçerik çok kısa (300 kelimeden az)");
                    recommendations.Add("SEO için en az 300 kelime içerik yazın");
                    score -= 15;
                }

                // Anahtar kelime yoğunluğu analizi
                if (!string.IsNullOrEmpty(keywords))
                {
                    var keywordList = keywords.Split(',').Select(k => k.Trim()).ToList();
                    var keywordDensities = AnalyzeKeywordDensity(content, keywordList);
                    result.Metrics["keywordDensities"] = keywordDensities;

                    foreach (var kd in keywordDensities)
                    {
                        if (kd.Value < 0.5)
                        {
                            recommendations.Add($"'{kd.Key}' anahtar kelimesini daha fazla kullanın (şu an %{kd.Value:F1})");
                        }
                        else if (kd.Value > 3)
                        {
                            issues.Add($"'{kd.Key}' anahtar kelimesi çok fazla kullanılmış (%{kd.Value:F1})");
                            recommendations.Add($"'{kd.Key}' anahtar kelimesinin kullanımını azaltın");
                            score -= 5;
                        }
                    }
                }

                // Başlık etiketleri analizi
                var headingAnalysis = AnalyzeHeadings(content);
                result.Metrics["headings"] = headingAnalysis;

                if (!headingAnalysis.ContainsKey("h1") || headingAnalysis["h1"] == 0)
                {
                    issues.Add("H1 etiketi eksik");
                    recommendations.Add("İçeriğe H1 etiketi ekleyin");
                    score -= 10;
                }

                if (!headingAnalysis.ContainsKey("h2") || headingAnalysis["h2"] == 0)
                {
                    recommendations.Add("İçeriği H2 etiketleriyle yapılandırın");
                    score -= 5;
                }
            }

            // Keywords analizi
            if (string.IsNullOrEmpty(keywords))
            {
                issues.Add("Anahtar kelimeler eksik");
                recommendations.Add("SEO için anahtar kelimeler belirleyin");
                score -= 10;
            }

            result.Score = Math.Max(0, score);
            result.Issues = issues;
            result.Recommendations = recommendations;

            return result;
        }

        private int CountWords(string text)
        {
            if (string.IsNullOrEmpty(text))
                return 0;

            // HTML etiketlerini kaldır
            var cleanText = Regex.Replace(text, @"<[^>]+>", " ");
            
            // Kelimeleri say
            var words = cleanText.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            return words.Length;
        }

        private Dictionary<string, double> AnalyzeKeywordDensity(string content, List<string> keywords)
        {
            var densities = new Dictionary<string, double>();
            
            if (string.IsNullOrEmpty(content) || keywords == null || !keywords.Any())
                return densities;

            // HTML etiketlerini kaldır
            var cleanContent = Regex.Replace(content, @"<[^>]+>", " ").ToLowerInvariant();
            var totalWords = CountWords(cleanContent);

            foreach (var keyword in keywords)
            {
                if (string.IsNullOrEmpty(keyword))
                    continue;

                var keywordLower = keyword.ToLowerInvariant();
                var matches = Regex.Matches(cleanContent, @"\b" + Regex.Escape(keywordLower) + @"\b");
                var density = totalWords > 0 ? (matches.Count * 100.0) / totalWords : 0;
                densities[keyword] = density;
            }

            return densities;
        }

        private Dictionary<string, int> AnalyzeHeadings(string content)
        {
            var headings = new Dictionary<string, int>();
            
            if (string.IsNullOrEmpty(content))
                return headings;

            // H1-H6 etiketlerini say
            for (int i = 1; i <= 6; i++)
            {
                var pattern = $@"<h{i}[^>]*>.*?</h{i}>";
                var matches = Regex.Matches(content, pattern, RegexOptions.IgnoreCase);
                headings[$"h{i}"] = matches.Count;
            }

            return headings;
        }

        public string GetSeoScoreColor(int score)
        {
            if (score >= 80) return "success";
            if (score >= 60) return "warning";
            return "danger";
        }

        public string GetSeoScoreText(int score)
        {
            if (score >= 80) return "Mükemmel";
            if (score >= 60) return "İyi";
            if (score >= 40) return "Orta";
            return "Zayıf";
        }

        public Dictionary<string, object> GetContentMetrics(string title, string description, string content)
        {
            var metrics = new Dictionary<string, object>();

            metrics["titleLength"] = title?.Length ?? 0;
            metrics["descriptionLength"] = description?.Length ?? 0;
            metrics["contentLength"] = content?.Length ?? 0;
            metrics["wordCount"] = CountWords(content);
            
            if (!string.IsNullOrEmpty(content))
            {
                metrics["headings"] = AnalyzeHeadings(content);
                metrics["imageCount"] = Regex.Matches(content, @"<img[^>]*>", RegexOptions.IgnoreCase).Count;
                metrics["linkCount"] = Regex.Matches(content, @"<a[^>]*>", RegexOptions.IgnoreCase).Count;
            }

            return metrics;
        }
    }
}