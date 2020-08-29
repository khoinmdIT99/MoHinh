namespace TechAndTools.Services.Models
{
    using Data.Models;
    using Mapping;

    public class ReviewServiceModel : IMapFrom<Review>, IMapTo<Review>
    {
        public string Id { get; set; }

        public string UserId { get; set; }
        public TechAndToolsUser User { get; set; }

        public int ProductId { get; set; }
        public ProductServiceModel Product { get; set; }

        public string Comment { get; set; }

        public double Rating { get; set; }
    }
}
