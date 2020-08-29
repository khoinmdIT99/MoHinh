namespace TechAndTools.Services.Models
{
    using Data.Models;
    using Mapping;

    using System.Collections.Generic;

    public class CategoryServiceModel : IMapTo<Category>, IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<ProductServiceModel> Products { get; set; }

        public int MainCategoryId  { get; set; }
        public MainCategoryServiceModel MainCategory { get; set; }
    }
}
