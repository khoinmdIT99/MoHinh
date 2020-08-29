namespace TechAndTools.Web.InputModels.Products
{
    using Commons;
    using Services.Mapping;
    using Services.Models;

    using Microsoft.AspNetCore.Http;
    
    using System.ComponentModel.DataAnnotations;

    public class ProductCreateInputModel : IMapTo<ProductServiceModel>, IMapFrom<ProductServiceModel>
    {
        private const int NameMaxLength = 25;
        private const int NameMinLength = 3;

        private const int DescriptionMaxLength = 255;
        private const int DescriptionMinLength = 3;

        private const int WarrantyRangeMax = 120;
        private const int WarrantyRangeMin = 1;

        private const int QuantityInStockRangeMax = 10000;
        private const int QuantityInStockRangeMin = 1;

        private const string PriceMinValue = "0.1";
        private const string PriceMaxValue = "25000";

        private const string DisplayName = "Име";
        private const string DisplayProductCategory = "Категория";
        private const string DisplayBrand = "Марка";
        private const string DisplayDescription = "Описание";
        private const string DisplayDocumentation = "Линк към документация";
        private const string DisplayWarranty = "Месеци гаранция";
        private const string DisplayPrice = "Цена";
        private const string DisplayQuantityInStock = "Бройки";
        private const string DisplayImageFormFile = "Снимка";

        [Display(Name = DisplayName)]
        [Required(ErrorMessage = InputModelsConstants.RequiredMessage)]
        [StringLength(NameMaxLength, ErrorMessage = InputModelsConstants.StringLengthMessage, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        [Display(Name = DisplayProductCategory)]
        [Required(ErrorMessage = InputModelsConstants.RequiredMessage)]
        public int ProductCategoryId { get; set; }

        [Display(Name = DisplayBrand)]
        [Required(ErrorMessage = InputModelsConstants.RequiredMessage)]
        public int BrandId { get; set; }

        [Display(Name = DisplayDescription)]
        [Required(ErrorMessage = InputModelsConstants.RequiredMessage)]
        [StringLength(DescriptionMaxLength, ErrorMessage = InputModelsConstants.StringLengthMessage, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; }

        [Display(Name = DisplayDocumentation)]
        public string DocumentationUrl { get; set; }

        [Display(Name = DisplayWarranty)]
        [Required(ErrorMessage = InputModelsConstants.RequiredMessage)]
        [Range(WarrantyRangeMin, WarrantyRangeMax, ErrorMessage = InputModelsConstants.RangeMessage)]
        public int Warranty { get; set; }

        [Display(Name = DisplayPrice)]
        [Required(ErrorMessage = InputModelsConstants.RequiredMessage)]
        [Range(typeof(decimal), PriceMinValue, PriceMaxValue, ErrorMessage = InputModelsConstants.RangeMessage)]
        public decimal Price { get; set; }

        [Display(Name = DisplayQuantityInStock)]
        [Required(ErrorMessage = InputModelsConstants.RequiredMessage)]
        [Range(QuantityInStockRangeMin, QuantityInStockRangeMax, ErrorMessage = InputModelsConstants.RangeMessage)]
        public int QuantityInStock { get; set; }

        [Display(Name = DisplayImageFormFile)]
        [Required(ErrorMessage = InputModelsConstants.RequiredMessage)]
        public IFormFile ImageFormFile { get; set; }
    }
}
