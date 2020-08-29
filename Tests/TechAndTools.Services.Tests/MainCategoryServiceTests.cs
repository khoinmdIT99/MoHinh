namespace TechAndTools.Services.Tests
{
    using Common;
    using Contracts;
    using Data;
    using Data.Models;
    using Mapping;
    using Models;

    using Microsoft.EntityFrameworkCore;
    using Xunit;
    
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class MainCategoryServiceTests
    {
        
        private List<MainCategory> GetMainCategoriesData()
        {
            return new List<MainCategory>
            {
                new MainCategory
                {
                    Id = 1,
                    Name = "PC"
                },
                new MainCategory
                {
                    Id = 2,
                    Name = "Tools"
                }
            };
        }

        private async Task SeedData(TechAndToolsDbContext context)
        {
            context.AddRange(GetMainCategoriesData());
            await context.SaveChangesAsync();
        }

        public MainCategoryServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Fact]
        public async void CreateMainCategoryAsyncShouldCreateMainCategory()
        {
            
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "CreateMainCategoryAsyncShouldCreateMainCategory")
                .Options;

            var context = new TechAndToolsDbContext(options);

            IMainCategoryService mainCategoryService = new MainCategoryService(context);

            await mainCategoryService.CreateAsync(new MainCategoryServiceModel{Name = "PC"});
            await mainCategoryService.CreateAsync(new MainCategoryServiceModel{Name = "Tools"});

            MainCategory first = context.MainCategories.First();
            MainCategory second = context.MainCategories.Last();

            Assert.Equal(2, context.MainCategories.Count());
            Assert.Equal("PC", first.Name);
            Assert.Equal("Tools", second.Name);
            Assert.NotNull(first);
            Assert.NotNull(first.Categories);
            Assert.NotNull(second);
            Assert.NotNull(second.Categories);
        }

        [Fact]
        public async void GetAllMainCategoriesAsyncShouldReturnAllMainCategories()
        {

            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "GetAllMainCategoriesAsyncShouldReturnAllMainCategories")
                .Options;

            var context = new TechAndToolsDbContext(options);

            await SeedData(context);

            IMainCategoryService mainCategoryService = new MainCategoryService(context);

            IEnumerable<MainCategoryServiceModel> mainCategories = mainCategoryService.GetAllMainCategories();

            Assert.NotNull(mainCategories);
            Assert.Equal(2, mainCategories.Count());
            Assert.Equal("PC", mainCategories.First().Name);
            Assert.Equal("Tools", mainCategories.Last().Name);
        }
        
        [Fact]
        public async void DeleteAsyncShouldDeleteMainCategoryFromDatabaseById()
        {

            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "DeleteAsyncShouldDeleteMainCategoryFromDatabaseById")
                .Options;

            var context = new TechAndToolsDbContext(options);

            await SeedData(context);

            IMainCategoryService mainCategoryService = new MainCategoryService(context);

            MainCategory mainCategoryFromDb = context.MainCategories.First();

            bool result = await mainCategoryService.DeleteAsync(mainCategoryFromDb.Id);

            Assert.True(result);
            Assert.Equal(1, context.MainCategories.Count());
        }
        
        [Fact]
        public async void EditAsyncShouldEditMainCategoryFromDatabaseById()
        {

            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "EditAsyncShouldEditMainCategoryFromDatabaseById")
                .Options;

            var context = new TechAndToolsDbContext(options);

            await SeedData(context);

            IMainCategoryService mainCategoryService = new MainCategoryService(context);

            MainCategory mainCategoryFromDb = context.MainCategories.First();

            mainCategoryFromDb.Name = "New Name";

            MainCategoryServiceModel expectedResult = mainCategoryFromDb.To<MainCategoryServiceModel>();

            await mainCategoryService.EditAsync(expectedResult);

            var actualResult = context.MainCategories.First();

            Assert.Equal(expectedResult.Name, actualResult.Name);
        }

         
        [Fact]
        public async void GetMainCategoryByIdShouldReturnServiceModelFromDatabaseById()
        {

            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "GetMainCategoryByIdShouldReturnServiceModelFromDatabaseById")
                .Options;

            var context = new TechAndToolsDbContext(options);

            await SeedData(context);

            IMainCategoryService mainCategoryService = new MainCategoryService(context);

            MainCategory expectedData = context.MainCategories.First();

            MainCategoryServiceModel actualData = mainCategoryService.GetMainCategoryById(expectedData.Id);
            ;
            Assert.Equal(expectedData.Name, actualData.Name);
            Assert.Equal(expectedData.Id, actualData.Id);
            Assert.NotNull(actualData.Categories);
            Assert.Equal(expectedData.Categories.Count, actualData.Categories.Count);
        }
    }
}
