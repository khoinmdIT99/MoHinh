namespace TechAndTools.Services.Models
{
    using Data.Models;
    using Mapping;

    public class OrderProductServiceModel : IMapFrom<OrderProduct>, IMapTo<OrderProduct>
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public OrderServiceModel Order { get; set; }

        public int ProductId { get; set; }
        public ProductServiceModel Product { get; set; }

        public int Quantity { get; set; }
    }
}
