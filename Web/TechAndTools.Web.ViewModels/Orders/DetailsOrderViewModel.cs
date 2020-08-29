namespace TechAndTools.Web.ViewModels.Orders
{
    using Services.Mapping;
    using Services.Models;

    using System;
    using System.Collections.Generic;

    public class DetailsOrderViewModel : IMapFrom<OrderServiceModel>
    {
        public int Id { get; set; }

        public string PaymentStatusName { get; set; }

        public string OrderStatusName { get; set; }

        public string PaymentMethodName { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime ExpectedDeliveryDate { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public decimal TotalPrice { get; set; }

        public string Recipient { get; set; }

        public string RecipientPhoneNumber { get; set; }
        
        public string AddressCity { get; set; }

        public string AddressAddressStreet { get; set; }

        public string AddressPostCode { get; set; }

        public IList<OrderProductViewModel> OrderProducts { get; set; }

        public decimal DeliveryPrice { get; set; }
    }
}
