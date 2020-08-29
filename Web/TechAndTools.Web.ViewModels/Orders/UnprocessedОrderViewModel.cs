namespace TechAndTools.Web.ViewModels.Orders
{
    using Services.Mapping;
    using Services.Models;

    using System;

    public class UnprocessedОrderViewModel : IMapFrom<OrderServiceModel>
    {
        public int Id { get; set; }

        public string PaymentStatusName { get; set; }

        public DateTime OrderDate { get; set; }

        public string PaymentMethodName { get; set; }
    }
}
