namespace TechAndTools.Services
{
    using Contracts;
    using Data;
    using Mapping;
    using Models;
    using TechAndTools.Data.Models;

    using Microsoft.EntityFrameworkCore;
    
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class ArticleService : IArticleService
    {
        private readonly TechAndToolsDbContext context;

        public ArticleService(TechAndToolsDbContext context)
        {
            this.context = context;
        }

        public IQueryable<ArticleServiceModel> GetAllArticles()
        {
            return this.context.Articles.To<ArticleServiceModel>();
        }

        public IQueryable<ArticleServiceModel> GetAllByUserId(string userId)
        {
            return this.context.Articles
                .Where(x => x.AuthorId == userId)
                .To<ArticleServiceModel>();
        }

        public IQueryable<ArticleServiceModel> GetLastThreeArticles(int articleId)
        {
            return this.context.Articles
                .Where(x => x.Id != articleId)
                .OrderByDescending(x => x.CreatedOn)
                .Include(x => x.Image)
                .Take(3)
                .To<ArticleServiceModel>();
        }

        public async Task<ArticleServiceModel> CreateArticleAsync(ArticleServiceModel articleServiceModel, string authorId)
        {
            Article article = articleServiceModel.To<Article>();
            article.AuthorId = authorId;
            article.CreatedOn = DateTime.UtcNow;

            await this.context.Articles.AddAsync(article);
            await this.context.SaveChangesAsync();

            return article.To<ArticleServiceModel>();
        }

        public async Task<ArticleServiceModel> EditArticleAsync(ArticleServiceModel articleServiceModel)
        {
            Article articleFromDb = this.context.Articles
                .Find(articleServiceModel.Id);

            if (articleFromDb == null)
            {
                throw new ArgumentNullException(nameof(articleFromDb));
            }

            articleFromDb.Content = articleServiceModel.Content;
            articleFromDb.Title = articleServiceModel.Title;

            this.context.Articles.Update(articleFromDb);
            await this.context.SaveChangesAsync();

            return articleFromDb.To<ArticleServiceModel>();
        }

        public async Task<bool> DeleteArticleByIdAsync(int articleId)
        {
            Article articleFromDb = await this.context.Articles
                .SingleOrDefaultAsync(x => x.Id == articleId);

            if (articleFromDb == null)
            {
                throw new ArgumentNullException(nameof(articleFromDb));
            }

            this.context.Articles.Remove(articleFromDb);
            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<ArticleServiceModel> GetArticleAsync(int articleId)
        {
            Article articleFromDb = this.context.Articles
                .Include(x => x.Image)
                .SingleOrDefault(x => x.Id == articleId);

            if (articleFromDb == null)
            {
                throw new ArgumentNullException(nameof(articleFromDb));
            }

            this.IncrementTimesRead(articleFromDb);

            this.context.Articles.Update(articleFromDb);
            await this.context.SaveChangesAsync();

            return articleFromDb.To<ArticleServiceModel>();
        }

        private Article IncrementTimesRead(Article article)
        {
            article.TimesRead++;

            return article;
        }
    }
}
