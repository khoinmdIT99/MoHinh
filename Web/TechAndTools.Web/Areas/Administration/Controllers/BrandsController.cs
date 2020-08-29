namespace TechAndTools.Web.Areas.Administration.Controllers
{
    using InputModels.Brands;
    using Services.Contracts;
    using Services.Mapping;
    using Services.Models;
    using ViewModels.Brands;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    
    using System.Threading.Tasks;

    public class BrandsController : AdministrationController
    {
        private readonly IBrandService brandService;

        public BrandsController(IBrandService brandService)
        {
            this.brandService = brandService;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BrandCreateInputModel brandCreateInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(brandCreateInputModel);
            }

            await this.brandService.CreateAsync(brandCreateInputModel.To<BrandServiceModel>());

            return this.RedirectToAction("All", "Brands");
        }

        public async Task<IActionResult> Edit(int id)
        {
            BrandServiceModel brandServiceModel = await this.brandService.GetBrandByIdAsync(id);

            BrandEditInputModel brandEditInputModel = brandServiceModel.To<BrandEditInputModel>();

            return this.View(brandEditInputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BrandEditInputModel brandEditInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(brandEditInputModel);
            }

            await this.brandService.EditAsync(brandEditInputModel.To<BrandServiceModel>());

            return this.RedirectToAction("All", "Brands");
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            await this.brandService.DeleteAsync(id);

            return this.RedirectToAction("All", "Brands");
        }

        public async Task<IActionResult> All()
        {
            var viewModels = await this.brandService.GetAllBrands()
                .To<BrandIndexViewModel>()
                .ToListAsync();

            return this.View(viewModels);
        }
    }
}