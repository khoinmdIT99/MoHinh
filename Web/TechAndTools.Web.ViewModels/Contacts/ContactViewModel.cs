﻿namespace TechAndTools.Web.ViewModels.Contacts
{
    using Services.Mapping;
    using Services.Models;

    public class ContactViewModel : IMapFrom<ContactServiceModel>
    {
        public int Id { get; set; }

        public string FullName { get;set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Message { get; set; }

        public bool MarkAsRead { get; set; }
    }
}
