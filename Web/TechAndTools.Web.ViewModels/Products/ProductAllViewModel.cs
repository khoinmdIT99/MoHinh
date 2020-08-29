namespace TechAndTools.Web.ViewModels.Products
{
    using Services.Mapping;
    using Services.Models;

    using System.Collections.Generic;

    public class ProductAllViewModel : IMapFrom<ProductServiceModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int QuantityInStock { get; set; }

        public int SalesCount { get; set; }

        public string ProductCategoryName { get; set; }

        public ICollection<ImageServiceModel> Images { get; set; }
    }
}
