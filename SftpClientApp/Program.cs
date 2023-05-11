using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SftpClientApp.Database;
using SftpClientApp.Services;

namespace SftpClientApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            var sftpService = host.Services.GetRequiredService<ISftpService>();
            sftpService.ExecuteSftpTasks().GetAwaiter().GetResult();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDbContext<AppDbContext>();
                    services.AddScoped<ISftpService, SftpService>();
                    services.AddScoped<ILoggingService, LoggingService>();
                    services.AddScoped<IConfigurationService, ConfigurationService>();
                    services.AddScoped<ICertificateService, CertificateService>();
                    services.AddScoped<IFileFilterService, FileFilterService>();
                });
    }
}
