namespace TechAndTools.Data.Models
{
    using System;

    public class Article
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int TimesRead { get; set; }

        public string AuthorId { get; set; }
        public virtual TechAndToolsUser Author { get; set; }

        public virtual Image Image { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
