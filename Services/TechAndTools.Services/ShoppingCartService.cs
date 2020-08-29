namespace TechAndTools.Services
{
    using Contracts;
    using Data;
    using Data.Models;
    using Mapping;
    using Models;

    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class ShoppingCartService : IShoppingCartService
    {
        private readonly TechAndToolsDbContext context;
        private readonly IProductService productService;
        private readonly IUserService userService;

        public ShoppingCartService(TechAndToolsDbContext context,
            IProductService productService,
            IUserService userService)
        {
            this.context = context;
            this.productService = productService;
            this.userService = userService;
        }

        public async Task<ShoppingCartProductServiceModel> AddToShoppingCartAsync(int productId, string username, int quantity)
        {
            var product = this.productService.GetProductById(productId);
            var user = this.userService.GetUserByUsername(username);

            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (quantity > product.QuantityInStock)
            {
                quantity = product.QuantityInStock;
            }

            var shoppingCartProduct = this.GetShoppingCartProduct(productId, user.ShoppingCartId);

            if (shoppingCartProduct != null)
            {
                shoppingCartProduct.Quantity++;
                this.context.ShoppingCartProducts.Update(shoppingCartProduct);
            }
            else
            {
                shoppingCartProduct = new ShoppingCartProduct
                {
                    ProductId = productId,
                    Quantity = quantity,
                    ShoppingCartId = user.ShoppingCartId
                };

                await this.context.ShoppingCartProducts.AddAsync(shoppingCartProduct);
            }

            await this.context.SaveChangesAsync();

            return shoppingCartProduct.To<ShoppingCartProductServiceModel>();
        }

        private ShoppingCartProduct GetShoppingCartProduct(int productId, int shoppingCartId)
        {
            return this.context.ShoppingCartProducts.FirstOrDefault(x => x.ShoppingCartId == shoppingCartId && x.ProductId == productId);
        }

        public IQueryable<ShoppingCartProductServiceModel> GetAllShoppingCartProducts(string username)
        {
            var user = this.userService.GetUserByUsername(username);

            return this.context.ShoppingCartProducts
                .Where(x => x.ShoppingCartId == user.ShoppingCartId)
                .To<ShoppingCartProductServiceModel>();
        }

        public async Task<bool> RemoveProductFromShoppingCart(int id, string username)
        {
            var product = this.productService.GetProductById(id);
            var user = this.userService.GetUserByUsername(username);

            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var shoppingCartProduct = GetShoppingCartProduct(product.Id, user.ShoppingCartId);

            this.context.ShoppingCartProducts.Remove(shoppingCartProduct);
            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public bool AnyProducts(string username)
        {
            return this.context.ShoppingCartProducts.Any(x => x.ShoppingCart.User.UserName == username);
        }

        public async Task<bool> RemoveAllProductFromShoppingCart(string username)
        {
            var user = this.userService.GetUserByUsername(username);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var shoppingCartProducts = this.context.ShoppingCartProducts
                .Where(x => x.ShoppingCartId == user.ShoppingCartId).ToList();

            this.context.ShoppingCartProducts.RemoveRange(shoppingCartProducts);

            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }
    }
}
