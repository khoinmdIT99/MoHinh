namespace TechAndTools.Services
{
    using Contracts;
    using Data;
    using Data.Models;
    using Mapping;
    using Models;

    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class ContactService : IContactService
    {
        private readonly TechAndToolsDbContext context;

        public ContactService(TechAndToolsDbContext context)
        {
            this.context = context;
        }

        public async Task<ContactServiceModel> CreateAsync(ContactServiceModel serviceModel)
        {
            Contact contact = serviceModel.To<Contact>();

            await this.context.Contacts.AddAsync(contact);
            await this.context.SaveChangesAsync();

            return contact.To<ContactServiceModel>();
        }

        public async Task<bool> DeleteAsync(int contactId)
        {
            Contact contact = this.context.Contacts
                .Find(contactId);

            if (contact == null)
            {
                throw new ArgumentNullException(nameof(contact));
            }

            this.context.Contacts.Remove(contact);
            int result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public bool Archive(int contactId)
        {
            Contact contact = this.context.Contacts.FirstOrDefault(x => x.Id == contactId);

            if (contact == null)
            {
                throw new ArgumentNullException(nameof(contact));
            }

            contact.IsArchived = true;

            this.context.Contacts.Update(contact);
            int result = this.context.SaveChanges();

            return result > 0;
        }

        public IQueryable<ContactServiceModel> GetAllContacts()
        {
            return this.context.Contacts.Where(x => x.IsArchived == false).To<ContactServiceModel>();
        }

        public IQueryable<ContactServiceModel> GetAllArchivedContacts()
        {
            return this.context.Contacts.Where(x => x.IsArchived == true).To<ContactServiceModel>();
        }
    }
}
