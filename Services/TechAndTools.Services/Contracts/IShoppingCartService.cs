namespace TechAndTools.Services.Contracts
{
    using Models;

    using System.Linq;
    using System.Threading.Tasks;

    public interface IShoppingCartService
    {
        Task<ShoppingCartProductServiceModel> AddToShoppingCartAsync(int productId, string username, int quantity);

        IQueryable<ShoppingCartProductServiceModel> GetAllShoppingCartProducts(string username);

        Task<bool> RemoveProductFromShoppingCart(int id, string username);

        bool AnyProducts(string username);

        Task<bool> RemoveAllProductFromShoppingCart(string username);
    }
}
