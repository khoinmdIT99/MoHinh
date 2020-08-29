namespace TechAndTools.Web.Controllers
{
    using Services.Mapping;
    using Services.Contracts;
    using ViewModels;
    using ViewModels.Home;
    using ViewModels.Products;

    using X.PagedList;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    [AllowAnonymous]
    public class HomeController : BaseController
    {

        private const int DefaultPageNumber = 1;
        private const int DefaultPageSize = 15;

        private readonly IProductService productService;

        public HomeController(IProductService productService,
            ICategoryService categoryService)
        {
            this.productService = productService;
        }


        public IActionResult Index(HomeIndexViewModel model)
        {
            IList<ProductIndexViewModel> productIndexViewModels = new List<ProductIndexViewModel>();

            if (model.CategoryId != null)
            {
                productIndexViewModels = this.productService
                    .GetProductsByCategoryId((int)model.CategoryId)
                    .To<ProductIndexViewModel>()
                    .ToList();
            }
            else
            {
                productIndexViewModels = this.productService
                    .GetAllProducts()
                    .To<ProductIndexViewModel>()
                    .ToList();
            }

            int pageNumber = model.PageNumber ?? DefaultPageNumber;
            int pageSize = model.PageSize ?? DefaultPageSize;

            var viewModel = new HomeIndexViewModel
            {
                ProductsViewModels = productIndexViewModels.ToPagedList(pageNumber, pageSize)
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
