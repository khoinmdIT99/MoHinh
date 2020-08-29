namespace TechAndTools.Web.ViewModels.Suppliers
{
    using Services.Mapping;
    using Services.Models;

    public class SupplierAllViewModel : IMapFrom<SupplierServiceModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal PriceToOffice { get; set; }

        public decimal PriceToAddress { get; set; }

        public int DeliveryTimeInDays { get; set; }
    }
}
