namespace TechAndTools.Web.ViewModels.Addresses
{
    using Data.Models;
    using Services.Mapping;
    using Services.Models;

    public class AddressViewModel : IMapFrom<AddressServiceModel>, IMapFrom<Address>
    {
        public int Id { get; set; }
        
        public string City { get; set; }

        public string CityAddress { get; set; }

        public int PostCode { get; set; }
    }
}
