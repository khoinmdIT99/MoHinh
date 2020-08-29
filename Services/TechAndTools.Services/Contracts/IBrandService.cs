namespace TechAndTools.Services.Contracts
{
    using Models;

    using System.Linq;
    using System.Threading.Tasks;

    public interface IBrandService
    {
        Task<BrandServiceModel> CreateAsync(BrandServiceModel serviceModel);

        Task<BrandServiceModel> EditAsync(BrandServiceModel serviceModel);

        Task<bool> DeleteAsync(int id);

        IQueryable<BrandServiceModel> GetAllBrands();

        Task<BrandServiceModel> GetBrandByIdAsync(int id);
    }
}
