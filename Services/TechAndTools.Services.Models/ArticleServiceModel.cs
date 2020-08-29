namespace TechAndTools.Services.Models
{
    using Mapping;
    using TechAndTools.Data.Models;
    
    using System;

    public class ArticleServiceModel : IMapFrom<Article>, IMapTo<Article>
    {
        
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
        public int TimesRead { get; set; }

        public string AuthorId { get; set; }
        public TechAndToolsUser Author { get; set; }

        public ImageServiceModel Image { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
