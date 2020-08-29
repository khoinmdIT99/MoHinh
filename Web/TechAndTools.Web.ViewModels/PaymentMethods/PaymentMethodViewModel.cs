namespace TechAndTools.Web.ViewModels.PaymentMethods
{
    using Services.Mapping;
    using Services.Models;

    public class PaymentMethodViewModel : IMapFrom<PaymentMethodServiceModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
