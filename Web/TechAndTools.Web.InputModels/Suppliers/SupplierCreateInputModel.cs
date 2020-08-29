namespace TechAndTools.Web.InputModels.Suppliers
{
    using Commons;
    using Services.Mapping;
    using Services.Models;

    using System.ComponentModel.DataAnnotations;

    public class SupplierCreateInputModel : IMapTo<SupplierServiceModel>
    {
        private const int NameMinLength = 3;
        private const int NameMaxLength = 25;

        private const int DeliveryTimeInDaysMin = 1;
        private const int DeliveryTimeInDaysMax = 20;

        private const string PriceToOfficeMinLength = "0.01";
        private const string PriceToOfficeMaxLength = "10000";

        private const string PriceToAddressMinLength = "0.01";
        private const string PriceToAddressMaxLength = "10000";

        private const string DisplayName = "Име";
        private const string DisplayPriceToOffice = "Цена до офис";
        private const string DisplayPriceToAddress = "Цена до адрес";
        private const string DisplayDeliveryTimeInDays = "Време за доставка (Дни)";

        [Required(ErrorMessage = InputModelsConstants.RequiredMessage)]
        [Display(Name = DisplayName)]
        [StringLength(NameMaxLength, ErrorMessage = InputModelsConstants.StringLengthMessage, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        
        [Required(ErrorMessage = InputModelsConstants.RequiredMessage)]
        [Display(Name = DisplayPriceToOffice)]
        [Range(typeof(decimal), PriceToOfficeMinLength, PriceToOfficeMaxLength, ErrorMessage = InputModelsConstants.RangeMessage)]
        public decimal PriceToOffice { get; set; }
        
        [Required(ErrorMessage = InputModelsConstants.RequiredMessage)]
        [Display(Name = DisplayPriceToAddress)]
        [Range(typeof(decimal), PriceToAddressMinLength, PriceToAddressMaxLength, ErrorMessage = InputModelsConstants.RangeMessage)]
        public decimal PriceToAddress { get; set; }

        
        [Required(ErrorMessage = InputModelsConstants.RequiredMessage)]
        [Display(Name = DisplayDeliveryTimeInDays)]
        [Range(DeliveryTimeInDaysMin, DeliveryTimeInDaysMax, ErrorMessage = InputModelsConstants.RangeMessage)]
        public int DeliveryTimeInDays { get; set; }
    }
}
