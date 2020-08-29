namespace TechAndTools.Services
{
    using Contracts;
    using Data;
    using Data.Models;
    using Mapping;
    using Models;

    using Microsoft.EntityFrameworkCore;

    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProductService : IProductService
    {
        private readonly TechAndToolsDbContext context;

        public ProductService(TechAndToolsDbContext context)
        {
            this.context = context;
        }

        public async Task<ProductServiceModel> CreateAsync(ProductServiceModel productServiceModel)
        {
            Product product = productServiceModel.To<Product>();

            await this.context.Products.AddAsync(product);
            await this.context.SaveChangesAsync();

            return product.To<ProductServiceModel>();
        }

        public async Task<ProductServiceModel> EditAsync(ProductServiceModel productServiceModel)
        {
            Product productFromDb = this.context.Products
                .Find(productServiceModel.Id);

            productFromDb.Name = productServiceModel.Name;
            productFromDb.ManualUrl = productServiceModel.ManualUrl;
            productFromDb.Description = productServiceModel.Description;
            productFromDb.Price = productServiceModel.Price;
            productFromDb.Warranty = productServiceModel.Warranty;
            productFromDb.BrandId = productServiceModel.BrandId;
            productFromDb.ProductCategoryId = productServiceModel.ProductCategoryId;
            productFromDb.QuantityInStock = productServiceModel.QuantityInStock;

            this.context.Products.Update(productFromDb);
            await this.context.SaveChangesAsync();

            return productFromDb.To<ProductServiceModel>();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Product productFromDb = this.context.Products
                .Find(id);

            if (productFromDb == null)
            {
                throw new ArgumentNullException(nameof(productFromDb));
            }

            this.context.Products.Remove(productFromDb);
            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public IQueryable<ProductServiceModel> GetAllProducts()
        {
            return this.context.Products
                .To<ProductServiceModel>();
        }

        public IQueryable<ProductServiceModel> GetProductsByCategoryId(int categoryId)
        {
            return this.context.Products
                .Where(product => product.ProductCategoryId == categoryId)
                .To<ProductServiceModel>();
        }

        public ProductServiceModel GetProductById(int id)
        {
            return this.context.Products
                .To<ProductServiceModel>()
                .SingleOrDefault(x => x.Id == id);
        }

        public async Task<FavoriteProductsServiceModel> AddToFavoritesAsync(int id, string username)
        {
            var user = this.context.Users
                .Include(x => x.FavoriteProducts)
                .FirstOrDefault(x => x.UserName == username);
            
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            bool isProductExist = this.context.Products
                .Any(x => x.Id == id);

            if (!isProductExist)
            {
                throw new ArgumentNullException(nameof(isProductExist));
            }

            var favoriteProduct = new FavoriteProduct
            {
                ProductId = id,
                UserId = user.Id
            };

            await this.context.FavoriteProducts.AddAsync(favoriteProduct);
            await this.context.SaveChangesAsync();

            return favoriteProduct.To<FavoriteProductsServiceModel>();
        }

        public IQueryable<FavoriteProductsServiceModel> AllFavoriteProducts(string username)
        {
            var favoriteProducts = this.context.FavoriteProducts
                .Where(x => x.User.UserName == username)
                .To<FavoriteProductsServiceModel>();
            
            return favoriteProducts;
        }

        public async Task<bool> RemoveFromFavorites(int id, string username)
        {
            var favoriteProduct = this.context.FavoriteProducts
                .FirstOrDefault(x => x.User.UserName == username && x.ProductId == id);

            if (favoriteProduct == null)
            {
                throw new ArgumentNullException(nameof(favoriteProduct));
            }

            this.context.FavoriteProducts.Remove(favoriteProduct);
            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DecreaseQuantityInStock(int productId, int quantity)
        {
            var productFromDb = this.context.Products.Find(productId);

            if (productFromDb == null)
            {
                throw new ArgumentNullException(nameof(productFromDb));
            }

            productFromDb.QuantityInStock -= quantity;

            if (productFromDb.QuantityInStock == 0)
            {
                this.IsOutOfStock(productFromDb);
            }

            this.context.Products.Update(productFromDb);
            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        private void IsOutOfStock(Product product)
        {
            product.IsOutOfStock = true;
        }
    }
}
