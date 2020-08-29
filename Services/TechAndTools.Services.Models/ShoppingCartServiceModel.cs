namespace TechAndTools.Services.Models
{
    using Data.Models;
    using Mapping;

    using System.Collections.Generic;

    public class ShoppingCartServiceModel : IMapFrom<ShoppingCart>, IMapTo<ShoppingCart>
    {
        public int Id { get; set; }

        public TechAndToolsUser User { get; set; }

        public ICollection<ShoppingCartProductServiceModel> ShoppingCartProducts { get; set; }
    }
}
