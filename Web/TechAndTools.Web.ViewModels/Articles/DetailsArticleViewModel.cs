namespace TechAndTools.Web.ViewModels.Articles
{
    using Services.Mapping;
    using Services.Models;

    using AutoMapper;

    public class DetailsArticleViewModel : IMapFrom<ArticleServiceModel>, IHaveCustomMappings
    {
        
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string ImageUrl { get; set; }

        public string CreatedOn { get; set; }

        public string ModifiedOn { get; set; }

        public string AuthorUsername { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ArticleServiceModel, DetailsArticleViewModel>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Image.ImageUrl));
        }
    }
}
