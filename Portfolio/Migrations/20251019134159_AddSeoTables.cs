using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Migrations
{
    public partial class AddSeoTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MetaDescription",
                table: "Proje",
                type: "nvarchar(160)",
                maxLength: 160,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaKeywords",
                table: "Proje",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaTitle",
                table: "Proje",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Proje",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaDescription",
                table: "Hakkimda",
                type: "nvarchar(160)",
                maxLength: 160,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaKeywords",
                table: "Hakkimda",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaTitle",
                table: "Hakkimda",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaDescription",
                table: "Blog",
                type: "nvarchar(160)",
                maxLength: 160,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaKeywords",
                table: "Blog",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaTitle",
                table: "Blog",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Blog",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BlogSeo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlogId = table.Column<int>(type: "int", nullable: false),
                    MetaTitle = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    MetaDescription = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: true),
                    MetaKeywords = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CanonicalUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    MetaRobots = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    OgTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OgDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    OgImage = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    OgType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    TwitterTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TwitterDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TwitterImage = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TwitterCard = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SchemaMarkup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeoScore = table.Column<int>(type: "int", nullable: false),
                    LastAnalyzed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SeoIssues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoIndex = table.Column<bool>(type: "bit", nullable: false),
                    NoFollow = table.Column<bool>(type: "bit", nullable: false),
                    FocusKeyword = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogSeo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogSeo_Blog_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GlobalSeo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiteName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SiteUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SiteDescription = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: true),
                    SiteKeywords = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DefaultLanguage = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    DefaultImage = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FacebookAppId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TwitterSite = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TwitterCreator = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LinkedinPage = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    GoogleAnalyticsId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    GoogleTagManagerId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    GoogleSearchConsoleId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BingWebmasterToolsId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RobotsTxt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomHeadTags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnableSeoAnalysis = table.Column<bool>(type: "bit", nullable: false),
                    EnableCanonicalUrls = table.Column<bool>(type: "bit", nullable: false),
                    EnableOpenGraph = table.Column<bool>(type: "bit", nullable: false),
                    EnableTwitterCards = table.Column<bool>(type: "bit", nullable: false),
                    EnableSitemap = table.Column<bool>(type: "bit", nullable: false),
                    EnableJsonLd = table.Column<bool>(type: "bit", nullable: false),
                    DefaultMetaTitle = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    DefaultMetaDescription = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: true),
                    DefaultMetaKeywords = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DefaultOgImage = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TwitterHandle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    YandexWebmasterToolsId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RobotsTxtContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastSitemapGenerated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GlobalSchemaMarkup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnableAutoSlug = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalSeo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfileSeo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MetaTitle = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    MetaDescription = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: true),
                    MetaKeywords = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CanonicalUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    MetaRobots = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    OgTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OgDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    OgImage = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    OgType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    TwitterTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TwitterDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TwitterImage = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TwitterCard = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SchemaMarkup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeoScore = table.Column<int>(type: "int", nullable: false),
                    LastAnalyzed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SeoIssues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PageType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileSeo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectSeo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    MetaTitle = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    MetaDescription = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: true),
                    MetaKeywords = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CanonicalUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    MetaRobots = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    OgTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OgDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    OgImage = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    OgType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    TwitterTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TwitterDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TwitterImage = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TwitterCard = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SchemaMarkup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeoScore = table.Column<int>(type: "int", nullable: false),
                    LastAnalyzed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SeoIssues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoIndex = table.Column<bool>(type: "bit", nullable: false),
                    NoFollow = table.Column<bool>(type: "bit", nullable: false),
                    FocusKeyword = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectSeo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectSeo_Proje_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Proje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeoAnalytics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContentType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContentId = table.Column<int>(type: "int", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SeoScore = table.Column<int>(type: "int", nullable: false),
                    TitleLength = table.Column<int>(type: "int", nullable: false),
                    DescriptionLength = table.Column<int>(type: "int", nullable: false),
                    KeywordCount = table.Column<int>(type: "int", nullable: false),
                    ContentLength = table.Column<int>(type: "int", nullable: false),
                    SeoIssues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeoRecommendations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnalyzedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ViewCount = table.Column<int>(type: "int", nullable: false),
                    ClickCount = table.Column<int>(type: "int", nullable: false),
                    ClickThroughRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeoAnalytics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SeoKeywords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Keyword = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContentId = table.Column<int>(type: "int", nullable: true),
                    Density = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    IsMainKeyword = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeoKeywords", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Proje_Slug",
                table: "Proje",
                column: "Slug",
                unique: true,
                filter: "[Slug] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Blog_Slug",
                table: "Blog",
                column: "Slug",
                unique: true,
                filter: "[Slug] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BlogSeo_BlogId",
                table: "BlogSeo",
                column: "BlogId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSeo_ProjectId",
                table: "ProjectSeo",
                column: "ProjectId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogSeo");

            migrationBuilder.DropTable(
                name: "GlobalSeo");

            migrationBuilder.DropTable(
                name: "ProfileSeo");

            migrationBuilder.DropTable(
                name: "ProjectSeo");

            migrationBuilder.DropTable(
                name: "SeoAnalytics");

            migrationBuilder.DropTable(
                name: "SeoKeywords");

            migrationBuilder.DropIndex(
                name: "IX_Proje_Slug",
                table: "Proje");

            migrationBuilder.DropIndex(
                name: "IX_Blog_Slug",
                table: "Blog");

            migrationBuilder.DropColumn(
                name: "MetaDescription",
                table: "Proje");

            migrationBuilder.DropColumn(
                name: "MetaKeywords",
                table: "Proje");

            migrationBuilder.DropColumn(
                name: "MetaTitle",
                table: "Proje");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Proje");

            migrationBuilder.DropColumn(
                name: "MetaDescription",
                table: "Hakkimda");

            migrationBuilder.DropColumn(
                name: "MetaKeywords",
                table: "Hakkimda");

            migrationBuilder.DropColumn(
                name: "MetaTitle",
                table: "Hakkimda");

            migrationBuilder.DropColumn(
                name: "MetaDescription",
                table: "Blog");

            migrationBuilder.DropColumn(
                name: "MetaKeywords",
                table: "Blog");

            migrationBuilder.DropColumn(
                name: "MetaTitle",
                table: "Blog");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Blog");
        }
    }
}
