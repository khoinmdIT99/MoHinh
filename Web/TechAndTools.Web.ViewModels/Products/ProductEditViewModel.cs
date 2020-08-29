namespace TechAndTools.Web.ViewModels.Products
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ProductEditViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ProductCategoryId { get; set; }

        public int BrandId { get; set; }

        public string Description { get; set; }

        public string ManualUrl { get; set; }

        public int Warranty { get; set; }

        public decimal Price { get; set; }

        [Range(1, Int32.MaxValue)]
        public int QuantityInStock { get; set; }
    }
}
