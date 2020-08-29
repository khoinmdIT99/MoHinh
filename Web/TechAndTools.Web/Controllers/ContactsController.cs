namespace TechAndTools.Web.Controllers
{
    using Commons.Constants;
    using InputModels.Contacts;
    using Services.Contracts;
    using Services.Mapping;
    using Services.Models;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    
    using System.Threading.Tasks;

    [Authorize(Roles = GlobalConstants.UserRole +", " + GlobalConstants.AdminRole)]
    public class ContactsController : BaseController
    {
        private readonly IContactService contactService;

        public ContactsController(IContactService contactService)
        {
            this.contactService = contactService;
        }

        public IActionResult Contacts()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendContact(CreateContactInputModel inputModel)
        {
            await this.contactService.CreateAsync(inputModel.To<ContactServiceModel>());

            return this.Redirect("/");
        }
    }
}
