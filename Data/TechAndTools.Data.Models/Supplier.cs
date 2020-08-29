namespace TechAndTools.Data.Models
{
    using System.Collections.Generic;

    public class Supplier
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal PriceToOffice { get; set; }

        public decimal PriceToAddress { get; set; }

        public int DeliveryTimeInDays { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
