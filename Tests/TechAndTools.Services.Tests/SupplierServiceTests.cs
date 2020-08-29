namespace TechAndTools.Services.Tests
{
    using Common;
    using Contracts;
    using Data;
    using Data.Models;
    using Data.Models.Enums;
    using Models;

    using Microsoft.EntityFrameworkCore;
    using Xunit;
    
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SupplierServiceTests
    {
        private List<Supplier> GetSuppliersData()
        {
            return new List<Supplier>
            {
                new Supplier
                {
                    Id = 1,
                    Name = "Name1",
                    DeliveryTimeInDays = 1,
                    PriceToOffice = 2,
                    PriceToAddress = 5
                },
                new Supplier
                {
                    Id = 2,
                    Name = "Name2",
                    DeliveryTimeInDays = 2,
                    PriceToOffice = 3,
                    PriceToAddress =4
                },
                new Supplier
                {
                    Id = 3,
                    Name = "Name3",
                    DeliveryTimeInDays = 3,
                    PriceToOffice = 3,
                    PriceToAddress = 3
                }
            };
        }

        private async Task SeedData(TechAndToolsDbContext context)
        {
            context.AddRange(GetSuppliersData());
            await context.SaveChangesAsync();
        }

        public SupplierServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Fact]
        public async void CreateAsync_ShouldCreateSuppliersAndAddToDatabase()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "CreateAsync_ShouldCreateSuppliersAndAddToDatabase")
                .Options;

            TechAndToolsDbContext dbContext = new TechAndToolsDbContext(options);

            ISupplierService supplierService = new SupplierService(dbContext);

            await supplierService.CreateAsync(new SupplierServiceModel
            {
                Name = "name1",
                DeliveryTimeInDays = 2,
                PriceToAddress = 2,
                PriceToOffice = 3
            });

            await supplierService.CreateAsync(new SupplierServiceModel
            {
                Name = "name2",
                DeliveryTimeInDays = 3,
                PriceToAddress = 3,
                PriceToOffice = 4
            });

            int expectedResult = 2;

            int actualResult = await dbContext.Suppliers.CountAsync();

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async void EditAsync_ShouldEditSupplier()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "EditAsync_ShouldEditSupplier")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);

            await SeedData(context);

            ISupplierService supplierService = new SupplierService(context);
            
            SupplierServiceModel serviceModel = new SupplierServiceModel
            {
                Id = 1,
                Name = "edited",
                DeliveryTimeInDays = 5,
                PriceToOffice = 10,
                PriceToAddress = 10
            };
            
            var actualResult = await supplierService.EditAsync(serviceModel);

            Assert.NotNull(actualResult);
            Assert.Equal(serviceModel.Name, actualResult.Name);
            Assert.Equal(serviceModel.DeliveryTimeInDays, actualResult.DeliveryTimeInDays);
            Assert.Equal(serviceModel.PriceToAddress, actualResult.PriceToAddress);
            Assert.Equal(serviceModel.PriceToOffice, actualResult.PriceToOffice);
        }

        [Fact]
        public async void EditAsync_ShouldThrowArgumentNullExceptionWithInvalidData()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "EditAsync_ShouldThrowArgumentNullExceptionWithInvalidData")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);

            await SeedData(context);

            ISupplierService supplierService = new SupplierService(context);
            
            SupplierServiceModel serviceModel = new SupplierServiceModel();

            await Assert.ThrowsAsync<ArgumentNullException>(() => supplierService.EditAsync(serviceModel));
        }

        [Fact]
        public async void DeleteAsync_ShouldRemoveSupplierFromDatabase()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "DeleteAsync_ShouldRemoveSupplierFromDatabase")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);

            await SeedData(context);

            ISupplierService supplierService = new SupplierService(context);

            int testSupplierId = 1;

            int expectedSuppliersCount = context.Suppliers.Count() - 1;

            bool result = await supplierService.DeleteAsync(testSupplierId);
            int actualSupplierCount = context.Suppliers.Count();

            Assert.True(result);
            Assert.Equal(expectedSuppliersCount, actualSupplierCount);
        }

        [Fact]
        public async void DeleteAsync_WithIncorrectIdShouldThrowException()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "DeleteAsync_WithIncorrectIdShouldThrowException")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);

            await SeedData(context);

            ISupplierService supplierService = new SupplierService(context);

            int testSupplierId = 111111;

            await Assert.ThrowsAsync<ArgumentNullException>(() => supplierService.DeleteAsync(testSupplierId));
        }

        [Fact]
        public async void GetSupplierById_ShouldReturnSupplierFromDatabase()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "GetSupplierById_ShouldReturnSupplierFromDatabase")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);

            await SeedData(context);

            ISupplierService supplierService = new SupplierService(context);

            int testSupplierId = 1;

            var expectedResult = context.Suppliers.Find(testSupplierId);
            var actualResult = supplierService.GetSupplierById(testSupplierId);

            Assert.Equal(expectedResult.Id, actualResult.Id);
            Assert.Equal(expectedResult.Name, actualResult.Name);
            Assert.Equal(expectedResult.PriceToOffice, actualResult.PriceToOffice);
            Assert.Equal(expectedResult.PriceToAddress, actualResult.PriceToAddress);
            Assert.Equal(expectedResult.DeliveryTimeInDays, actualResult.DeliveryTimeInDays);
        }

        [Fact]
        public async void GetSupplierById_WithIncorrectIdShouldThrowException()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "GetSupplierById_WithIncorrectIdShouldThrowException")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);

            await SeedData(context);

            ISupplierService supplierService = new SupplierService(context);

            int testSupplierId = 111111;

            Assert.Throws<ArgumentNullException>(() => supplierService.GetSupplierById(testSupplierId));
        }

        [Fact]
        public async void GetAllSuppliers_ShouldReturnAllSupplierFromDatabase()
        {
            
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "GetAllSuppliers_ShouldReturnAllSupplierFromDatabase")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);

            await SeedData(context);

            ISupplierService supplierService = new SupplierService(context);

            int expectedResult = context.Suppliers.Count();
            int actualResult = supplierService.GetAllSuppliers().Count();

            Assert.Equal(expectedResult, actualResult);
        }
        
        [Fact]
        public async void GetDeliveryPrice_ShouldReturnPriceBySupplierId()
        {
            
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "GetDeliveryPrice_ShouldReturnPriceBySupplierId")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);

            await SeedData(context);

            ISupplierService supplierService = new SupplierService(context);

            int testSupplierId = 1;

            var expectedResult = context.Suppliers.Find(testSupplierId).PriceToAddress;

            decimal actualResult = supplierService.GetDeliveryPrice(testSupplierId, ShippingTo.Address);

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async void GetDeliveryPrice_WithInvalidIdShouldThrowException()
        {
            
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "GetDeliveryPrice_WithInvalidIdShouldThrowException")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);

            await SeedData(context);

            ISupplierService supplierService = new SupplierService(context);

            int testSupplierId = 111;

            Assert.Throws<ArgumentNullException>(() =>
                supplierService.GetDeliveryPrice(testSupplierId, ShippingTo.Office));
        }
    }
}
