namespace TechAndTools.Web
{
    using CloudinaryDotNet;
    using Data;
    using Data.Models;
    using Data.Seeding;
    using InputModels.Brands;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Services;
    using Services.Contracts;
    using Services.Mapping;
    using Services.Messaging;
    using Services.Models;
    using Services.Upload;
    using System;
    using System.Reflection;
    using ViewModels;
    using ViewModels.Brands;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<TechAndToolsDbContext>(options =>
            {
                options.UseSqlServer("data source=DESKTOP-UML28IP;initial catalog=WebsiteTB;integrated security=True;MultipleActiveResultSets=True;");
                options.UseLazyLoadingProxies();
            });

            services.AddIdentity<TechAndToolsUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
                .AddDefaultTokenProviders()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<TechAndToolsDbContext>();

            Account cloudinaryCredentials = new Account(
                this.configuration.GetSection("Cloudinary:CloudName").Value,
                this.configuration.GetSection("Cloudinary:ApiKey").Value,
                this.configuration.GetSection("Cloudinary:ApiSecret").Value);

            Cloudinary cloudinaryUtility = new Cloudinary(cloudinaryCredentials);

            services.AddSingleton(cloudinaryUtility);

            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.IdleTimeout = new TimeSpan(0, 4, 0, 0);
            });

            services.AddTransient<IBrandService, BrandService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IMainCategoryService, MainCategoryService>();
            services.AddTransient<ISupplierService, SupplierService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<IShoppingCartService, ShoppingCartService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAddressService, AddressService>();
            services.AddTransient<IPaymentMethodService, PaymentMethodService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IArticleService, ArticleService>();
            services.AddTransient<IContactService, ContactService>();

            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddTransient<ICloudinaryService, CloudinaryService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly,
                typeof(BrandIndexViewModel).GetTypeInfo().Assembly,
                typeof(BrandCreateInputModel).GetTypeInfo().Assembly,
                typeof(BrandServiceModel).GetTypeInfo().Assembly);

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<TechAndToolsDbContext>();

                if (env.IsDevelopment())
                {
                    dbContext.Database.Migrate();
                }

                new TechAndToolsDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseSession();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
