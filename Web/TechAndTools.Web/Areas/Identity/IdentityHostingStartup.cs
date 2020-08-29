using Microsoft.AspNetCore.Hosting;
[assembly: HostingStartup(typeof(TechAndTools.Web.Areas.Identity.IdentityHostingStartup))]

namespace TechAndTools.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}