using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;

namespace GarageSpace.NotificationService.Templates.Extensions
{
    public class WebHostEnvironment : IWebHostEnvironment
    {
        public string EnvironmentName { get; set; } = "Development";
        public string ApplicationName { get; set; } = "GarageSpace.NotificationService";
        public string WebRootPath { get; set; } = Directory.GetCurrentDirectory();
        public IFileProvider WebRootFileProvider { get; set; }
            = new PhysicalFileProvider(Directory.GetCurrentDirectory());

        public string ContentRootPath { get; set; } = Directory.GetCurrentDirectory();
        public IFileProvider ContentRootFileProvider { get; set; }
            = new PhysicalFileProvider(Directory.GetCurrentDirectory());
    }
}
