namespace TechAndTools.Services.Contracts
{
    using Models;

    using System.Linq;
    using System.Threading.Tasks;

    public interface IPaymentMethodService
    {
        IQueryable<PaymentMethodServiceModel> GetAllPaymentMethods();

        Task<PaymentMethodServiceModel> GetPaymentMethodByName(string paymentMethodName);
    }
}
