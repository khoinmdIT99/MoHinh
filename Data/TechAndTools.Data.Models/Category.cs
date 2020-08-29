namespace TechAndTools.Data.Models
{
    using System.Collections.Generic;

    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public int MainCategoryId  { get; set; }
        public virtual MainCategory MainCategory { get; set; }
    }
}
