namespace TechAndTools.Web.Controllers
{
    using Commons.Constants;
    using InputModels.Addresses;
    using Services.Contracts;
    using Services.Mapping;
    using Services.Models;
    using ViewModels.Addresses;
    
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    
    using System.Security.Claims;
    using System.Threading.Tasks;

    [Authorize(Roles = GlobalConstants.UserRole +", " + GlobalConstants.AdminRole)]
    public class AddressesController : BaseController
    {
        private readonly IAddressService addressService;

        public AddressesController(IAddressService addressService)
        {
            this.addressService = addressService;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddressCreateInputModel addressCreateInputModel)
        {
            await this.addressService.CreateAsync(addressCreateInputModel.To<AddressServiceModel>(), this.User.Identity.Name);

            return this.RedirectToAction("Create", "Orders");
        }

        public async Task<IActionResult> MyAddresses()
        {
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var viewModels = await this.addressService.GetAllByUserId(userId).To<AddressViewModel>().ToListAsync();

            return this.View(viewModels);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.addressService.DeleteByIdAsync(id);

            return this.RedirectToAction("MyAddresses", "Addresses");
        }
    }
}