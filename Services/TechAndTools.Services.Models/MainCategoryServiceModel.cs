namespace TechAndTools.Services.Models
{
    using Data.Models;
    using Mapping;

    using System.Collections.Generic;

    public class MainCategoryServiceModel : IMapFrom<MainCategory>, IMapTo<MainCategory>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<CategoryServiceModel> Categories { get; set; }
    }
}
