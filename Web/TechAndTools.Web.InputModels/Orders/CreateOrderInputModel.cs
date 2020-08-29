namespace TechAndTools.Web.InputModels.Orders
{
    using Commons;
    using Data.Models.Enums;
    using Services.Mapping;
    using Services.Models;
    using ViewModels.Addresses;
    using ViewModels.PaymentMethods;
    using ViewModels.ShoppingCart;
    using ViewModels.Suppliers;

    using AutoMapper;
    
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CreateOrderInputModel : IMapTo<OrderServiceModel>, IHaveCustomMappings
    {
        private const string DisplayFirstName = "Име";
        private const string DisplayLastName = "Фамилия";
        private const string DisplayPhoneNumber = "Телефонен номер";
        private const string DisplaySupplier = "Куриер";
        private const string DisplayAddress = "Адрес за доставка";
        private const string DisplayPaymentMethod = "Метод на плащане";
        private const string DisplayShippingTo = "Доставка до";

        [Display(Name = DisplayFirstName)]
        [Required(ErrorMessage = InputModelsConstants.RequiredMessage)]
        public string FirstName { get; set; }

        [Display(Name = DisplayLastName)]
        [Required(ErrorMessage = InputModelsConstants.RequiredMessage)]
        public string LastName { get; set; }

        [Display(Name = DisplayPhoneNumber)]
        [Required(ErrorMessage = InputModelsConstants.RequiredMessage)]
        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        [Display(Name = DisplaySupplier)]
        [Required(ErrorMessage = InputModelsConstants.RequiredMessage)]
        public int SupplierId { get; set; }

        [Display(Name = DisplayAddress)]
        [Required(ErrorMessage = InputModelsConstants.RequiredMessage)]
        public int AddressId { get; set; }

        [Display(Name = DisplayPaymentMethod)]
        [Required(ErrorMessage = InputModelsConstants.RequiredMessage)]
        public int PaymentMethodId { get; set; }

        [Display(Name = DisplayShippingTo)]
        [Required(ErrorMessage = InputModelsConstants.RequiredMessage)]
        public ShippingTo ShippingTo { get; set; }

        public IEnumerable<AddressViewModel> AddressesViewModels { get; set; }

        public IEnumerable<SupplierViewModel> SuppliersViewModel { get; set; }

        public IEnumerable<PaymentMethodViewModel> PaymentMethodViewModels { get; set; }

        public IEnumerable<ShoppingCartProductViewModel> ShoppingCartProductViewModels { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<CreateOrderInputModel, OrderServiceModel>()
                .ForMember(dest => dest.RecipientPhoneNumber, ops => ops.MapFrom(origin => origin.PhoneNumber))
                .ForMember(dest => dest.Recipient,
                    opts => opts.MapFrom(origin => origin.FirstName + " " + origin.LastName));
        }
    }
}
