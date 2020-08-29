namespace TechAndTools.Web.Areas.Administration.Controllers
{
    using InputModels.Categories;
    using Services.Contracts;
    using Services.Mapping;
    using Services.Models;
    using ViewModels.Categories;
    using ViewModels.MainCategories;

    using Microsoft.AspNetCore.Mvc;

    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CategoriesController : AdministrationController
    {
        private readonly ICategoryService categoryService;
        private readonly IMainCategoryService mainCategoryService;

        public CategoriesController(ICategoryService categoryService, IMainCategoryService mainCategoryService)
        {
            this.categoryService = categoryService;
            this.mainCategoryService = mainCategoryService;
        }

        public IActionResult Create()
        {
            this.ViewData["mainCategories"] = this.mainCategoryService
                .GetAllMainCategories()
                .To<CreateCategoryMainCategoryViewModel>();

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateInputModel createInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                this.ViewData["mainCategories"] = this.mainCategoryService
                    .GetAllMainCategories()
                    .To<CreateCategoryMainCategoryViewModel>();

                return this.View();
            }

            await this.categoryService.CreateCategoryAsync(createInputModel.To<CategoryServiceModel>());

            return this.Redirect("All");
        }

        public IActionResult Edit(int id)
        {
            CategoryEditInputModel editInputModel = this.categoryService
                .GetCategoryById(id)
                .To<CategoryEditInputModel>();

            if(editInputModel == null)
            {
                // TODO: Error Handling
                return this.Redirect("/");
            }

            this.ViewData["mainCategories"] = this.mainCategoryService
                .GetAllMainCategories()
                .To<CreateCategoryMainCategoryViewModel>();

            return this.View(editInputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryEditInputModel editInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                this.ViewData["mainCategories"] = this.mainCategoryService
                    .GetAllMainCategories()
                    .To<CreateCategoryMainCategoryViewModel>();

                return this.View(editInputModel);
            }

            await this.categoryService.EditCategoryAsync(editInputModel.To<CategoryServiceModel>());

            return this.RedirectToAction("All", "Categories");
        }
        
        public IActionResult All()
        {
            List<CategoryViewModel> categoriesViewModels = this.categoryService
                .GetAllCategories()
                .To<CategoryViewModel>()
                .ToList();

            return this.View(categoriesViewModels);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.categoryService.DeleteAsync(id);

            return this.RedirectToAction("All", "Categories");
        }
    }
}