namespace TechAndTools.Services.Models
{
    using Data.Models;
    using Mapping;

    public class ImageServiceModel : IMapFrom<Image>, IMapTo<Image>
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public int? ProductId { get; set; }
        public ProductServiceModel Product { get; set; }

        public int? ArticleId { get; set; }
        public ArticleServiceModel Article { get; set; }
    }
}
