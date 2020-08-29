namespace TechAndTools.Services
{
    using Contracts;
    using Data;
    using Mapping;
    using Models;
    using TechAndTools.Data.Models;

    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class AddressService : IAddressService
    {
        private readonly TechAndToolsDbContext context;
        private readonly IUserService userService;

        public AddressService(TechAndToolsDbContext context,
            IUserService userService)
        {
            this.context = context;
            this.userService = userService;
        }

        public async Task<AddressServiceModel> CreateAsync(AddressServiceModel addressServiceModel, string username)
        {
            var address = addressServiceModel.To<Address>();
            var user = this.userService.GetUserByUsername(username);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));    
            }

            address.TechAndToolsUserId = user.Id;
            
            await this.context.Addresses.AddAsync(address);
            await this.context.SaveChangesAsync();
            
            return address.To<AddressServiceModel>();
        }

        public async Task<bool> DeleteByIdAsync(int addressId)
        {
            Address addressFromDb = this.context.Addresses.Find(addressId);

            if (addressFromDb == null)
            {
                throw new ArgumentNullException(nameof(addressFromDb));
            }

            this.context.Addresses.Remove(addressFromDb);

            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public IQueryable<AddressServiceModel> GetAllByUserId(string userId)
        {
            return this.context.Addresses.Where(x => x.TechAndToolsUserId == userId).To<AddressServiceModel>();
        }
    }
}
