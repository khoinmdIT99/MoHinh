namespace TechAndTools.Services.Tests
{
    using Common;
    using Contracts;
    using Data;
    using Data.Models;
    using Mapping;
    using Models;
    
    using Moq;
    using Xunit;
    using Microsoft.EntityFrameworkCore;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ShoppingCartServiceTests
    {

        private List<Product> GetProductsData()
        {
            return new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "product1"
                },
                new Product
                {
                    Id = 2,
                    Name = "product2"
                }
            };
        }

        private List<TechAndToolsUser> GetUsersData()
        {
            return new List<TechAndToolsUser>
            {
                new TechAndToolsUser
                {
                    Id = "id1",
                    UserName = "testUser1",
                    ShoppingCart = new ShoppingCart
                    {
                        Id = 1,
                        ShoppingCartProducts = new List<ShoppingCartProduct>
                        {
                            new ShoppingCartProduct
                            {
                                ProductId = 1,
                                Quantity = 1,
                                ShoppingCartId = 1
                            }
                        }
                    }
                },
                new TechAndToolsUser
                {
                    Id = "id2",
                    UserName = "testUser2",
                    ShoppingCart = new ShoppingCart
                    {
                        Id = 2,
                        ShoppingCartProducts = new List<ShoppingCartProduct>()
                    }
                },
            };
        }

        private async Task SeedData(TechAndToolsDbContext context)
        {
            context.Products.AddRange(this.GetProductsData());
            context.Users.AddRange(this.GetUsersData());
            await context.SaveChangesAsync();
        }

        public ShoppingCartServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Fact]
        public async Task AddProductInShoppingCartAsync_WithAlreadyAddedProductInShoppingCartShouldIncreaseShoppingCartProductQuantity()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "AddProductInShoppingCartAsync_WithAlreadyAddedProductInShoppingCartShouldIncreaseShoppingCartProductQuantity")
                .Options;
            var context = new TechAndToolsDbContext(options);
            await SeedData(context);

            const string username = "testUser1";
            const int productId = 1;
            const int defaultQuantity = 1;
            const int expectedCount = 1;

            var userService = new Mock<IUserService>();
            userService.Setup(r => r.GetUserByUsername(username))
                .Returns(context.Users.FirstOrDefault(x => x.UserName == username));

            var productService = new Mock<IProductService>();
            productService.Setup(p => p.GetProductById(productId))
                .Returns(context.Products.Find(productId).To<ProductServiceModel>());

            IShoppingCartService shoppingCartsService = new ShoppingCartService(context, productService.Object, userService.Object);

            await shoppingCartsService.AddToShoppingCartAsync(productId, username, defaultQuantity);

            var user = userService.Object.GetUserByUsername(username);

            Assert.True(user.ShoppingCart.ShoppingCartProducts.Any());
            Assert.Equal(expectedCount, user.ShoppingCart.ShoppingCartProducts.Count);
        }

        [Fact]
        public async Task AddProductInShoppingCartAsync_ShouldAddProductInShoppingCart()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "AddProductInShoppingCartAsync_ShouldAddProductInShoppingCart")
                .Options;
            var context = new TechAndToolsDbContext(options);
            await SeedData(context);

            const string username = "testUser1";
            const int productId = 2;
            const int defaultQuantity = 1;
            const int expectedCount = 2;

            var userService = new Mock<IUserService>();
            userService.Setup(r => r.GetUserByUsername(username))
                .Returns(context.Users.FirstOrDefault(x => x.UserName == username));

            var productService = new Mock<IProductService>();
            productService.Setup(p => p.GetProductById(productId))
                .Returns(context.Products.Find(productId).To<ProductServiceModel>());

            IShoppingCartService shoppingCartsService = new ShoppingCartService(context, productService.Object, userService.Object);

            await shoppingCartsService.AddToShoppingCartAsync(productId, username, defaultQuantity);

            var user = userService.Object.GetUserByUsername(username);

            Assert.True(user.ShoppingCart.ShoppingCartProducts.Any());
            Assert.Equal(expectedCount, user.ShoppingCart.ShoppingCartProducts.Count);
        }

        [Fact]
        public async Task AddProductInShoppingCartAsync_WithIncorrectUserShouldThrowException()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "AddProductInShoppingCartAsync_WithIncorrectUserShouldThrowException")
                .Options;
            var context = new TechAndToolsDbContext(options);
            await SeedData(context);

            const string username = "";
            const int productId = 1;
            const int defaultQuantity = 1;

            var userService = new Mock<IUserService>();
            userService.Setup(r => r.GetUserByUsername(username))
                .Returns(context.Users.FirstOrDefault(x => x.UserName == username));

            var productService = new Mock<IProductService>();
            productService.Setup(p => p.GetProductById(productId))
                .Returns(context.Products.Find(productId).To<ProductServiceModel>());

            IShoppingCartService shoppingCartsService = new ShoppingCartService(context, productService.Object, userService.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                shoppingCartsService.AddToShoppingCartAsync(productId, username, defaultQuantity));
        }

        [Fact]
        public async Task AddProductInShoppingCartAsync_WithIncorrectProductShouldThrowException()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "AddProductInShoppingCartAsync_WithIncorrectProductShouldThrowException")
                .Options;
            var context = new TechAndToolsDbContext(options);
            await SeedData(context);

            const string username = "testUser1";
            const int productId = int.MaxValue;
            const int defaultQuantity = 1;

            var userService = new Mock<IUserService>();
            userService.Setup(r => r.GetUserByUsername(username))
                .Returns(context.Users.FirstOrDefault(x => x.UserName == username));

            var productService = new Mock<IProductService>();

            IShoppingCartService shoppingCartsService = new ShoppingCartService(context, productService.Object, userService.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                shoppingCartsService.AddToShoppingCartAsync(productId, username, defaultQuantity));
        }

        [Fact]
        public async Task GetAllShoppingCartProducts_ShouldReturnAllShoppingCartProductsByUsername()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "GetAllShoppingCartProducts_ShouldReturnAllShoppingCartProductsByUsername")
                .Options;
            var context = new TechAndToolsDbContext(options);
            await SeedData(context);

            const string username = "testUser2";
            const int productId = 2;

            var userService = new Mock<IUserService>();
            userService.Setup(r => r.GetUserByUsername(username))
                .Returns(context.Users.FirstOrDefault(x => x.UserName == username));
            var productService = new Mock<IProductService>();
            productService.Setup(p => p.GetProductById(productId))
                .Returns(context.Products.Find(productId).To<ProductServiceModel>());
            IShoppingCartService shoppingCartsService = new ShoppingCartService(context, productService.Object, userService.Object);

            var user = userService.Object.GetUserByUsername(username);
            int expectedResult = user.ShoppingCart.ShoppingCartProducts.Count;
            int actualResult = shoppingCartsService.GetAllShoppingCartProducts(username).Count();

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task RemoveProductFromShoppingCart_ShouldRemoveProductFromShoppingCartAndReturnTrue()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "RemoveProductFromShoppingCart_ShouldRemoveProductFromShoppingCartAndReturnTrue")
                .Options;
            var context = new TechAndToolsDbContext(options);
            await SeedData(context);

            const string username = "testUser1";
            const int productId = 1;

            var userService = new Mock<IUserService>();
            userService.Setup(r => r.GetUserByUsername(username))
                .Returns(context.Users.FirstOrDefault(x => x.UserName == username));
            var productService = new Mock<IProductService>();
            productService.Setup(p => p.GetProductById(productId))
                .Returns(context.Products.Find(productId).To<ProductServiceModel>());
            IShoppingCartService shoppingCartsService = new ShoppingCartService(context, productService.Object, userService.Object);

            bool result = await shoppingCartsService.RemoveProductFromShoppingCart(productId, username);

            Assert.True(result);
        }

        [Fact]
        public async Task RemoveProductFromShoppingCart_WithIncorrectUsernameShouldThrowException()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "RemoveProductFromShoppingCart_WithIncorrectUsernameShouldThrowException")
                .Options;
            var context = new TechAndToolsDbContext(options);
            await SeedData(context);

            const string username = "";
            const int productId = 1;

            var userService = new Mock<IUserService>();
            var productService = new Mock<IProductService>();
            productService.Setup(p => p.GetProductById(productId))
                .Returns(context.Products.Find(productId).To<ProductServiceModel>());
            IShoppingCartService shoppingCartsService = new ShoppingCartService(context, productService.Object, userService.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                shoppingCartsService.RemoveProductFromShoppingCart(productId, username));
        }

        [Fact]
        public async Task RemoveProductFromShoppingCart_WithIncorrectProductShouldThrowException()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "RemoveProductFromShoppingCart_WithIncorrectProductShouldThrowException")
                .Options;
            var context = new TechAndToolsDbContext(options);
            await SeedData(context);

            const string username = "testUser1";
            const int productId = 0;

            var userService = new Mock<IUserService>();
            userService.Setup(r => r.GetUserByUsername(username))
                .Returns(context.Users.FirstOrDefault(x => x.UserName == username));
            var productService = new Mock<IProductService>();
            IShoppingCartService shoppingCartsService = new ShoppingCartService(context, productService.Object, userService.Object);
            
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                shoppingCartsService.RemoveProductFromShoppingCart(productId, username));
        }

        [Fact]
        public async Task RemoveAllProductFromShoppingCart_ShouldRemoveAllProductsFromShoppingCart()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "RemoveAllProductFromShoppingCart_ShouldRemoveAllProductsFromShoppingCart")
                .Options;
            var context = new TechAndToolsDbContext(options);
            await SeedData(context);

            const string username = "testUser1";
            const int productId = 1;

            var userService = new Mock<IUserService>();
            userService.Setup(r => r.GetUserByUsername(username))
                .Returns(context.Users.FirstOrDefault(x => x.UserName == username));
            var productService = new Mock<IProductService>();
            productService.Setup(p => p.GetProductById(productId))
                .Returns(context.Products.Find(productId).To<ProductServiceModel>());
            IShoppingCartService shoppingCartsService = new ShoppingCartService(context, productService.Object, userService.Object);

            bool result = await shoppingCartsService.RemoveAllProductFromShoppingCart(username);

            Assert.True(result);
        }

        [Fact]
        public async Task RemoveAllProductFromShoppingCart_WithIncorrectUserShouldThrowException()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "RemoveAllProductFromShoppingCart_WithIncorrectUserShouldThrowException")
                .Options;
            var context = new TechAndToolsDbContext(options);
            await SeedData(context);

            const string username = "";

            var userService = new Mock<IUserService>();
            var productService = new Mock<IProductService>();
            IShoppingCartService shoppingCartsService = new ShoppingCartService(context, productService.Object, userService.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                shoppingCartsService.RemoveAllProductFromShoppingCart(username));
        }

        [Fact]
        public async Task AnyProducts_ShouldReturnIsAnyProductsInShoppingCart()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "AnyProducts_ShouldReturnIsAnyProductsInShoppingCart")
                .Options;
            var context = new TechAndToolsDbContext(options);
            await SeedData(context);

            const string username = "testUser1";
            const int productId = 1;

            var userService = new Mock<IUserService>();
            userService.Setup(r => r.GetUserByUsername(username))
                .Returns(context.Users.FirstOrDefault(x => x.UserName == username));
            var productService = new Mock<IProductService>();
            productService.Setup(p => p.GetProductById(productId))
                .Returns(context.Products.Find(productId).To<ProductServiceModel>());
            IShoppingCartService shoppingCartsService = new ShoppingCartService(context, productService.Object, userService.Object);

            bool result = shoppingCartsService.AnyProducts(username);

            Assert.True(result);
        }
    }
}
