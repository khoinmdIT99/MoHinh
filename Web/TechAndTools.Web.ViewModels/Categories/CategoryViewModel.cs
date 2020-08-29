namespace TechAndTools.Web.ViewModels.Categories
{
    using Services.Mapping;
    using Services.Models;

    public class CategoryViewModel : IMapFrom<CategoryServiceModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string MainCategoryName { get; set; }
    }
}
