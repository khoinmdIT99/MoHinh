﻿namespace TechAndTools.Web.InputModels.Categories
{
    using Commons;
    using Services.Mapping;
    using Services.Models;

    using System.ComponentModel.DataAnnotations;

    public class CategoryEditInputModel : IMapFrom<CategoryServiceModel>, IMapTo<CategoryServiceModel>
    {
        
        private const int NameMaxLength = 25;
        private const int NameMinLength = 3;

        private const string DisplayName = "Име";
        private const string DisplayMainCategory = "Главна категория";
        
        public int Id { get; set; }

        [Display(Name = DisplayName)]
        [Required(ErrorMessage = InputModelsConstants.RequiredMessage)]
        [StringLength(NameMaxLength, ErrorMessage = InputModelsConstants.StringLengthMessage, MinimumLength = NameMinLength)]
        public string Name { get; set; }
        
        [Display(Name = DisplayMainCategory)]
        [Required(ErrorMessage = InputModelsConstants.RequiredMessage)]
        public int MainCategoryId { get; set; }
    }
}
