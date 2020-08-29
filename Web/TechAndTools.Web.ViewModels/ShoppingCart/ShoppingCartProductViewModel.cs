namespace TechAndTools.Web.ViewModels.ShoppingCart
{
    using Services.Mapping;
    using Services.Models;

    using AutoMapper;

    using System.Linq;

    public class ShoppingCartProductViewModel : IMapFrom<ShoppingCartProductServiceModel>, IMapFrom<ProductServiceModel>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ShoppingCartProductServiceModel, ShoppingCartProductViewModel>()
                .ForMember(dest => dest.ImageUrl,
                    opts => opts.MapFrom(origin => origin.Product.Images.FirstOrDefault().ImageUrl))
                .ForMember(dest => dest.Id, opts => opts.MapFrom(origin => origin.ProductId))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(origin => origin.Product.Name))
                .ForMember(dest => dest.Price, opts => opts.MapFrom(origin => origin.Product.Price))
                .ForMember(dest => dest.TotalPrice,
                    opts => opts.MapFrom(origin => origin.Product.Price * origin.Quantity));
        }
    }
}
