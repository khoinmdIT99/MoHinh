namespace TechAndTools.Services.Models
{
    using Data.Models;
    using Mapping;

    using System.Collections.Generic;

    public class BrandServiceModel : IMapTo<Brand>, IMapFrom<Brand>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LogoUrl { get; set; }

        public string OfficialSite { get; set; }

        public ICollection<ProductServiceModel> Products { get; set; }
    }
}
