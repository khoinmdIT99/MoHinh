namespace TechAndTools.Services.Contracts
{
    using Models;

    using System.Linq;
    using System.Threading.Tasks;

    public interface ICategoryService
    {
        Task<CategoryServiceModel> CreateCategoryAsync(CategoryServiceModel categoryServiceModel);

        Task<CategoryServiceModel> EditCategoryAsync(CategoryServiceModel categoryServiceModel);

        Task<bool> DeleteAsync(int id);

        CategoryServiceModel GetCategoryById(int id);

        IQueryable<CategoryServiceModel> GetAllCategoriesByMainCategoryId(int mainCategoryId);

        IQueryable<CategoryServiceModel> GetAllCategories();
    }
}
