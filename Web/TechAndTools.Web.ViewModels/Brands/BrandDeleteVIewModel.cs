namespace TechAndTools.Web.ViewModels.Brands
{
    using Services.Mapping;
    using Services.Models;

    public class BrandDeleteViewModel : IMapFrom<BrandServiceModel>, IMapTo<BrandServiceModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LogoUrl { get; set; }

        public string OfficialSite { get; set; }
    }
}
