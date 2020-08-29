namespace TechAndTools.Services.Tests
{
    using Common;
    using Contracts;
    using Data;
    using Data.Models;
    using Mapping;
    using Models;

    using Xunit;
    using Microsoft.EntityFrameworkCore;
    
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class BrandServiceTests
    {
        private List<Brand> GetBrandsData()
        {
            return new List<Brand>
            {
                new Brand
                {
                    Id = 1,
                    Name = "brand1",
                    LogoUrl = "logo1",
                    OfficialSite = "site1"
                },
                new Brand
                {
                    Id = 2,
                    Name = "brand2",
                    LogoUrl = "logo2",
                    OfficialSite = "site2"
                }
            };
        }

        private async Task SeedData(TechAndToolsDbContext context)
        {
            context.AddRange(GetBrandsData());
            await context.SaveChangesAsync();
        }

        public BrandServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Fact]
        public async Task CreateBrandAsync_ShouldCreateBrand()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "CreateBrandAsync_ShouldCreateBrand")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);
            IBrandService brandService = new BrandService(context);

            await brandService.CreateAsync(new BrandServiceModel { Name = "name1", LogoUrl = "Logo1", OfficialSite = "Site1" });
            await brandService.CreateAsync(new BrandServiceModel { Name = "name2", LogoUrl = "Logo2", OfficialSite = "Site2" });

            int expectedResult = 2;
            int actualResult = context.Brands.Count();

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task GetAllBrands_ShouldReturnAllBrands()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "GetAllBrands_ShouldReturnAllBrands")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);
            await SeedData(context);
            IBrandService brandService = new BrandService(context);

            int expectedResult = context.Brands.Count();
            int actualResult = brandService.GetAllBrands().Count();

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task GetBrandById_ShouldReturnBrandByBrandId()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "GetBrandById_ShouldReturnBrandByBrandId")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);
            await SeedData(context);
            IBrandService brandService = new BrandService(context);

            int brandId = 1;

            BrandServiceModel actualData = await brandService.GetBrandByIdAsync(brandId);

            Assert.NotNull(actualData);
            Assert.Equal(brandId, actualData.Id);
        }

        [Fact]
        public async Task EditBrandAsync_ShouldEditBrandById()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "EditBrandAsync_ShouldEditBrandById")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);

            await SeedData(context);

            IBrandService brandService = new BrandService(context);

            Brand expectedData = context.Brands.First();

            expectedData.Name = "New Name";
            expectedData.LogoUrl = "New Logo";
            expectedData.OfficialSite = "New Site";

            BrandServiceModel serviceModel = expectedData.To<BrandServiceModel>();

            BrandServiceModel actualData = await brandService.EditAsync(serviceModel);

            Assert.Equal(expectedData.Id, actualData.Id);
            Assert.Equal(expectedData.Name, actualData.Name);
            Assert.Equal(expectedData.LogoUrl, actualData.LogoUrl);
            Assert.Equal(expectedData.OfficialSite, actualData.OfficialSite);
            Assert.NotNull(actualData);
            Assert.NotNull(actualData.Products);
        }

        [Fact]
        public async Task DeleteAsyncShould_DeleteBrandFromDatabaseById()
        {
            
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "DeleteAsyncShould_DeleteBrandFromDatabaseById")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);

            await SeedData(context);

            IBrandService brandService = new BrandService(context);

            Brand brandFromDb = context.Brands.First();

            bool result = await brandService.DeleteAsync(brandFromDb.Id);

            Assert.True(result);
        }
    }
}
