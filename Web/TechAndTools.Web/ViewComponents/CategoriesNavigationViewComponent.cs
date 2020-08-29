namespace TechAndTools.Web.ViewComponents
{
    using Services.Contracts;
    using Services.Mapping;
    using ViewModels.MainCategories;
    
    using Microsoft.AspNetCore.Mvc;
    
    using System.Collections.Generic;
    using System.Linq;

    public class CategoriesNavigationViewComponent : ViewComponent
    {
        private readonly IMainCategoryService mainCategoryService;

        public CategoriesNavigationViewComponent(IMainCategoryService mainCategoryService)
        {
            this.mainCategoryService = mainCategoryService;
        }

        public IViewComponentResult Invoke()
        {
            IEnumerable<MainCategoryComponentViewModel> mainCategoriesCategoryComponentViewModels = this.mainCategoryService
                .GetAllMainCategories()
                .To<MainCategoryComponentViewModel>()
                .ToList();
            
            return this.View(mainCategoriesCategoryComponentViewModels);
        }
    }
}
