namespace TechAndTools.Services.Models
{
    using Data.Models;
    using Mapping;

    public class AddressServiceModel : IMapFrom<Address>, IMapTo<Address>
    {
        public int Id { get; set; }

        public string TechAndToolsUserId { get; set; }
        
        public string City { get; set; }

        public string CityAddress { get; set; }

        public int PostCode { get; set; }
    }
}
