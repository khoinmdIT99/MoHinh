namespace TechAndTools.Web.ViewModels.Categories
{
    using Services.Mapping;
    using Services.Models;
    
    using AutoMapper;

    public class CategoryDeleteViewModel : IMapFrom<CategoryServiceModel>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string MainCategoryName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<CategoryServiceModel, CategoryDeleteViewModel>()
                .ForMember(destination => destination.MainCategoryName,
                    opts => opts.MapFrom(origin => origin.MainCategory.Name));
        }
    }
}
