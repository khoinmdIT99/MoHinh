namespace TechAndTools.Web.ViewModels.Orders
{
    using Services.Mapping;
    using Services.Models;

    using System;

    public class ProcessedОrderViewModel : IMapFrom<OrderServiceModel>
    {
        public int Id { get; set; }

        public string PaymentStatusName { get; set; }

        public DateTime ShippingDate { get; set; }

        public string PaymentMethodName { get; set; }
    }
}
