namespace TechAndTools.Services.Models
{
    using Data.Models;
    using Mapping;

    public class OrderStatusServiceModel : IMapFrom<OrderStatus>, IMapTo<OrderStatus>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
