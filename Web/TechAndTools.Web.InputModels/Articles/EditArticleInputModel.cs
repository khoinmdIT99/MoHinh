namespace TechAndTools.Web.InputModels.Articles
{
    using Commons;
    using Services.Mapping;
    using Services.Models;
    
    using Microsoft.AspNetCore.Http;
    
    using System.ComponentModel.DataAnnotations;

    public class EditArticleInputModel : IMapTo<ArticleServiceModel>, IMapFrom<ArticleServiceModel>
    {
        private const int TitleMaxLength = 255;
        private const int TitleMinLength = 3;

        private const string DisplayName = "Заглавие";
        private const string DisplayContent = "Съдържание";
        private const string DisplayImageFormFile = "Снимка";
        
        [Display(Name = DisplayName)]
        [Required(ErrorMessage = InputModelsConstants.RequiredMessage)]
        [DataType(DataType.Text)]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; }
        
        [Display(Name = DisplayContent)]
        [Required(ErrorMessage = InputModelsConstants.RequiredMessage)]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
        
        [Display(Name = DisplayImageFormFile)]
        public IFormFile ImageFormFile { get; set; }
    }
}
