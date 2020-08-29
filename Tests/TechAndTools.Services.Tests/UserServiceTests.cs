namespace TechAndTools.Services.Tests
{
    using Common;
    using Contracts;
    using Data;
    using Data.Models;
    
    using Xunit;
    using Microsoft.EntityFrameworkCore;
    
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class UserServiceTests
    {
        private List<TechAndToolsUser> GetUsersData()
        {
            return new List<TechAndToolsUser>
            {
                new TechAndToolsUser
                {
                    Id = "testId1",
                    UserName = "testUsername1",
                    FirstName = "testFirstName1",
                    LastName = "testLastName1",
                    Email = "testEmail1"
                },
                new TechAndToolsUser
                {
                    Id = "testId2",
                    UserName = "testUsername2",
                    FirstName = "testFirstName2",
                    LastName = "testLastName2",
                    Email = "testEmail2"
                }
            };
        }

        private async Task SeedData(TechAndToolsDbContext context)
        {
            context.AddRange(GetUsersData());
            await context.SaveChangesAsync();
        }

        public UserServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Fact]
        public async void GetUserByUsername_ShouldReturnUserFromDatabase()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "GetUserByUsername_ShouldReturnUserFromDatabase")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);
            await this.SeedData(context);
            IUserService userService = new UserService(context);

            string testUsername = "testUsername1";

            var userFromDb = userService.GetUserByUsername(testUsername);

            Assert.NotNull(userFromDb);
            Assert.Equal(testUsername, userFromDb.UserName);
        }

        [Fact]
        public async void GetUserById_ShouldReturnUserFromDatabase()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "GetUserById_ShouldReturnUserFromDatabase")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);
            await this.SeedData(context);
            IUserService userService = new UserService(context);

            string testId = "testId1";

            var userFromDb = userService.GetUserById(testId);

            Assert.NotNull(userFromDb);
            Assert.Equal(testId, userFromDb.Id);
        }

        [Fact]
        public async void EditFirstName_ShouldEditUsersFirstNameAndReturnTrue()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "EditFirstName_ShouldEditUsersFirstNameAndReturnTrue")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);
            await this.SeedData(context);
            IUserService userService = new UserService(context);

            string testId = "testId1";
            string testNewFirstName = "newFirstName";
            var userFromDb = userService.GetUserById(testId);

            bool result = await userService.EditFirstName(userFromDb, testNewFirstName);

            Assert.True(result);
            Assert.Equal(testNewFirstName, userFromDb.FirstName);
        }
        
        [Fact]
        public async void EditFirstName_WithIncorrectUserShouldThrowException()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "EditFirstName_WithIncorrectUserShouldThrowException")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);
            await this.SeedData(context);
            IUserService userService = new UserService(context);

            string testWrongId = "wrongId";
            string testNewFirstName = "newFirstName";
            var userFromDb = userService.GetUserById(testWrongId);

            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                userService.EditFirstName(userFromDb, testNewFirstName));
        }

        [Fact]
        public async void EditFirstName_WithIncorrectFirstNameShouldThrowException()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "EditFirstName_WithIncorrectFirstNameShouldThrowException")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);
            await this.SeedData(context);
            IUserService userService = new UserService(context);

            string testId = "testId1";
            string testNewFirstName = null;
            var userFromDb = userService.GetUserById(testId);

            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                userService.EditFirstName(userFromDb, testNewFirstName));
        }

        [Fact]
        public async void EditLastName_ShouldEditUsersLastNameAndReturnTrue()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "EditLastName_ShouldEditUsersLastNameAndReturnTrue")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);
            await this.SeedData(context);
            IUserService userService = new UserService(context);

            string testId = "testId1";
            string testNewLastName = "newLastName";
            var userFromDb = userService.GetUserById(testId);

            bool result = await userService.EditLastName(userFromDb, testNewLastName);

            Assert.True(result);
            Assert.Equal(testNewLastName, userFromDb.LastName);
        }
        [Fact]
        public async void EditLastName_WithIncorrectUserShouldThrowException()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "EditLastName_WithIncorrectUserShouldThrowException")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);
            await this.SeedData(context);
            IUserService userService = new UserService(context);

            string testWrongId = "wrongId";
            string testNewLastName = "newLastName";
            var userFromDb = userService.GetUserById(testWrongId);

            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                userService.EditLastName(userFromDb, testNewLastName));
        }

        [Fact]
        public async void EditLastName_WithIncorrectLastNameShouldThrowException()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "EditLastName_WithIncorrectLastNameShouldThrowException")
                .Options;

            TechAndToolsDbContext context = new TechAndToolsDbContext(options);
            await this.SeedData(context);
            IUserService userService = new UserService(context);

            string testId = "testId1";
            string testNewLastName = null;
            var userFromDb = userService.GetUserById(testId);

            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                userService.EditLastName(userFromDb, testNewLastName));
        }
    }
}
