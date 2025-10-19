using Portfolio.Models;

namespace Portfolio.Models.ViewModels
{
    public class SeoIndexViewModel
    {
        public int BlogCount { get; set; }
        public int ProjectCount { get; set; }
        public int BlogSeoCount { get; set; }
        public int ProjectSeoCount { get; set; }
        public List<SeoAnalytics> RecentAnalytics { get; set; } = new List<SeoAnalytics>();
        public GlobalSeo? GlobalSeo { get; set; }
    }
}
