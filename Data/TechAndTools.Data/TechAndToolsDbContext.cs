namespace TechAndTools.Data
{
    using Models;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class TechAndToolsDbContext : IdentityDbContext<TechAndToolsUser>
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<FavoriteProduct> FavoriteProducts { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<PaymentStatus> PaymentStatuses { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartProduct> ShoppingCartProducts { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<MainCategory> MainCategories { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        
        public TechAndToolsDbContext(DbContextOptions<TechAndToolsDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<OrderProduct>()
                .HasKey(x => new { x.OrderId, x.ProductId });

            builder.Entity<FavoriteProduct>()
                .HasKey(x => new { x.UserId, x.ProductId });

            builder.Entity<ShoppingCartProduct>()
                .HasKey(x => new { x.ShoppingCartId, x.ProductId });

            builder.Entity<Product>()
                .HasMany(x => x.Images)
                .WithOne(x => x.Product)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Article>()
                .HasOne(x => x.Image)
                .WithOne(x => x.Article)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<MainCategory>()
                .HasMany(x => x.Categories)
                .WithOne(x => x.MainCategory)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Category>()
                .HasOne(x => x.MainCategory)
                .WithMany(x => x.Categories)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ShoppingCart>()
                .HasOne(x => x.User)
                .WithOne(x => x.ShoppingCart)
                .HasForeignKey<TechAndToolsUser>(x => x.ShoppingCartId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Brand>().HasMany(x => x.Products)
                .WithOne(x => x.Brand)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Supplier>().HasMany(x => x.Orders)
                .WithOne(x => x.Supplier)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
