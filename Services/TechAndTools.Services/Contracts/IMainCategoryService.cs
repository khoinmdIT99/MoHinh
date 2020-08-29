namespace TechAndTools.Services.Contracts
{
    using Models;

    using System.Linq;
    using System.Threading.Tasks;

    public interface IMainCategoryService
    {
        Task<MainCategoryServiceModel> CreateAsync (MainCategoryServiceModel serviceModel);

        IQueryable<MainCategoryServiceModel> GetAllMainCategories();

        MainCategoryServiceModel GetMainCategoryById(int id);

        Task<MainCategoryServiceModel> EditAsync(MainCategoryServiceModel mainCategoryServiceModel);

        Task<bool> DeleteAsync(int id);
    }
}
