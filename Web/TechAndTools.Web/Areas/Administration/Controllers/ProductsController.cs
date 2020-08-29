namespace TechAndTools.Web.Areas.Administration.Controllers
{
    using InputModels.Products;
    using Services.Contracts;
    using Services.Mapping;
    using Services.Models;
    using Services.Upload;
    using ViewModels.Brands;
    using ViewModels.Categories;
    using ViewModels.Products;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    
    using System.Collections.Generic;
    using System.Threading.Tasks;
    
    public class ProductsController : AdministrationController
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly IBrandService brandService;
        private readonly ICloudinaryService cloudinaryService;
        private readonly IImageService imageService;

        public ProductsController(IProductService productService,
                                  ICategoryService categoryService,
                                  IBrandService brandService,
                                  ICloudinaryService cloudinaryService,
                                  IImageService imageService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.brandService = brandService;
            this.cloudinaryService = cloudinaryService;
            this.imageService = imageService;
        }

        public IActionResult Create()
        {
            this.ViewData["categories"] = this.categoryService
                .GetAllCategories()
                .To<CategoryListViewModel>();

            this.ViewData["brands"] = this.brandService
                .GetAllBrands()
                .To<BrandListViewModel>();

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateInputModel productCreateInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(productCreateInputModel);
            }

            ProductServiceModel productServiceModel = productCreateInputModel.To<ProductServiceModel>();

            string pictureUrl = await this.cloudinaryService
                .UploadPictureAsync(productCreateInputModel.ImageFormFile, productCreateInputModel.Name);

            ProductServiceModel productFromDb = await this.productService.CreateAsync(productServiceModel);

            await this.imageService.CreateWithProductAsync(pictureUrl, productFromDb.Id);


            return this.Redirect("All");
        }

        public IActionResult Edit(int id)
        {
            this.ViewData["categories"] = this.categoryService
                .GetAllCategories()
                .To<CategoryListViewModel>();

            this.ViewData["brands"] = this.brandService
                .GetAllBrands()
                .To<BrandListViewModel>();

            var productEditInput = this.productService
                .GetProductById(id)
                .To<ProductEditInputModel>();

            return this.View(productEditInput);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductEditInputModel productEditInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            ProductServiceModel productServiceModel = productEditInputModel.To<ProductServiceModel>();

            if (productEditInputModel.ImageFormFile != null)
            {
                string pictureUrl = await this.cloudinaryService
                    .UploadPictureAsync(productEditInputModel.ImageFormFile, productEditInputModel.Name);

                ProductServiceModel productFromDb = await this.productService.EditAsync(productServiceModel);

                await this.imageService.CreateWithProductAsync(pictureUrl, productFromDb.Id);

                return this.RedirectToAction("All", "Products");
            }

            await this.productService.EditAsync(productServiceModel);

            return this.RedirectToAction("All", "Products");

        }

        public async Task<IActionResult> All()
        {
            List<ProductAllViewModel> productViewModels = await this.productService
                .GetAllProducts()
                .To<ProductAllViewModel>()
                .ToListAsync();

            return this.View(productViewModels);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.productService.DeleteAsync(id);
            return this.RedirectToAction("All", "Products");
        }
    }
}
