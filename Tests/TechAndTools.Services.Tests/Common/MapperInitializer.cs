namespace TechAndTools.Services.Tests.Common
{
    using Mapping;
    using Models;
    using System.Reflection;
    using TechAndTools.Data.Models;
    using Web.InputModels.Brands;
    using Web.ViewModels.Brands;

    public static class MapperInitializer
    {
        public static void InitializeMapper()
        {
            AutoMapperConfig.RegisterMappings(typeof(BrandIndexViewModel).GetTypeInfo().Assembly,
               typeof(BrandCreateInputModel).GetTypeInfo().Assembly,
               typeof(BrandServiceModel).GetTypeInfo().Assembly,
               typeof(Brand).GetTypeInfo().Assembly);
        }
    }
}
