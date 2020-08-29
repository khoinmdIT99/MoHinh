namespace TechAndTools.Web.ViewModels.Brands
{
    using Services.Mapping;
    using Services.Models;

    public class BrandListViewModel : IMapFrom<BrandServiceModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
