namespace TechAndTools.Data.Models
{
    public class Review
    {
        public string Id { get; set; }

        public string UserId { get; set; }
        public virtual TechAndToolsUser User { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public string Comment { get; set; }

        public double Rating { get; set; }
    }
}
