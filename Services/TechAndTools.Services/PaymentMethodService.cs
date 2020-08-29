namespace TechAndTools.Services
{
    using Contracts;
    using Data;
    using Data.Models;
    using Mapping;
    using Models;

    using Microsoft.EntityFrameworkCore;

    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly TechAndToolsDbContext context;

        public PaymentMethodService(TechAndToolsDbContext context)
        {
            this.context = context;
        }

        public IQueryable<PaymentMethodServiceModel> GetAllPaymentMethods()
        {
            return this.context.PaymentMethods.To<PaymentMethodServiceModel>();
        }

        public async Task<PaymentMethodServiceModel> GetPaymentMethodByName(string paymentMethodName)
        {
            PaymentMethod paymentMethod = await this.context.PaymentMethods.FirstOrDefaultAsync(x => x.Name == paymentMethodName);

            if (paymentMethod == null)
            {
                throw new ArgumentNullException(nameof(paymentMethod));
            }

            return paymentMethod.To<PaymentMethodServiceModel>();
        }
    }
}
