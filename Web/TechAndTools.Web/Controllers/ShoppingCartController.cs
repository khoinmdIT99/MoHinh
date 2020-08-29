namespace TechAndTools.Web.Controllers
{
    using Services.Contracts;
    using Services.Mapping;
    using Services.Models;
    using ViewModels.ShoppingCart;

    using X.PagedList;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [AllowAnonymous]
    public class ShoppingCartController : BaseController
    {
        private readonly IShoppingCartService shoppingCartService;


        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            this.shoppingCartService = shoppingCartService;
        }

        public async Task<IActionResult> MyCart()
        {
            List<ShoppingCartProductServiceModel> shoppingCartProductsServiceModels = await this.shoppingCartService
                .GetAllShoppingCartProducts(this.User.Identity.Name)
                .ToListAsync();

            List<ShoppingCartProductViewModel> shoppingCartProductsViewModels = await shoppingCartProductsServiceModels
                .Select(x => x.To<ShoppingCartProductViewModel>())
                .ToListAsync();

            return this.View(shoppingCartProductsViewModels);

        }

        public async Task<IActionResult> Add(int id, int quantity)
        {
            if (quantity <= 0)
            {
                return this.RedirectToAction(nameof(MyCart));
            }

            await this.shoppingCartService.AddToShoppingCartAsync(id, this.User.Identity.Name, quantity);

            return this.RedirectToAction(nameof(MyCart));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.shoppingCartService.RemoveProductFromShoppingCart(id, this.User.Identity.Name);

            return this.RedirectToAction(nameof(MyCart));

        }
    }
}