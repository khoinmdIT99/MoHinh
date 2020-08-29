namespace TechAndTools.Web.Areas.Administration.Controllers
{
    using InputModels.MainCategories;
    using Services.Contracts;
    using Services.Mapping;
    using Services.Models;
    using ViewModels.MainCategories;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class MainCategoriesController : AdministrationController
    {
        private readonly IMainCategoryService mainCategoryService;

        public MainCategoriesController(IMainCategoryService mainCategoryService)
        {
            this.mainCategoryService = mainCategoryService;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MainCategoryInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.mainCategoryService.CreateAsync(model.To<MainCategoryServiceModel>());

            return this.Redirect("All");
        }

        public IActionResult Edit(int id)
        {
            MainCategoryEditInputModel mainCategoryEditInputModel = this.mainCategoryService
                .GetMainCategoryById(id)
                .To<MainCategoryEditInputModel>();

            return this.View(mainCategoryEditInputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MainCategoryEditInputModel mainCategoryEditInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(mainCategoryEditInputModel);
            }

            await this.mainCategoryService.EditAsync(mainCategoryEditInputModel.To<MainCategoryServiceModel>());

            return this.RedirectToAction("All", "MainCategories");
        }

        public async Task<IActionResult> All()
        {
            List<MainCategoryViewModel> mainCategoriesViewModels = await this.mainCategoryService
                .GetAllMainCategories()
                .To<MainCategoryViewModel>()
                .ToListAsync();

            return this.View(mainCategoriesViewModels);
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            await this.mainCategoryService.DeleteAsync(id);

            return this.RedirectToAction("All", "MainCategories");
        }
    }
}