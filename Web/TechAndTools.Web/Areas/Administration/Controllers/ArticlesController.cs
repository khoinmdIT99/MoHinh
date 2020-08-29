namespace TechAndTools.Web.Areas.Administration.Controllers
{
    using InputModels.Articles;
    using Services.Contracts;
    using Services.Mapping;
    using Services.Models;
    using Services.Upload;
    using ViewModels.Articles;

    using Microsoft.AspNetCore.Mvc;

    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class ArticlesController : AdministrationController
    {
        private readonly IArticleService articleService;
        private readonly ICloudinaryService cloudinaryService;
        private readonly IImageService imageService;

        public ArticlesController(IArticleService articleService,
            ICloudinaryService cloudinaryService,
            IImageService imageService)
        {
            this.articleService = articleService;
            this.cloudinaryService = cloudinaryService;
            this.imageService = imageService;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateArticleInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            ArticleServiceModel articleServiceModel = inputModel.To<ArticleServiceModel>();

            string pictureUrl = await this.cloudinaryService
                .UploadPictureAsync(inputModel.ImageFormFile, inputModel.Title);

            ArticleServiceModel productFromDb = await this.articleService.CreateArticleAsync(articleServiceModel, this.User.FindFirstValue(ClaimTypes.NameIdentifier));

            await this.imageService.CreateWithArticleAsync(pictureUrl, productFromDb.Id);

            return this.RedirectToAction("All");
        }

        public IActionResult All()
        {
            IEnumerable<AllArticleViewModel> articles = this.articleService.GetAllArticles()
                .To<AllArticleViewModel>()
                .ToList();

            return this.View(articles);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.articleService.DeleteArticleByIdAsync(id);

            return this.RedirectToAction("All");
        }

        public async Task<IActionResult> Edit(int id)
        {
            EditArticleInputModel inputModel =
                (await this.articleService.GetArticleAsync(id)).To<EditArticleInputModel>();

            return this.View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditArticleInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            ArticleServiceModel articleServiceModel = inputModel.To<ArticleServiceModel>();

            if (inputModel.ImageFormFile != null)
            {
                string imageUrl = await this.cloudinaryService
                    .UploadPictureAsync(inputModel.ImageFormFile, inputModel.Title);

                await this.imageService.EditWithArticleAsync(imageUrl, articleServiceModel.Id);
            }

            await this.articleService.EditArticleAsync(articleServiceModel);

            return this.RedirectToAction("All", "Articles");
        }
    }
}
