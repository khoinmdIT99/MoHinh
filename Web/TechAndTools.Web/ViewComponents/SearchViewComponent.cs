namespace TechAndTools.Web.ViewComponents
{
    using Services.Contracts;
    using Services.Mapping;
    using ViewModels.Categories;

    using Microsoft.AspNetCore.Mvc;
    
    using System.Collections.Generic;
    using System.Linq;

    public class SearchViewComponent : ViewComponent
    {
        
        private readonly ICategoryService categoryService;

        public SearchViewComponent(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public IViewComponentResult Invoke()
        {
            IEnumerable<CategoryComponentViewModel> mainCategoriesCategoryComponentViewModels = this.categoryService
                .GetAllCategories()
                .To<CategoryComponentViewModel>()
                .ToList();
            
            return this.View(mainCategoriesCategoryComponentViewModels);
        }
    }
}
