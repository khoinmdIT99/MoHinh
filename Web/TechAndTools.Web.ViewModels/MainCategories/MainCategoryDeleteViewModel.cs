namespace TechAndTools.Web.ViewModels.MainCategories
{
    using Services.Mapping;
    using Services.Models;

    public class MainCategoryDeleteViewModel : IMapFrom<MainCategoryServiceModel>, IMapTo<MainCategoryServiceModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
