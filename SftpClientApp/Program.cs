using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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

                    var configuration = hostContext.Configuration;
                    var connectionString = configuration.GetConnectionString("SftpConfigurationDB");

                    services.AddDbContext<AppDbContext>(options =>
                        options.UseSqlServer(connectionString)); 

                    var _workstationname = configuration.GetValue<string>("Workstationname");
                    services.AddScoped<IConfigurationService>(provider =>
                        new ConfigurationService(provider.GetRequiredService<AppDbContext>(), _workstationname));

                    services.AddScoped<ISftpClient, SftpClient>();
                    services.AddScoped<ISftpService, SftpService>();
                    services.AddScoped<ILoggingService, LoggingService>();
                    services.AddScoped<ICertificateService, CertificateService>();
                    services.AddScoped<IFileFilterService, FileFilterService>();
                });
    }
}
