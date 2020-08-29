namespace TechAndTools.Services.Models
{
    using Data.Models;
    using Mapping;

    public class PaymentMethodServiceModel : IMapFrom<PaymentMethod>, IMapTo<PaymentMethod>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
