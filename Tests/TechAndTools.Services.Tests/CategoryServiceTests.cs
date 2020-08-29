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

    public class CategoryServiceTests
    {
        private List<Category> GetCategoriesData()
        {
            return new List<Category>
            {
                new Category
                {
                    Id = 1,
                    Name = "category1",
                    MainCategoryId = 1
                },
                new Category
                {
                    Id = 2,
                    Name = "category2",
                    MainCategoryId = 1
                }
            };
        }

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
                    Name = "PC2"
                }
            };
        }

        private async Task SeedData(TechAndToolsDbContext context)
        {
            context.AddRange(GetCategoriesData());
            context.AddRange(GetMainCategoriesData());
            await context.SaveChangesAsync();
        }

        public CategoryServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Fact]
        public async void CreateCategoryAsync_ShouldCreateCategory()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "CreateCategoryAsync_ShouldCreateCategory")
                .Options;

            var context = new TechAndToolsDbContext(options);

            context.Database.EnsureDeleted();

            var mainCategory = new MainCategory
            {
                Id = 1,
                Name = "PC"
            };

            await context.MainCategories.AddAsync(mainCategory);
            await context.SaveChangesAsync();

            ICategoryService categoryService = new CategoryService(context);

            await categoryService.CreateCategoryAsync(new CategoryServiceModel { Name = "category1", MainCategoryId = 1 });
            await categoryService.CreateCategoryAsync(new CategoryServiceModel { Name = "category2", MainCategoryId = 1 });

            int expectedResult = 2;
            int actualResult = context.Categories.Count();

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async void GetAllCategoriesByMainCategoryIdAsync_ShouldReturnAllCategoriesWithSameMainCategory()
        {

            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "GetAllCategoriesByMainCategoryIdAsync_ShouldReturnAllCategoriesWithSameMainCategory")
                .Options;

            var context = new TechAndToolsDbContext(options);

            await SeedData(context);

            ICategoryService categoryService = new CategoryService(context);

            var mainCategoryFromDb = context.MainCategories.First();

            var categories = categoryService.GetAllCategoriesByMainCategoryId(mainCategoryFromDb.Id);

            int expectedResult = context.Categories.Count();
            int actualResult = categories.Count();

            Assert.NotNull(categories);
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task EditCategoryAsync_ShouldEditCategoryById()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "EditCategoryAsync_ShouldEditCategoryById")
                .Options;

            var context = new TechAndToolsDbContext(options);

            await SeedData(context);

            ICategoryService categoryService = new CategoryService(context);

            var categoryId = 1;
            var newName = "newName";
            var newMainCategoryId = 2;

            var expectedData = context.Categories.Find(categoryId).To<CategoryServiceModel>();
            expectedData.MainCategoryId = newMainCategoryId;
            expectedData.Name = newName;

            await categoryService.EditCategoryAsync(expectedData);

            var actualData = context.Categories.Find(categoryId);

            Assert.Equal(expectedData.Name, actualData.Name);
            Assert.Equal(expectedData.MainCategoryId, actualData.MainCategoryId);
        }

        [Fact]
        public async Task DeleteAsyncShould_DeleteCategoryFromDatabase()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "DeleteAsyncShould_DeleteCategoryFromDatabase")
                .Options;

            var context = new TechAndToolsDbContext(options);

            await SeedData(context);

            ICategoryService categoryService = new CategoryService(context);

            var categoryFromDb = context.Categories.First();

            bool result = await categoryService.DeleteAsync(categoryFromDb.Id);

            Assert.True(result);
        }

        [Fact]
        public async Task GetAllCategories_ShouldReturnAllCategories()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "GetAllCategories_ShouldReturnAllCategories")
                .Options;

            var context = new TechAndToolsDbContext(options);

            await SeedData(context);

            ICategoryService categoryService = new CategoryService(context);

            int expectedCount = context.Categories.Count();
            int actualCount = categoryService.GetAllCategories().ToList().Count;

            Assert.Equal(expectedCount, actualCount);
        }


        [Fact]
        public async Task GetCategoryByIdShould_ReturnCategoryById()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "GetCategoryByIdShould_ReturnCategoryById")
                .Options;

            var context = new TechAndToolsDbContext(options);

            await SeedData(context);

            ICategoryService categoryService = new CategoryService(context);

            int categoryId = 1;

            Category category = context.Categories.Find(categoryId);

            var actualResult = categoryService.GetCategoryById(category.Id);

            Assert.NotNull(actualResult);
            Assert.Equal(categoryId, actualResult.Id);
        }
    }
}
