namespace TechAndTools.Web.ViewModels.Images
{
    using Services.Mapping;
    using Services.Models;

    public class ImageViewModel : IMapFrom<ImageServiceModel>
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }
    }
}
