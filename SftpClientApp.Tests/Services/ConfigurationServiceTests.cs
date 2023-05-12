using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using SftpClientApp.Database;
using SftpClientApp.Database.Entities;
using SftpClientApp.Services;
using Xunit;

namespace SftpClientApp.Tests.Services
{
    public class ConfigurationServiceTests
    {
        private AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase_ConfigurationServices")
                .Options;

            var dbContext = new AppDbContext(options);
            return dbContext;
        }

        [Fact]
        public async Task GetSftpConfigurations_ReturnsConfigurations()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            var configService = new ConfigurationService(dbContext, "HansWurst");
            dbContext.SftpConfigurations.Add(new SftpConfiguration { Id = 1, Workstationname = "HansWurst", Host = "example.com", Username = "alex", Password = "1234" });
            dbContext.SftpConfigurations.Add(new SftpConfiguration { Id = 2, Workstationname = "HansWurst", Host = "example2.com", Username = "thisaa", Password = "1i239821923ui!!22k2i8_123" });
            await dbContext.SaveChangesAsync();

            // Act
            var configs = await configService.GetSftpConfigurations();

            // Assert
            Assert.NotNull(configs);
            Assert.Equal(2, configs.Count);
            Assert.Equal("example.com", configs.First().Host);
            Assert.Equal("example2.com", configs.Last().Host);
        }
    }
}
