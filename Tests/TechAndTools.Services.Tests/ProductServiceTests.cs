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

    public class ProductServiceTests
    {
        private List<Product> GetProductsData()
        {
            return new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "product1",
                    Description = "Description1",
                    Price = 1,
                    Warranty = 1,
                    QuantityInStock = 1,
                    ManualUrl = "manual url",
                    Brand = new Brand
                    {
                        Id = 1,
                        LogoUrl = "logoUrl",
                        Name = "brand1",
                        OfficialSite = "site1"
                    },
                    ProductCategory = new Category
                    {
                        Id = 1,
                        MainCategory = new MainCategory
                        {
                            Id = 1,
                            Name = "MainCategory1"
                        }
                    },
                    Images = new List<Image>
                    {
                        new Image
                        {
                            ProductId = 1,
                            ImageUrl = "url1"
                        }
                    },
                },
                new Product
                {
                    Id = 2,
                    Name = "product2",
                    Description = "Description2",
                    Price = 2,
                    Warranty = 2,
                    QuantityInStock = 2,
                    ManualUrl = "manual url2",
                    Brand = new Brand
                    {
                        Id = 2,
                        LogoUrl = "logoUrl2",
                        Name = "brand2",
                        OfficialSite = "site2"
                    },
                    ProductCategory = new Category
                    {
                        Id = 2,
                        MainCategory = new MainCategory
                        {
                            Id = 2,
                            Name = "MainCategory2"
                        }
                    },
                    Images = new List<Image>
                    {
                        new Image
                        {
                            ProductId = 2,
                            ImageUrl = "url2"
                        }
                    },
                },
                new Product
                {
                    Id = 3,
                    Name = "product3",
                    Description = "Description3",
                    Price = 3,
                    Warranty = 3,
                    QuantityInStock = 3,
                    ManualUrl = "manual url3",
                    Brand = new Brand
                    {
                        Id = 3,
                        LogoUrl = "logoUrl3",
                        Name = "brand3",
                        OfficialSite = "site3"
                    },
                    ProductCategory = new Category
                    {
                        Id = 3,
                        MainCategory = new MainCategory
                        {
                            Id = 3,
                            Name = "MainCategory3"
                        }
                    },
                    Images = new List<Image>
                    {
                        new Image
                        {
                            ProductId = 3,
                            ImageUrl = "url3"
                        }
                    },
                },
            };
        }

        private TechAndToolsUser GetTestUser()
        {
            return new TechAndToolsUser
            {
                Id = "asdasd",
                UserName = "TestUsername",
                Email = "Test@test.com",
                FirstName = "TestFirstName",
                LastName = "TestLastName",
            };
        }

        private List<FavoriteProduct> GetFavoriteProductsData()
        {
            return new List<FavoriteProduct>
            {
                new FavoriteProduct
                {
                    ProductId = 2,
                    UserId = "asdasd"
                },
                new FavoriteProduct
                {
                    ProductId = 3,
                    UserId = "asdasd"
                }
            };
        }

        private async Task SeedData(TechAndToolsDbContext context)
        {
            context.AddRange(this.GetProductsData());
            context.Users.Add(this.GetTestUser());
            context.FavoriteProducts.AddRange(this.GetFavoriteProductsData());
            await context.SaveChangesAsync();
        }

        public ProductServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateProduct()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "CreateAsync_ShouldCreateProduct")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);
            IProductService productService = new ProductService(context);

            var productServiceModel1 = new ProductServiceModel
            {
                Name = "product1",
                Description = "Description1",
                Price = 1,
                Warranty = 1,
                QuantityInStock = 1,
                ManualUrl = "manual url",
                BrandId = 1,
                ProductCategoryId = 1

            };

            var productServiceModel2 = new ProductServiceModel
            {
                Name = "product2",
                Description = "Description2",
                Price = 2,
                Warranty = 2,
                QuantityInStock = 2,
                ManualUrl = "manual url",
                BrandId = 2,
                ProductCategoryId = 2

            };

            await productService.CreateAsync(productServiceModel1);
            await productService.CreateAsync(productServiceModel2);

            const int expectedResult = 2;

            int actualResult = context.Products.Count();

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task EditAsync_ShouldEditProductById()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "EditAsync_ShouldEditProductById")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);
            await SeedData(context);
            IProductService productService = new ProductService(context);

            var productServiceModel = new ProductServiceModel
            {
                Id = 1,
                Name = "new",
                Description = "new",
                Price = 2,
                Warranty = 2,
                QuantityInStock = 2,
                ManualUrl = "new",
                BrandId = 2,
                ProductCategoryId = 2

            };

            var actualData = await productService.EditAsync(productServiceModel);

            Assert.Equal(productServiceModel.Name, actualData.Name);
            Assert.Equal(productServiceModel.Description, actualData.Description);
            Assert.Equal(productServiceModel.ProductCategoryId, actualData.ProductCategoryId);
            Assert.Equal(productServiceModel.Price, actualData.Price);
            Assert.Equal(productServiceModel.Warranty, actualData.Warranty);
            Assert.Equal(productServiceModel.QuantityInStock, actualData.QuantityInStock);
            Assert.Equal(productServiceModel.BrandId, actualData.BrandId);
            Assert.Equal(productServiceModel.ManualUrl, actualData.ManualUrl);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteProductById()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "DeleteAsync_ShouldDeleteProductById")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);
            await SeedData(context);
            IProductService productService = new ProductService(context);

            const int productId = 1;

            bool actualResult = await productService.DeleteAsync(productId);

            Assert.True(actualResult);
        }

        [Fact]
        public async Task DeleteAsync_WithIncorrectIdShouldThrowException()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "DeleteAsync_WithIncorrectIdShouldThrowException")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);
            await SeedData(context);
            IProductService productService = new ProductService(context);

            const int wrongId = int.MaxValue;

            await Assert.ThrowsAsync<ArgumentNullException>(() => productService.DeleteAsync(wrongId));
        }

        [Fact]
        public async Task GetAllProducts_ShouldReturnAllProductsFromDatabase()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "GetAllProducts_ShouldReturnAllProductsFromDatabase")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);
            await SeedData(context);
            IProductService productService = new ProductService(context);

            int expectedResult = context.Products.Count();
            int actualResult = productService.GetAllProducts().Count();

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task GetProductsByCategoryId_ShouldReturnAllProductsByCategoryId()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "GetProductsByCategoryId_ShouldReturnAllProductsByCategoryId")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);
            await SeedData(context);
            IProductService productService = new ProductService(context);

            const int categoryId = 1;

            int expectedResult = context.Products.Where(x => x.ProductCategoryId == categoryId).ToList().Count;
            int actualResult = productService.GetProductsByCategoryId(categoryId).Count();

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task GetProductById_ShouldReturnProductById()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "GetProductById_ShouldReturnProductById")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);
            await SeedData(context);
            IProductService productService = new ProductService(context);

            const int productId = 1;

            int expectedResult = productId;
            int actualResult = productService.GetProductById(productId).Id;

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task AddToFavoritesAsync_ShouldAddProductToFavorites()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "AddToFavoritesAsync_ShouldAddProductToFavorites")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);
            await SeedData(context);
            IProductService productService = new ProductService(context);

            const int productId = 1;
            const string testUserId = "asdasd";
            const string testUsername = "TestUsername";

            var actualResult = await productService.AddToFavoritesAsync(productId, testUsername);

            Assert.NotNull(actualResult);
            Assert.Equal(productId, actualResult.ProductId);
            Assert.Equal(testUserId, actualResult.UserId);
        }

        [Fact]
        public async Task AddToFavoritesAsync_WithIncorrectProductShouldThrowException()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "AddToFavoritesAsync_WithIncorrectProductShouldThrowException")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);
            await SeedData(context);
            IProductService productService = new ProductService(context);

            const int productId = int.MaxValue;
            const string testUsername = "TestUsername";

            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                productService.AddToFavoritesAsync(productId, testUsername));
        }

        [Fact]
        public async Task AddToFavoritesAsync_WithIncorrectUsernameShouldThrowException()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "AddToFavoritesAsync_WithIncorrectUsernameShouldThrowException")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);
            await SeedData(context);
            IProductService productService = new ProductService(context);

            const int productId = 1;
            const string testUsername = null;

            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                productService.AddToFavoritesAsync(productId, testUsername));
        }

        [Fact]
        public async Task RemoveFromFavorites_ShouldRemoveFavoriteProductFromDatabaseByIdAndUsername()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "RemoveFromFavorites_ShouldRemoveFavoriteProductFromDatabaseByIdAndUsername")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);
            await SeedData(context);
            IProductService productService = new ProductService(context);

            const int testProductId = 3;
            const string testUsername = "TestUsername";

            bool actualResult = await productService.RemoveFromFavorites(testProductId, testUsername);

            Assert.True(actualResult);
        }

        [Fact]
        public async Task RemoveFromFavorites_WithIncorrectUserShouldThrowException()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "RemoveFromFavorites_WithIncorrectUserShouldThrowException")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);
            await SeedData(context);
            IProductService productService = new ProductService(context);

            const int testProductId = 3;
            const string testUsername = null;

            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                productService.RemoveFromFavorites(testProductId, testUsername));
        }

        [Fact]
        public async Task RemoveFromFavorites_WithIncorrectProductIdShouldThrowException()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "RemoveFromFavorites_WithIncorrectProductIdShouldThrowException")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);
            await SeedData(context);
            IProductService productService = new ProductService(context);

            const int testProductId = int.MaxValue;
            const string testUsername = "TestUsername";

            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                productService.RemoveFromFavorites(testProductId, testUsername));
        }

        [Fact]
        public async Task AllFavoriteProducts_ShouldReturnAllFavoriteProductByUser()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "AllFavoriteProducts_ShouldReturnAllFavoriteProductByUser")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);
            await SeedData(context);
            IProductService productService = new ProductService(context);

            const string testUsername = "TestUsername";
            const int expectedResult = 2;

            int actualResult = productService.AllFavoriteProducts(testUsername).Count();

            Assert.Equal(expectedResult, actualResult);
        }
    }
}
