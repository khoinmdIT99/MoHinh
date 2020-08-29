namespace TechAndTools.Services.Models
{
    using Data.Models;
    using Mapping;

    public class PaymentStatusServiceModel : IMapFrom<PaymentStatus>, IMapTo<PaymentStatus>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
