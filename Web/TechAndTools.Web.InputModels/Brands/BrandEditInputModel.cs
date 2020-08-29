namespace TechAndTools.Web.InputModels.Brands
{
    using Commons;
    using Services.Mapping;
    using Services.Models;

    using System.ComponentModel.DataAnnotations;

    public class BrandEditInputModel : IMapFrom<BrandServiceModel>, IMapTo<BrandServiceModel>
    {
        private const int NameMaxLength = 25;
        private const int NameMinLength = 3;
        
        private const int LogoUrlMaxLength = 25;
        private const int LogoUrlMinLength = 3;

        private const int OfficialSiteMaxLength = 25;
        private const int OfficialSiteMinLength = 3;

        private const string DisplayName = "Име";
        private const string DisplayLogoUrl = "Лого";
        private const string DisplayOfficialSite = "Официален сайт";

        [Display(Name = DisplayName)]
        [Required(ErrorMessage = InputModelsConstants.RequiredMessage)]
        [StringLength(NameMaxLength, ErrorMessage = InputModelsConstants.StringLengthMessage, MinimumLength = NameMinLength)]
        public string Name { get; set; }
        
        [Display(Name = DisplayLogoUrl)]
        [Required(ErrorMessage = InputModelsConstants.RequiredMessage)]
        [StringLength(LogoUrlMaxLength, ErrorMessage = InputModelsConstants.StringLengthMessage, MinimumLength = LogoUrlMinLength)]
        public string LogoUrl { get; set; }
        
        [Display(Name = DisplayOfficialSite)]
        [Required(ErrorMessage = InputModelsConstants.RequiredMessage)]
        [StringLength(OfficialSiteMaxLength, ErrorMessage = InputModelsConstants.StringLengthMessage, MinimumLength = OfficialSiteMinLength)]
        public string OfficialSite { get; set; }
    }
}
