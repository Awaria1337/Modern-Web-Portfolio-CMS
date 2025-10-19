using static Portfolio.Services.SeoAnalysisService;

namespace Portfolio.Services
{
    public interface ISeoAnalysisService
    {
        SeoAnalysisResult AnalyzeContent(string title, string description, string content, string keywords = "");
        string GetSeoScoreColor(int score);
        string GetSeoScoreText(int score);
    }
}