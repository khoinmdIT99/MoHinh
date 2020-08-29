namespace TechAndTools.Services.Tests
{
    using Common;
    using Contracts;
    using Data;
    using Data.Models;
    
    using Xunit;
    using Microsoft.EntityFrameworkCore;
    
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ImageServiceTests
    {
        private List<Image> GetImagesData()
        {
            return new List<Image>
            {
                new Image
                {
                    Id = 1,
                    ImageUrl = "url1",
                    ProductId = 1
                },
                new Image
                {
                    Id = 2,
                    ImageUrl = "url2",
                    ProductId = 2
                },
                new Image
                {
                    Id = 3,
                    ImageUrl = "url3",
                    ArticleId = 1
                }
            };
        }

        private List<Article> GetArticlesData()
        {
            return new List<Article>
            {
                new Article
                {
                    Id = 1,
                    Title = "title",
                    Content = "content"
                }
            };
        }

        private List<Product> GetProductsData()
        {
            return new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "product1",
                    Images = new List<Image>()
                }
            };
        }
        private async Task SeedData(TechAndToolsDbContext context)
        {
            context.AddRange(this.GetProductsData());
            context.AddRange(this.GetArticlesData());
            context.AddRange(this.GetImagesData());
            await context.SaveChangesAsync();
        }

        public ImageServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Fact]
        public async Task CreateWithProductAsync_ShouldCreateImageWithProduct()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "CreateWithProductAsync_ShouldCreateImageWithProduct")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);

            IImageService imageService = new ImageService(context);

            await imageService.CreateWithProductAsync(imageUrl: "imageurl1", productId: 1);
            await imageService.CreateWithProductAsync(imageUrl: "imageurl2", productId: 2);


            const int expectedResult = 2;

            int actualResult = context.Images.Count();

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task CreateWithArticleAsync_ShouldCreateImageWithArticle()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "CreateWithArticleAsync_ShouldCreateImageWithArticle")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);

            IImageService imageService = new ImageService(context);

            await imageService.CreateWithArticleAsync(imageUrl: "imageurl2", 1);


            const int expectedResult = 1;

            int actualResult = context.Images.Count();

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task EditWithArticleAsync_ShouldEditImageWithArticle()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "EditWithArticleAsync_ShouldEditImageWithArticle")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);
            await SeedData(context);
            IImageService imageService = new ImageService(context);

            const int articleId = 1;
            const string imageUrl = "new Url";

            var actualResult = await imageService.EditWithArticleAsync(imageUrl, articleId);

            Assert.NotNull(actualResult);
            Assert.Equal(imageUrl, actualResult.ImageUrl);
        }

        [Fact]
        public async Task EditWithArticleAsync_WithIncorrectArticleShouldThrowException()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "EditWithArticleAsync_WithIncorrectArticleShouldThrowException")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);
            await SeedData(context);
            IImageService imageService = new ImageService(context);

            const int articleId = 0;
            const string imageUrl = "new Url";

            await Assert.ThrowsAsync<
                ArgumentNullException>(() => imageService.EditWithArticleAsync(imageUrl, articleId));
        }
    }
}
