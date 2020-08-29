namespace TechAndTools.Web.ViewModels.MainCategories
{
    using Categories;
    using Services.Mapping;
    using Services.Models;

    using System.Collections.Generic;

    public class MainCategoryComponentViewModel : IMapFrom<MainCategoryServiceModel>
    {
        public string Name { get; set; }

        public ICollection<CategoryComponentViewModel> Categories { get; set; }
    }
}
