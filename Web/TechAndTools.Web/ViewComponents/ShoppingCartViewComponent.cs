namespace TechAndTools.Web.ViewComponents
{
    using Commons.Constants;
    using Services.Contracts;
    using Services.Mapping;
    using Services.Models;
    using ViewModels.ShoppingCart;
    
    using Microsoft.AspNetCore.Mvc;
    
    using System.Collections.Generic;
    using System.Linq;

    public class ShoppingCartViewComponent : ViewComponent
    {
        private readonly IShoppingCartService shoppingCartService;

        public ShoppingCartViewComponent(IShoppingCartService shoppingCartService)
        {
            this.shoppingCartService = shoppingCartService;
        }

        public IViewComponentResult Invoke()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                List<ShoppingCartProductServiceModel> shoppingCartProductsServiceModels = this.shoppingCartService
                    .GetAllShoppingCartProducts(this.User.Identity.Name)
                    .ToList();

                var shoppingCartProductsViewModels = shoppingCartProductsServiceModels
                    .Select(x => x.To<ShoppingCartProductViewModel>())
                    .ToList();

                return this.View(shoppingCartProductsViewModels);
            }

            var shoppingCartSession = SessionHelper.GetObjectFromJson<List<ShoppingCartProductViewModel>>(HttpContext.Session, GlobalConstants.SessionShoppingCartKey) ??
                                      new List<ShoppingCartProductViewModel>();
            
            return this.View(shoppingCartSession);
        }
    }
}
