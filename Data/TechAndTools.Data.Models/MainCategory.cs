namespace TechAndTools.Data.Models
{
    using System.Collections.Generic;

    public class MainCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
    }
}
