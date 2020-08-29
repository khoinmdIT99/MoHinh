namespace TechAndTools.Data.Models
{
    public class FavoriteProduct
    {
        public string UserId { get; set; }
        public virtual TechAndToolsUser User { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
