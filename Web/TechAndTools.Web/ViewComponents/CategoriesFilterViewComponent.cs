namespace TechAndTools.Web.ViewComponents
{
    using Services.Contracts;
    using Services.Mapping;
    using ViewModels.Categories;

    using Microsoft.AspNetCore.Mvc;
    
    using System.Linq;

    public class CategoriesFilterViewComponent : ViewComponent
    {
        private readonly ICategoryService categoryService;

        public CategoriesFilterViewComponent(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public IViewComponentResult Invoke()
        {
            var categoryViewModels = this.categoryService.GetAllCategories()
                .To<CategoryListViewModel>()
                .ToList();

            return this.View(categoryViewModels);
        }
    }
}
