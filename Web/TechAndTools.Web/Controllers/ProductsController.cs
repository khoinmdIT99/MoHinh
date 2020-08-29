namespace TechAndTools.Web.Controllers
{
    using Services.Contracts;
    using Services.Mapping;
    using ViewModels.Products;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ProductsController : BaseController
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            var serviceModel = this.productService.GetProductById(id);
            
            ProductDetailsViewModel viewModel = this.productService.GetProductById(id).To<ProductDetailsViewModel>();
            
            return View(viewModel);
        }
    }
}