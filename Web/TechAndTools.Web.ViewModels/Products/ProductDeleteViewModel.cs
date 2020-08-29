namespace TechAndTools.Web.ViewModels.Products
{
    using Services.Mapping;
    using Services.Models;

    public class ProductDeleteViewModel : IMapFrom<ProductServiceModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ProductCategoryName { get; set; }
    }
}
