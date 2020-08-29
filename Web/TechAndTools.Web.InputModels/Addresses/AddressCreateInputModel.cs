namespace TechAndTools.Web.InputModels.Addresses
{
    using Commons;
    using Services.Mapping;
    using Services.Models;

    using System.ComponentModel.DataAnnotations;

    public class AddressCreateInputModel : IMapTo<AddressServiceModel>
    {
        private const string DisplayCity = "Град";
        private const string DisplayCityAddress = "Адрес";
        private const string DisplayPostCode = "Пощенски код";

        [Display(Name = DisplayCity)]
        [Required(ErrorMessage = InputModelsConstants.RequiredMessage)]
        public string City { get; set; }

        
        [Display(Name = DisplayCityAddress)]
        [Required(ErrorMessage = InputModelsConstants.RequiredMessage)]
        public string CityAddress { get; set; }

        
        [Display(Name = DisplayPostCode)]
        [Required(ErrorMessage = InputModelsConstants.RequiredMessage)]
        public int PostCode { get; set; }
    }
}
