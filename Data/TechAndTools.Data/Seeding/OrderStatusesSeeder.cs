namespace TechAndTools.Data.Seeding
{
    using Models;

    using Microsoft.EntityFrameworkCore;

    using System;
    using System.Threading.Tasks;

    public class OrderStatusesSeeder : ISeeder
    {
        public async Task SeedAsync(TechAndToolsDbContext dbContext, IServiceProvider serviceProvider)
        {           
            bool hasOrderStatuses = await dbContext.OrderStatuses.AnyAsync();

            if (!hasOrderStatuses)
            {
                await SeedOrderStatusAsync("Необработена", dbContext);
                await SeedOrderStatusAsync("Обработена", dbContext);
                await SeedOrderStatusAsync("Доставена", dbContext);

                await dbContext.SaveChangesAsync();
            }
        }
        
        private static async Task SeedOrderStatusAsync(string orderStatusName, TechAndToolsDbContext dbContext)
        {
            var orderStatus = new OrderStatus { Name = orderStatusName };

            await dbContext.OrderStatuses.AddAsync(orderStatus);
        }
    }
}
