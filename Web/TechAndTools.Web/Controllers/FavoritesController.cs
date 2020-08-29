namespace TechAndTools.Web.Controllers
{
    using Commons.Constants;
    using Services.Contracts;
    using Services.Mapping;
    using ViewModels.Favorites;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using System.Linq;
    using System.Threading.Tasks;

    [Authorize(Roles = TechAndTools.Commons.Constants.GlobalConstants.UserRole +", " + GlobalConstants.AdminRole)]
    public class FavoritesController : BaseController
    {
        private readonly IProductService productService;

        public FavoritesController(IProductService productService)
        {
            this.productService = productService;
        }

        public IActionResult My()
        {
            var favoriteProductsServiceModels = this.productService
                .AllFavoriteProducts(this.User.Identity.Name)
                .ToList();

                var viewModels = favoriteProductsServiceModels
                    .Select(x => x.To<FavoriteProductViewModel>())
                    .ToList();

            return View(viewModels);
        }

        public async Task<IActionResult> AddToFavorites(int id)
        {
            await this.productService.AddToFavoritesAsync(id, this.User.Identity.Name);
            
            return RedirectToAction(nameof(My));
        }

        public async Task<IActionResult> RemoveFromFavorites(int id)
        {
            await this.productService.RemoveFromFavorites(id, this.User.Identity.Name);

            return RedirectToAction(nameof(My));
        }
    }
}