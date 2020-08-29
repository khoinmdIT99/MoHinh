namespace TechAndTools.Data.Models
{
    using System.Collections.Generic;

    public class Brand
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LogoUrl { get; set; }

        public string OfficialSite { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
