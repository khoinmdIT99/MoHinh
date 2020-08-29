namespace TechAndTools.Services.Models
{
    using Data.Models;
    using Mapping;

    using System.Collections.Generic;

    public class SupplierServiceModel : IMapFrom<Supplier>, IMapTo<Supplier>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal PriceToOffice { get; set; }

        public decimal PriceToAddress { get; set; }

        public int DeliveryTimeInDays { get; set; }

        public ICollection<OrderServiceModel> Orders { get; set; }
    }
}
