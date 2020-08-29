namespace TechAndTools.Services.Models
{
    using Data.Models;
    using Mapping;

    using System;
    using System.Collections.Generic;

    public class OrderServiceModel : IMapFrom<Order>, IMapTo<Order>
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }
        
        public DateTime ExpectedDeliveryDate { get; set; }

        public DateTime? ShippingDate { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal DeliveryPrice { get; set; }

        public string Recipient { get; set; }

        public string RecipientPhoneNumber { get; set; }

        public int PaymentMethodId { get; set; }
        public PaymentMethodServiceModel PaymentMethod { get; set; }

        public int OrderStatusId { get; set; }
        public OrderStatusServiceModel OrderStatus { get; set; }

        public int PaymentStatusId { get; set; }
        public PaymentStatusServiceModel PaymentStatus { get; set; }

        public int AddressId { get; set; }
        public AddressServiceModel Address { get; set; }

        public int SupplierId { get; set; }
        public SupplierServiceModel Supplier { get; set; }

        public string UserId { get; set; }
        public TechAndToolsUser User { get; set; }

        public ICollection<OrderProductServiceModel> OrderProducts { get; set; }
    }
}
