namespace TechAndTools.Web.ViewModels.Orders
{
    using Services.Mapping;
    using Services.Models;
    
    using System;

    public class MyOrdersViewModel : IMapFrom<OrderServiceModel>
    {
        public int Id { get; set; }

        public string OrderStatusName { get; set; }

        public string PaymentMethodName { get; set; }

        public string PaymentStatusName { get; set; }

        public DateTime ExpectedDeliveryDate { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
