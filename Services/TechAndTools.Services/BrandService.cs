namespace TechAndTools.Services
{
    using Contracts;
    using Data;
    using Data.Models;
    using Mapping;
    using Models;

    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class BrandService : IBrandService
    {
        private readonly TechAndToolsDbContext context;

        public BrandService(TechAndToolsDbContext context)
        {
            this.context = context;
        }

        public async Task<BrandServiceModel> CreateAsync(BrandServiceModel serviceModel)
        {

            Brand brand = serviceModel.To<Brand>();

            await this.context.Brands.AddAsync(brand);

            await this.context.SaveChangesAsync();

            return serviceModel;
        }

        public async Task<BrandServiceModel> EditAsync(BrandServiceModel serviceModel)
        {
            Brand brand = this.context.Brands.FirstOrDefault(x => x.Id == serviceModel.Id);

            if (brand == null)
            {
                throw new ArgumentNullException(nameof(brand));
            }

            brand.Name = serviceModel.Name;
            brand.LogoUrl = serviceModel.LogoUrl;
            brand.OfficialSite = serviceModel.OfficialSite;

            this.context.Brands.Update(brand);

            await this.context.SaveChangesAsync();

            return brand.To<BrandServiceModel>();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Brand brand = await this.context.Brands.FindAsync(id);

            if (brand == null)
            {
                throw new ArgumentNullException(nameof(brand));
            }

            this.context.Brands.Remove(brand);

            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public IQueryable<BrandServiceModel> GetAllBrands()
        {
            return this.context.Brands.To<BrandServiceModel>();
        }

        public async Task<BrandServiceModel> GetBrandByIdAsync(int id)
        {
            Brand brandFromDb = await this.context.Brands.FindAsync(id);

            return brandFromDb.To<BrandServiceModel>();
        }
    }
}
