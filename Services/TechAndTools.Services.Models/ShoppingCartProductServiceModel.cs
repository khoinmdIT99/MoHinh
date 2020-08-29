namespace TechAndTools.Services.Models
{
    using Data.Models;
    using Mapping;

    public class ShoppingCartProductServiceModel : IMapFrom<ShoppingCartProduct>, IMapTo<ShoppingCartProduct>
    {
        public int ShoppingCartId { get; set; }
        public virtual ShoppingCartServiceModel ShoppingCart { get; set; }

        public int ProductId { get; set; }
        public ProductServiceModel Product { get; set; }

        public int Quantity { get; set; }
    }
}
