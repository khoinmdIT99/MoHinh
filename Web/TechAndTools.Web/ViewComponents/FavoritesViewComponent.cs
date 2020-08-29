namespace TechAndTools.Web.ViewComponents
{
    using Services.Contracts;
    
    using Microsoft.AspNetCore.Mvc;

    using System.Linq;

    public class FavoritesViewComponent : ViewComponent
    {
        
        private readonly IProductService productService;

        public FavoritesViewComponent(IProductService productService)
        {
            this.productService = productService;
        }

        public IViewComponentResult Invoke()
        {
            var favoriteProductCount = this.productService.AllFavoriteProducts(this.User.Identity.Name).Count();

            return this.View(favoriteProductCount);
        }
    }
}
