namespace TechAndTools.Web.ViewComponents
{
    using Services.Contracts;
    using Services.Mapping;
    using ViewModels.Brands;

    using Microsoft.AspNetCore.Mvc;

    using System.Linq;

    public class BrandsFilterViewComponent : ViewComponent
    {
        private readonly IBrandService brandService;

        public BrandsFilterViewComponent(IBrandService brandService)
        {
            this.brandService = brandService;
        }

        public IViewComponentResult Invoke()
        {
            var brandsViewModels = this.brandService.GetAllBrands().To<BrandListViewModel>().ToList();

            return this.View(brandsViewModels);
        }
    }
}
