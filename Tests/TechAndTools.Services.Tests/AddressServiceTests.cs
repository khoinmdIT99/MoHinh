namespace TechAndTools.Services.Tests
{
    using Common;
    using Contracts;
    using Data;
    using Data.Models;
    using Models;
    
    using Xunit;
    using Microsoft.EntityFrameworkCore;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AddressServiceTests
    {

        private List<Address> GetAddressesData()
        {
            return new List<Address>
            {
                new Address
                {
                    Id = 1,
                    City = "City1",
                    CityAddress = "Address1",
                    PostCode = 9001,
                    TechAndToolsUserId = "testId"
                },
                new Address
                {
                    Id = 2,
                    City = "City2",
                    CityAddress = "Address2",
                    PostCode = 9001,
                    TechAndToolsUserId = "testId"
                },
                new Address
                {
                    Id = 3,
                    City = "City3",
                    CityAddress = "Address3",
                    PostCode = 9001,
                    TechAndToolsUserId = "testId"
                },
                new Address
                {
                    Id = 4,
                    City = "City4",
                    CityAddress = "Address4",
                    TechAndToolsUserId = "testId"
                }
            };
        }

        private async Task SeedData(TechAndToolsDbContext context)
        {
            context.AddRange(GetAddressesData());
            await context.SaveChangesAsync();
        }

        public AddressServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Fact]
        public async void CreateAsync_ShouldCreateAndAddAddressToDatabase()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "CreateArticleAsync_ShouldCreateAndAddArticlesToDatabase")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);
            IUserService userService = new UserService(context);
            IAddressService addressService = new AddressService(context, userService);

            TechAndToolsUser testUser = new TechAndToolsUser
            {
                UserName = "testUsername",
                Email = "testEmail"
            };

            await context.AddAsync(testUser);
            await context.SaveChangesAsync();

            await addressService.CreateAsync(
                new AddressServiceModel
                {
                    Id = 1,
                    City = "CityTest1",
                    CityAddress = "CityAddressTest1",
                    PostCode = 9000,
                    TechAndToolsUserId = Guid.NewGuid().ToString()
                }, "testUsername");

            await addressService.CreateAsync(
                new AddressServiceModel
                {
                    Id = 2,
                    City = "CityTest2",
                    CityAddress = "CityAddressTest2",
                    PostCode = 9000,
                    TechAndToolsUserId = Guid.NewGuid().ToString()
                }, "testUsername");

            int expectedCount = 2;

            int actualCount = context.Addresses.ToList().Count;

            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public async void CreateAsync_WithIncorrectUserShouldThrowException()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "CreateAsync_WithIncorrectUserShouldThrowException")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);
            IUserService userService = new UserService(context);
            IAddressService addressService = new AddressService(context, userService);

            TechAndToolsUser testUser = new TechAndToolsUser
            {
                UserName = "testUsername",
                Email = "testEmail"
            };

            await context.AddAsync(testUser);
            await context.SaveChangesAsync();


            AddressServiceModel serviceModel = new AddressServiceModel
            {
                Id = 1,
                City = "CityTest1",
                CityAddress = "CityAddressTest1",
                PostCode = 9000,
                TechAndToolsUserId = Guid.NewGuid().ToString()
            };

            await Assert.ThrowsAsync<ArgumentNullException>(() => addressService.CreateAsync(serviceModel, "wrongUsername"));
        }

        [Fact]
        public async void DeleteByIdAsync_ShouldDeleteAddressById()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "DeleteByIdAsync_ShouldDeleteAddressById")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);
            await SeedData(context);
            IUserService userService = new UserService(context);
            IAddressService addressService = new AddressService(context, userService);

            int testId = 1;

            bool result = await addressService.DeleteByIdAsync(testId);

            Assert.True(result);
        }

        [Fact]
        public async void DeleteByIdAsync_WithIncorrectAddressIdShouldThrowException()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "DeleteByIdAsync_WithIncorrectAddressIdShouldThrowException")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);
            await SeedData(context);
            IUserService userService = new UserService(context);
            IAddressService addressService = new AddressService(context, userService);

            int wrongId = int.MaxValue;

            await Assert.ThrowsAsync<ArgumentNullException>(() => addressService.DeleteByIdAsync(wrongId));
        }

        [Fact]
        public async void GetAllByUserId_ShouldReturnAllUsersAddressesByUserId()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "GetAllByUserId_ShouldReturnAllUsersAddressesByUserId")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);
            await SeedData(context);
            IUserService userService = new UserService(context);
            IAddressService addressService = new AddressService(context, userService);

            TechAndToolsUser testUser = new TechAndToolsUser
            {
                Id = "testId",
                UserName = "testUsername",
                Email = "testEmail"
            };

            await context.AddAsync(testUser);
            await context.SaveChangesAsync();

            int expectedResult = context.Addresses.Where(x => x.TechAndToolsUserId == "testId").ToList().Count;
            int actualResult = addressService.GetAllByUserId("testId").ToList().Count;

            Assert.Equal(expectedResult, actualResult);
        }
    }
}
