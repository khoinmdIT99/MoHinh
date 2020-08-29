namespace TechAndTools.Web.ViewModels.Categories
{
    using Services.Mapping;
    using Services.Models;

    public class CategoryComponentViewModel : IMapFrom<CategoryServiceModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
