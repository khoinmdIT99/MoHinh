namespace TechAndTools.Services.Contracts
{
    using Models;

    using System.Threading.Tasks;

    public interface IImageService
    {
        Task<ImageServiceModel> CreateWithProductAsync(string imageUrl, int productId);

        Task<ImageServiceModel> CreateWithArticleAsync(string imageUrl, int articleId);

        Task<ImageServiceModel> EditWithArticleAsync(string imageUrl, int articleId);
    }
}
