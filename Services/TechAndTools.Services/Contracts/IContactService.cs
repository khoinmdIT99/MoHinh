namespace TechAndTools.Services.Contracts
{
    using Models;

    using System.Linq;
    using System.Threading.Tasks;

    public interface IContactService
    {
        Task<ContactServiceModel> CreateAsync(ContactServiceModel serviceModel);

        Task<bool> DeleteAsync(int contactId);

        bool Archive(int contactId);

        IQueryable<ContactServiceModel> GetAllContacts();

        IQueryable<ContactServiceModel> GetAllArchivedContacts();
    }
}
