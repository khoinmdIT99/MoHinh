namespace TechAndTools.Services.Contracts
{
    using Models;

    using System.Linq;
    using System.Threading.Tasks;

    public interface IArticleService
    {
        Task<ArticleServiceModel> CreateArticleAsync(ArticleServiceModel articleServiceModel, string authorId);

        Task<ArticleServiceModel> EditArticleAsync(ArticleServiceModel articleServiceModel);

        Task<bool> DeleteArticleByIdAsync(int articleId);

        Task<ArticleServiceModel> GetArticleAsync(int articleId);

        IQueryable<ArticleServiceModel> GetAllArticles();

        IQueryable<ArticleServiceModel> GetAllByUserId(string userId);

        IQueryable<ArticleServiceModel> GetLastThreeArticles(int articleId);
    }
}
