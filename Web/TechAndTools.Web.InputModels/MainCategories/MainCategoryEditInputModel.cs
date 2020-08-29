namespace TechAndTools.Web.InputModels.MainCategories
{
    using Commons;
    using Services.Mapping;
    using Services.Models;

    using System.ComponentModel.DataAnnotations;

    public class MainCategoryEditInputModel : IMapFrom<MainCategoryServiceModel>, IMapTo<MainCategoryServiceModel>
    {
        private const int NameMaxLength = 255;
        private const int NameMinLength = 3;

        private const string DisplayName = "Име";

        [Display(Name = DisplayName)]
        [Required(ErrorMessage = InputModelsConstants.RequiredMessage)]
        [StringLength(NameMaxLength, ErrorMessage = InputModelsConstants.StringLengthMessage, MinimumLength = NameMinLength)]
        public string Name { get; set; }
    }
}
