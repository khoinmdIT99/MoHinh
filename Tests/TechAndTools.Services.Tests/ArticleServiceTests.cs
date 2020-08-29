namespace TechAndTools.Services.Tests
{
    using Common;
    using Contracts;
    using Data;
    using Data.Models;
    using Models;
    
    using Xunit;
    using Microsoft.EntityFrameworkCore;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ArticleServiceTests
    {
        private List<Article> GetArticlesData()
        {
            return new List<Article>
            {
                new Article
                {
                    Id = 1,
                    Title = "Title1",
                    Content = "\r\n\r\nLorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas eu sapien faucibus, molestie quam vel, dictum massa. Quisque sed neque a nisi tempus aliquet ut id risus. Pellentesque nec finibus lectus, sed porta ligula. Mauris vitae mauris dictum, finibus diam non, rhoncus erat. Vivamus a aliquet metus, nec iaculis mauris. Ut varius ligula vel neque maximus, a convallis tellus ullamcorper. Pellentesque finibus neque massa, vel efficitur leo imperdiet in. Mauris ut turpis scelerisque, consectetur neque ut, hendrerit turpis. Sed vitae justo consequat risus lacinia vulputate. Suspendisse cursus consectetur nunc, bibendum rutrum ante molestie eget. Fusce sit amet orci eget massa fermentum cursus a in ipsum.\r\n\r\nCurabitur finibus dolor at sapien lacinia vestibulum. Integer gravida congue magna vel porttitor. Sed et urna et mauris ornare ultrices et quis mauris. Nulla facilisi. Donec a ipsum id purus interdum ornare a vitae ipsum. In hac habitasse platea dictumst. Proin sit amet metus quis elit placerat sodales. Donec lacinia nisl augue, non eleifend massa fermentum a. Nulla finibus quis mauris a blandit. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos.\r\n\r\nNunc sit amet diam eu odio tristique lacinia in eu mi. Aliquam ullamcorper consectetur neque, sed egestas diam maximus sed. Suspendisse blandit est eu mi dignissim ornare. Ut nec gravida felis, id rhoncus metus. Suspendisse ac viverra lectus. Integer in nisl et lacus tincidunt posuere vitae vel eros. Nulla facilisi.\r\n\r\nFusce suscipit diam turpis, eget pellentesque eros dignissim id. Nam nisi nunc, mollis nec pharetra in, feugiat in lorem. Pellentesque sit amet dolor molestie, venenatis urna ut, commodo tortor. Donec sodales auctor magna eu eleifend. Nunc cursus quam at lacinia suscipit. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Integer ac tincidunt metus. Praesent auctor urna justo, eu faucibus felis posuere id. Curabitur volutpat vitae quam eu consectetur. Sed non urna massa. Fusce eu fermentum metus. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Donec malesuada lacus ut nibh vulputate sodales. Sed sagittis porta massa, rhoncus iaculis lorem eleifend ac. Nullam interdum pulvinar ultrices.\r\n\r\nAliquam vel vestibulum eros, eu rutrum quam. Proin vehicula, odio eget efficitur scelerisque, ante tortor interdum mauris, at dapibus dui diam et mi. Suspendisse finibus, sem sit amet commodo mattis, orci urna interdum turpis, vitae commodo massa libero nec ante. Ut id lorem volutpat, scelerisque turpis sed, dapibus elit. Donec ac sem a eros hendrerit malesuada sed mollis elit. Integer euismod, felis quis ultricies ornare, elit nibh feugiat libero, quis consequat lectus turpis id purus. Vivamus finibus mauris eu finibus varius. Maecenas nec lorem eros. Suspendisse venenatis mi tincidunt ornare cursus. ",
                    AuthorId = "998DC52A-7FA4-461F-9F79-3F9720D64C01",
                    Image = new Image
                    {
                        ArticleId = 1,
                        ImageUrl = "Url"
                    }

                },
                new Article
                {
                    Id = 2,
                    Title = "Title2",
                    Content = "\r\n\r\nLorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas eu sapien faucibus, molestie quam vel, dictum massa. Quisque sed neque a nisi tempus aliquet ut id risus. Pellentesque nec finibus lectus, sed porta ligula. Mauris vitae mauris dictum, finibus diam non, rhoncus erat. Vivamus a aliquet metus, nec iaculis mauris. Ut varius ligula vel neque maximus, a convallis tellus ullamcorper. Pellentesque finibus neque massa, vel efficitur leo imperdiet in. Mauris ut turpis scelerisque, consectetur neque ut, hendrerit turpis. Sed vitae justo consequat risus lacinia vulputate. Suspendisse cursus consectetur nunc, bibendum rutrum ante molestie eget. Fusce sit amet orci eget massa fermentum cursus a in ipsum.\r\n\r\nCurabitur finibus dolor at sapien lacinia vestibulum. Integer gravida congue magna vel porttitor. Sed et urna et mauris ornare ultrices et quis mauris. Nulla facilisi. Donec a ipsum id purus interdum ornare a vitae ipsum. In hac habitasse platea dictumst. Proin sit amet metus quis elit placerat sodales. Donec lacinia nisl augue, non eleifend massa fermentum a. Nulla finibus quis mauris a blandit. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos.\r\n\r\nNunc sit amet diam eu odio tristique lacinia in eu mi. Aliquam ullamcorper consectetur neque, sed egestas diam maximus sed. Suspendisse blandit est eu mi dignissim ornare. Ut nec gravida felis, id rhoncus metus. Suspendisse ac viverra lectus. Integer in nisl et lacus tincidunt posuere vitae vel eros. Nulla facilisi.\r\n\r\nFusce suscipit diam turpis, eget pellentesque eros dignissim id. Nam nisi nunc, mollis nec pharetra in, feugiat in lorem. Pellentesque sit amet dolor molestie, venenatis urna ut, commodo tortor. Donec sodales auctor magna eu eleifend. Nunc cursus quam at lacinia suscipit. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Integer ac tincidunt metus. Praesent auctor urna justo, eu faucibus felis posuere id. Curabitur volutpat vitae quam eu consectetur. Sed non urna massa. Fusce eu fermentum metus. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Donec malesuada lacus ut nibh vulputate sodales. Sed sagittis porta massa, rhoncus iaculis lorem eleifend ac. Nullam interdum pulvinar ultrices.\r\n\r\nAliquam vel vestibulum eros, eu rutrum quam. Proin vehicula, odio eget efficitur scelerisque, ante tortor interdum mauris, at dapibus dui diam et mi. Suspendisse finibus, sem sit amet commodo mattis, orci urna interdum turpis, vitae commodo massa libero nec ante. Ut id lorem volutpat, scelerisque turpis sed, dapibus elit. Donec ac sem a eros hendrerit malesuada sed mollis elit. Integer euismod, felis quis ultricies ornare, elit nibh feugiat libero, quis consequat lectus turpis id purus. Vivamus finibus mauris eu finibus varius. Maecenas nec lorem eros. Suspendisse venenatis mi tincidunt ornare cursus. ",
                    Image = new Image
                    {
                        ArticleId = 2,
                        ImageUrl = "Url2"
                    }
                },
                new Article
                {
                    Id = 3,
                    Title = "Title3",
                    Content = "\r\n\r\nLorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas eu sapien faucibus, molestie quam vel, dictum massa. Quisque sed neque a nisi tempus aliquet ut id risus. Pellentesque nec finibus lectus, sed porta ligula. Mauris vitae mauris dictum, finibus diam non, rhoncus erat. Vivamus a aliquet metus, nec iaculis mauris. Ut varius ligula vel neque maximus, a convallis tellus ullamcorper. Pellentesque finibus neque massa, vel efficitur leo imperdiet in. Mauris ut turpis scelerisque, consectetur neque ut, hendrerit turpis. Sed vitae justo consequat risus lacinia vulputate. Suspendisse cursus consectetur nunc, bibendum rutrum ante molestie eget. Fusce sit amet orci eget massa fermentum cursus a in ipsum.\r\n\r\nCurabitur finibus dolor at sapien lacinia vestibulum. Integer gravida congue magna vel porttitor. Sed et urna et mauris ornare ultrices et quis mauris. Nulla facilisi. Donec a ipsum id purus interdum ornare a vitae ipsum. In hac habitasse platea dictumst. Proin sit amet metus quis elit placerat sodales. Donec lacinia nisl augue, non eleifend massa fermentum a. Nulla finibus quis mauris a blandit. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos.\r\n\r\nNunc sit amet diam eu odio tristique lacinia in eu mi. Aliquam ullamcorper consectetur neque, sed egestas diam maximus sed. Suspendisse blandit est eu mi dignissim ornare. Ut nec gravida felis, id rhoncus metus. Suspendisse ac viverra lectus. Integer in nisl et lacus tincidunt posuere vitae vel eros. Nulla facilisi.\r\n\r\nFusce suscipit diam turpis, eget pellentesque eros dignissim id. Nam nisi nunc, mollis nec pharetra in, feugiat in lorem. Pellentesque sit amet dolor molestie, venenatis urna ut, commodo tortor. Donec sodales auctor magna eu eleifend. Nunc cursus quam at lacinia suscipit. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Integer ac tincidunt metus. Praesent auctor urna justo, eu faucibus felis posuere id. Curabitur volutpat vitae quam eu consectetur. Sed non urna massa. Fusce eu fermentum metus. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Donec malesuada lacus ut nibh vulputate sodales. Sed sagittis porta massa, rhoncus iaculis lorem eleifend ac. Nullam interdum pulvinar ultrices.\r\n\r\nAliquam vel vestibulum eros, eu rutrum quam. Proin vehicula, odio eget efficitur scelerisque, ante tortor interdum mauris, at dapibus dui diam et mi. Suspendisse finibus, sem sit amet commodo mattis, orci urna interdum turpis, vitae commodo massa libero nec ante. Ut id lorem volutpat, scelerisque turpis sed, dapibus elit. Donec ac sem a eros hendrerit malesuada sed mollis elit. Integer euismod, felis quis ultricies ornare, elit nibh feugiat libero, quis consequat lectus turpis id purus. Vivamus finibus mauris eu finibus varius. Maecenas nec lorem eros. Suspendisse venenatis mi tincidunt ornare cursus. ",
                    Image = new Image
                    {
                        ArticleId = 3,
                        ImageUrl = "Url3"
                    }
                },
                new Article
                {
                    Id = 4,
                    Title = "Title4",
                    Content = "\r\n\r\nLorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas eu sapien faucibus, molestie quam vel, dictum massa. Quisque sed neque a nisi tempus aliquet ut id risus. Pellentesque nec finibus lectus, sed porta ligula. Mauris vitae mauris dictum, finibus diam non, rhoncus erat. Vivamus a aliquet metus, nec iaculis mauris. Ut varius ligula vel neque maximus, a convallis tellus ullamcorper. Pellentesque finibus neque massa, vel efficitur leo imperdiet in. Mauris ut turpis scelerisque, consectetur neque ut, hendrerit turpis. Sed vitae justo consequat risus lacinia vulputate. Suspendisse cursus consectetur nunc, bibendum rutrum ante molestie eget. Fusce sit amet orci eget massa fermentum cursus a in ipsum.\r\n\r\nCurabitur finibus dolor at sapien lacinia vestibulum. Integer gravida congue magna vel porttitor. Sed et urna et mauris ornare ultrices et quis mauris. Nulla facilisi. Donec a ipsum id purus interdum ornare a vitae ipsum. In hac habitasse platea dictumst. Proin sit amet metus quis elit placerat sodales. Donec lacinia nisl augue, non eleifend massa fermentum a. Nulla finibus quis mauris a blandit. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos.\r\n\r\nNunc sit amet diam eu odio tristique lacinia in eu mi. Aliquam ullamcorper consectetur neque, sed egestas diam maximus sed. Suspendisse blandit est eu mi dignissim ornare. Ut nec gravida felis, id rhoncus metus. Suspendisse ac viverra lectus. Integer in nisl et lacus tincidunt posuere vitae vel eros. Nulla facilisi.\r\n\r\nFusce suscipit diam turpis, eget pellentesque eros dignissim id. Nam nisi nunc, mollis nec pharetra in, feugiat in lorem. Pellentesque sit amet dolor molestie, venenatis urna ut, commodo tortor. Donec sodales auctor magna eu eleifend. Nunc cursus quam at lacinia suscipit. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Integer ac tincidunt metus. Praesent auctor urna justo, eu faucibus felis posuere id. Curabitur volutpat vitae quam eu consectetur. Sed non urna massa. Fusce eu fermentum metus. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Donec malesuada lacus ut nibh vulputate sodales. Sed sagittis porta massa, rhoncus iaculis lorem eleifend ac. Nullam interdum pulvinar ultrices.\r\n\r\nAliquam vel vestibulum eros, eu rutrum quam. Proin vehicula, odio eget efficitur scelerisque, ante tortor interdum mauris, at dapibus dui diam et mi. Suspendisse finibus, sem sit amet commodo mattis, orci urna interdum turpis, vitae commodo massa libero nec ante. Ut id lorem volutpat, scelerisque turpis sed, dapibus elit. Donec ac sem a eros hendrerit malesuada sed mollis elit. Integer euismod, felis quis ultricies ornare, elit nibh feugiat libero, quis consequat lectus turpis id purus. Vivamus finibus mauris eu finibus varius. Maecenas nec lorem eros. Suspendisse venenatis mi tincidunt ornare cursus. ",
                    Image = new Image
                    {
                        ArticleId = 4,
                        ImageUrl = "Url4"
                    }
                }
            };
        }

        private async Task SeedData(TechAndToolsDbContext context)
        {
            context.AddRange(GetArticlesData());
            await context.SaveChangesAsync();
        }

        public ArticleServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Fact]
        public async void CreateArticleAsync_ShouldCreateAndAddArticlesToDatabase()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "CreateArticleAsync_ShouldCreateAndAddArticlesToDatabase")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);

            IArticleService articleService = new ArticleService(context);

            string authorId = "998DC52A-7FA4-461F-9F79-3F9720D64C01";

            int articleId = 111;

            var actualResult = await articleService.CreateArticleAsync(new ArticleServiceModel
            {
                Id = articleId,
                Title = "title1",
                Content = "content1111111111111111111",
                Image = new ImageServiceModel
                {
                    ArticleId = articleId,
                    ImageUrl = "url"
                }
            }, authorId);

            int expectedCount = 1;
            var actualCount = context.Articles.ToList().Count;

            Assert.Equal(actualCount, expectedCount);
        }

        [Fact]
        public async void EditArticleAsync_ShouldEditArticleById()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "CreateArticleAsync_ShouldCreateAndAddArticlesToDatabase")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);

            IArticleService articleService = new ArticleService(context);

            await this.SeedData(context);

            var actualResult = await articleService.EditArticleAsync(new ArticleServiceModel
            {
                Id = 1,
                Title = "new title",
                Content = "new content",
                AuthorId = "998DC52A-7FA4-461F-9F79-3F9720D64C01",
                Image = new ImageServiceModel
                {
                    ArticleId = 1,
                    ImageUrl = "Url"
                }
            });


            Assert.Equal("new title", actualResult.Title);
            Assert.Equal("new content", actualResult.Content);
        }
        
        [Fact]
        public async void EditArticleAsync_ShouldThrowArgumentNullExceptionWithInvalidArticle()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "EditArticleAsync_ShouldThrowArgumentNullExceptionWithInvalidArticleId")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);

            IArticleService articleService = new ArticleService(context);

            await this.SeedData(context);

            ArticleServiceModel invalidArticle = new ArticleServiceModel();

            await Assert.ThrowsAsync<ArgumentNullException>(() => articleService.EditArticleAsync(invalidArticle));
        }
        
        [Fact]
        public async void DeleteArticleByIdAsync_ShouldDeleteArticleFromDatabaseById()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "DeleteArticleByIdAsync_ShouldDeleteArticleFromDatabaseById")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);

            IArticleService articleService = new ArticleService(context);

            await this.SeedData(context);

            int articleId = 1;

            int expectedCount = context.Articles.ToList().Count - 1;

            bool actualResult = await articleService.DeleteArticleByIdAsync(articleId);

            int actualCount = context.Articles.ToList().Count;

            Assert.True(actualResult);
            Assert.Equal(expectedCount, actualCount);
        }
        
        [Fact]
        public async void DeleteArticleByIdAsync_ShouldThrowArgumentNullExceptionWithInvalidArticleId()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "DeleteArticleByIdAsync_ShouldThrowArgumentNullExceptionWithInvalidArticleId")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);

            IArticleService articleService = new ArticleService(context);

            await this.SeedData(context);

            int invalidArticleId = 1111;

            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                articleService.DeleteArticleByIdAsync(invalidArticleId));
        }

        [Fact]
        public async void GetArticleAsync_ShouldReturnArticleById()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "GetArticleAsync_ShouldReturnArticleById")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);

            IArticleService articleService = new ArticleService(context);

            await this.SeedData(context);

            int articleId = 1;

            var actualResult = await articleService.GetArticleAsync(articleId);
            
            Assert.NotNull(actualResult);
            Assert.Equal(articleId, actualResult.Id);
        }

        [Fact]
        public async void GetArticleAsync_ShouldThrowArgumentNullExceptionWithInvalidArticleId()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "GetArticleAsync_ShouldThrowArgumentNullExceptionWithInvalidArticleId")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);

            IArticleService articleService = new ArticleService(context);

            await this.SeedData(context);

            int invalidArticleId = 333;

            await Assert.ThrowsAsync<ArgumentNullException>(() => articleService.GetArticleAsync(invalidArticleId));
        }

        [Fact]
        public async void GetAllArticles_ShouldReturnAllArticlesFromDatabase()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "GetAllArticles_ShouldReturnAllArticlesFromDatabase")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);

            IArticleService articleService = new ArticleService(context);

            await this.SeedData(context);

            List<Article> expectedResult = await context.Articles.ToListAsync();
            List<ArticleServiceModel> actualResult = await articleService.GetAllArticles().ToListAsync();

            Assert.Equal(expectedResult.Count, actualResult.Count);
        }

        [Fact]
        public async void GetAllByUserIdAsync_ShouldReturnAllArticlesFromDatabaseByUserId()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "GetAllByUserIdAsync_ShouldReturnAllArticlesFromDatabaseByUserId")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);

            IArticleService articleService = new ArticleService(context);

            await this.SeedData(context);

            string authorId = "998DC52A-7FA4-461F-9F79-3F9720D64C01";

            int expectedResult = 1;

            List<ArticleServiceModel> actualResult = await articleService.GetAllByUserId(authorId).ToListAsync();

            Assert.Equal(expectedResult, actualResult.Count);
        }

        [Fact]
        public async void GetLastThreeArticles_ShouldReturnLastThreeArticlesExceptArticleWithArticleId()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "GetLastThreeArticles_ShouldReturnLastThreeArticlesExceptArticleWithArticleId")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);

            IArticleService articleService = new ArticleService(context);

            await this.SeedData(context);

            int articleId = 1;

            int expectedResult = 3;

            List<ArticleServiceModel> actualResult = await articleService.GetLastThreeArticles(articleId).ToListAsync();

            Assert.Equal(expectedResult, actualResult.Count);
        }
    }
}
