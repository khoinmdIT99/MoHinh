namespace TechAndTools.Data.Seeding
{
    using Commons.Constants;
    using Models;
    using System;

    using Microsoft.EntityFrameworkCore;

    using System.Threading.Tasks;

    public class PaymentStatusesSeeder : ISeeder
    {
        public async Task SeedAsync(TechAndToolsDbContext dbContext, IServiceProvider serviceProvider)
        {
            bool hasPaymentStatuses = await dbContext.PaymentStatuses.AnyAsync();

            if (!hasPaymentStatuses)
            {
                await SeedPaymentStatusAsync(GlobalConstants.Unpaid, dbContext);
                await SeedPaymentStatusAsync(GlobalConstants.Paid, dbContext);
            }
        }

        private static async Task SeedPaymentStatusAsync(string paymentName, TechAndToolsDbContext dbContext)
        {
            var paymentStatus = new PaymentStatus { Name = paymentName };

            await dbContext.PaymentStatuses.AddAsync(paymentStatus);
            await dbContext.SaveChangesAsync();
        }
    }
}
