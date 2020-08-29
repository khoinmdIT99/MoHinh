namespace TechAndTools.Services.Models
{
    using Data.Models;
    using Mapping;

    public class FavoriteProductsServiceModel : IMapFrom<FavoriteProduct>, IMapTo<FavoriteProduct>
    {
        public string Id { get; set; }

        public string UserId { get; set; }
        public TechAndToolsUser User { get; set; }

        public int ProductId { get; set; }
        public ProductServiceModel Product { get; set; }
    }
}
