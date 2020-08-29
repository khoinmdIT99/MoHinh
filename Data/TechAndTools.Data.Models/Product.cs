namespace TechAndTools.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ProductCategoryId { get; set; }
        public virtual Category ProductCategory { get; set; }

        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public string Description { get; set; }

        public string ManualUrl { get; set; }

        public int Warranty { get; set; }

        public decimal Price { get; set; }

        public int QuantityInStock { get; set; }

        public int Rating { get; set; }

        public int SalesCount { get; set; }

        public bool IsOutOfStock { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
