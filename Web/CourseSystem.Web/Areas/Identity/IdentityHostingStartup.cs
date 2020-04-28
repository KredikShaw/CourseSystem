using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(CourseSystem.Web.Areas.Identity.IdentityHostingStartup))]

namespace CourseSystem.Web.Areas.Identity
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