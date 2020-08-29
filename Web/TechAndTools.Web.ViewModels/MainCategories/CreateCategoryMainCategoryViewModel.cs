namespace TechAndTools.Web.ViewModels.MainCategories
{
    using Services.Mapping;
    using Services.Models;

    public class CreateCategoryMainCategoryViewModel : IMapFrom<MainCategoryServiceModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
