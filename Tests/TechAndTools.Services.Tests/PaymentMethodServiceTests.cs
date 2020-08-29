namespace TechAndTools.Services.Tests
{
    using Common;
    using Contracts;
    using Data;
    using Data.Models;
    
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class PaymentMethodServiceTests
    {
        private List<PaymentMethod> GetPaymentMethodsData()
        {
            return new List<PaymentMethod>
            {
                new PaymentMethod
                {
                    Id = 1,
                    Name = "payment1"
                },
                new PaymentMethod
                {
                    Id = 2,
                    Name = "payment2"
                }
            };
        }

        private async Task SeedData(TechAndToolsDbContext context)
        {
            context.AddRange(GetPaymentMethodsData());
            await context.SaveChangesAsync();
        }

        public PaymentMethodServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Fact]
        public async void GetAllPaymentMethods_ShouldReturnAllPaymentMethodsFromDatabase()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "GetAllPaymentMethods_ShouldReturnAllPaymentMethods")
                .Options;

            var context = new TechAndToolsDbContext(options);

            await SeedData(context);

            IPaymentMethodService paymentMethodService = new PaymentMethodService(context);

            var expectedResult = await context.PaymentMethods.ToListAsync();
            var actualResult = await paymentMethodService.GetAllPaymentMethods().ToListAsync();

            Assert.Equal(expectedResult.Count, actualResult.Count);
        }

        [Fact]
        public async void GetPaymentMethodByName_ShouldReturnPaymentMethodFromDatabase()
        {
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "GetPaymentMethodByName_ShouldReturnPaymentMethodFromDatabase")
                .Options;

            var context = new TechAndToolsDbContext(options);

            await SeedData(context);

            IPaymentMethodService paymentMethodService = new PaymentMethodService(context);

            var actualResult = await paymentMethodService.GetPaymentMethodByName("payment1");
            
            Assert.NotNull(actualResult);
            Assert.Equal("payment1", actualResult.Name);
        }

        [Fact]
        public async void GetPaymentMethodByName_ShouldThrowArgumentNullExceptionIfMethodIsNull()
        {
            
            var options = new DbContextOptionsBuilder<TechAndToolsDbContext>()
                .UseInMemoryDatabase(databaseName: "GetPaymentMethodByName_ShouldThrowArgumentNullExceptionIfMethodIsNull")
                .Options;

            var context = new TechAndToolsDbContext(options);

            await SeedData(context);
            
            IPaymentMethodService paymentMethodService = new PaymentMethodService(context);

            await Assert.ThrowsAsync<ArgumentNullException>(() => paymentMethodService.GetPaymentMethodByName("blabla"));
        }
    }
}
