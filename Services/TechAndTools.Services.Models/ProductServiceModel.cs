namespace TechAndTools.Services.Models
{
    using Data.Models;
    using Mapping;

    using System;
    using System.Collections.Generic;

    public class ProductServiceModel : IMapFrom<Product>, IMapTo<Product>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ProductCategoryId { get; set; }
        public CategoryServiceModel ProductCategory { get; set; }

        public int BrandId { get; set; }
        public BrandServiceModel Brand { get; set; }

        public ICollection<ImageServiceModel> Images { get; set; }

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

        public ICollection<ReviewServiceModel> Reviews { get; set; }
    }
}