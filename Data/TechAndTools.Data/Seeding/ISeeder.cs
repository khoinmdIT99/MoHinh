namespace TechAndTools.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    public interface ISeeder
    {
        Task SeedAsync(TechAndToolsDbContext dbContext, IServiceProvider serviceProvider);
    }
}
