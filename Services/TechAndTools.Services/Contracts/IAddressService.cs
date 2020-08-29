namespace TechAndTools.Services.Contracts
{
    using Models;

    using System.Linq;
    using System.Threading.Tasks;

    public interface IAddressService
    {
        Task<AddressServiceModel> CreateAsync(AddressServiceModel addressServiceModel, string username);

        Task<bool> DeleteByIdAsync(int addressId);

        IQueryable<AddressServiceModel> GetAllByUserId(string userId);
    }
}
