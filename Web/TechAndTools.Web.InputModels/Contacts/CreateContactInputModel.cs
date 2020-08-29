namespace TechAndTools.Web.InputModels.Contacts
{
    using Commons;
    using Services.Mapping;
    using Services.Models;

    using System.ComponentModel.DataAnnotations;

    public class CreateContactInputModel : IMapTo<ContactServiceModel>
    {
        private const int NameMaxLength = 25;
        private const int NameMinLength = 3;
        
        private const int EmailMaxLength = 25;
        private const int EmailMinLength = 3;

        private const int MessageMaxLength = 255;
        private const int MessageMinLength = 3;

        private const string DisplayName = "Име";
        private const string DisplayEmail = "Email";
        private const string DisplayPhone = "Телефон";
        private const string DisplayMessage = "Съдържание";

        [Display(Name = DisplayName)]
        [Required(ErrorMessage = InputModelsConstants.RequiredMessage)]
        [StringLength(NameMaxLength, ErrorMessage = InputModelsConstants.StringLengthMessage, MinimumLength = NameMinLength)]
        public string FullName { get; set; }
        
        
        [Display(Name = DisplayEmail)]
        [Required(ErrorMessage = InputModelsConstants.RequiredMessage)]
        [StringLength(EmailMaxLength, ErrorMessage = InputModelsConstants.StringLengthMessage, MinimumLength = EmailMinLength)]
        [EmailAddress]
        public string Email { get; set; }
        
        [Display(Name = DisplayPhone)]
        [Required(ErrorMessage = InputModelsConstants.RequiredMessage)]
        [Phone]
        public string Phone { get; set; }
        
        [Display(Name = DisplayMessage)]
        [Required(ErrorMessage = InputModelsConstants.RequiredMessage)]
        [StringLength(MessageMaxLength, ErrorMessage = InputModelsConstants.StringLengthMessage, MinimumLength = MessageMinLength)]
        public string Message { get; set; }
    }
}
