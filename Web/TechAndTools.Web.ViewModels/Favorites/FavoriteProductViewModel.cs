namespace TechAndTools.Web.ViewModels.Favorites
{
    using Services.Mapping;
    using Services.Models;
    
    using AutoMapper;
    
    using System.Linq;

    public class FavoriteProductViewModel : IMapFrom<FavoriteProductsServiceModel>, IHaveCustomMappings
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal ProductPrice { get; set; }

        public bool ProductIsOutOfStock { get; set; }

        public string ProductImageUrl { get; set; }
        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<FavoriteProductsServiceModel, FavoriteProductViewModel>()
                .ForMember(dest => dest.ProductImageUrl,
                    opt => opt.MapFrom(src => src.Product.Images.FirstOrDefault().ImageUrl));
        }
    }
}
