using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;

namespace GarageSpace.NotificationService.IntegrationTests.Infrastructure
{
    public sealed class TestWebHostEnvironment : IWebHostEnvironment
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
