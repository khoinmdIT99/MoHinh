namespace TechAndTools.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    using System;
    using System.Collections.Generic;

    public class TechAndToolsUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public int ShoppingCartId { get; set; }
        public virtual ShoppingCart ShoppingCart { get; set; }

        public virtual ICollection<FavoriteProduct> FavoriteProducts { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}