namespace TechAndTools.Web.ViewModels.Suppliers
{
    using Services.Mapping;
    using Services.Models;

    public class SupplierDeleteViewModel : IMapFrom<SupplierServiceModel>
    {
        public string Name { get; set; }

        public decimal PriceToOffice { get; set; }

        public decimal PriceToAddress { get; set; }

        public int MinimumDeliveryTimeDays { get; set; }

        public int MaximumDeliveryTimeDays { get; set; }
    }
}
