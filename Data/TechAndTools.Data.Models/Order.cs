namespace TechAndTools.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Order
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
        public virtual PaymentMethod PaymentMethod { get; set; }

        public int OrderStatusId { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }

        public int PaymentStatusId { get; set; }
        public virtual PaymentStatus PaymentStatus { get; set; }

        public int AddressId { get; set; }
        public virtual Address Address { get; set; }

        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }

        public string UserId { get; set; }
        public virtual TechAndToolsUser User { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
