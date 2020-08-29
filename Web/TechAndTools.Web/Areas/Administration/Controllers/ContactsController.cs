namespace TechAndTools.Web.Areas.Administration.Controllers
{
    using Services.Contracts;
    using Services.Mapping;
    using ViewModels.Contacts;

    using Microsoft.AspNetCore.Mvc;

    using System.Linq;
    using System.Threading.Tasks;

    public class ContactsController : AdministrationController
    {
        private readonly IContactService contactService;

        public ContactsController(IContactService contactService)
        {
            this.contactService = contactService;
        }

        public IActionResult All()
        {
            var contacts = this.contactService.GetAllContacts().ToList();
            
            var viewModels = contacts.Select(x => x.To<ContactViewModel>()).ToList();
            
            return View(viewModels);
        }

        public IActionResult AllArchived()
        {
            var contacts = this.contactService
                .GetAllArchivedContacts()
                .ToList();
            
            var viewModels = contacts.Where(x => x.IsArchived == true)
                .Select(x => x.To<ContactViewModel>()).ToList();
            
            return View(viewModels);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.contactService.DeleteAsync(id);

            return RedirectToAction(nameof(AllArchived));
        }

        public IActionResult Archive(int id)
        {
            var userRequests = this.contactService.Archive(id);

            return RedirectToAction(nameof(All));
        }
    }
}