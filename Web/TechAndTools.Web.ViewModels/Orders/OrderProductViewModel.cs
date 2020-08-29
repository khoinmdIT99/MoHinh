namespace TechAndTools.Web.ViewModels.Orders
{
    using Services.Mapping;
    using Services.Models;
    
    using AutoMapper;

    using System.Linq;

    public class OrderProductViewModel : IMapFrom<OrderProductServiceModel>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<OrderProductServiceModel, OrderProductViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.ImageUrl,
                    opt => opt.MapFrom(src => src.Product.Images.FirstOrDefault().ImageUrl))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Quantity * src.Product.Price));
        }
    }
}
