namespace TechAndTools.Web.Areas.Administration.Controllers
{
    using InputModels.Suppliers;
    using Services.Contracts;
    using Services.Mapping;
    using Services.Models;
    using ViewModels.Suppliers;
    
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    
    using System.Threading.Tasks;
    
    public class SuppliersController : AdministrationController
    {
        private readonly ISupplierService supplierService;

        public SuppliersController(ISupplierService supplierService)
        {
            this.supplierService = supplierService;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SupplierCreateInputModel createInputModel)
        {
            await this.supplierService.CreateAsync(createInputModel.To<SupplierServiceModel>());

            return this.RedirectToAction("All", "Suppliers");
        }

        public IActionResult Edit(int id)
        {
            var supplierEditInputModel = this.supplierService
                .GetSupplierById(id)
                .To<SupplierEditInputModel>();

            return this.View(supplierEditInputModel);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(SupplierEditInputModel supplierEditInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(supplierEditInputModel);
            }

            await this.supplierService.EditAsync(supplierEditInputModel.To<SupplierServiceModel>());

            return this.RedirectToAction("All", "Suppliers");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.supplierService.DeleteAsync(id);

            return this.RedirectToAction("All", "Suppliers");
        }

        public async Task<IActionResult> All()
        {
            var suppliersViewModels = await this.supplierService
                .GetAllSuppliers()
                .To<SupplierAllViewModel>()
                .ToListAsync();

            return this.View(suppliersViewModels);
        }

    }
}