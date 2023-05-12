using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SftpClientApp.Database;
using SftpClientApp.Database.Entities;
using SftpClientApp.Services;
using Xunit;

namespace SftpClientApp.Tests.Services
{
    public class LoggingServiceTests
    {
        private AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase_LoggingService")
                .Options;

            var dbContext = new AppDbContext(options);
            return dbContext;
        }

        [Fact]
        public async Task Log_CreatesLogEntry()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            var loggingService = new LoggingService(dbContext);
            var logLevel = new LogLevel { Id = 1, Name = Enums.LogLevels.Error.ToString() };
            dbContext.LogLevels.Add(logLevel);
            await dbContext.SaveChangesAsync();
#pragma warning disable CS8601 // Mögliche Nullverweiszuweisung.
            var configuration = new SftpConfiguration { Id = 3, Workstationname = "HansWurst", Host = "example.com", Username = "alex", Password = "1234", LogLevel = dbContext.LogLevels.FirstOrDefault() };
#pragma warning restore CS8601 // Mögliche Nullverweiszuweisung.
            dbContext.SftpConfigurations.Add(configuration);

            await dbContext.SaveChangesAsync();

            // Act
            await loggingService.Log("Test log entry", Enums.LogLevels.Fatal, configuration);

            // Assert
            var logEntries = dbContext.LogEntries.ToList();
            Assert.Single(logEntries);
            Assert.Equal("Test log entry", logEntries[0].Message);
            Assert.Equal(logLevel.Id, logEntries[0].LogLevelId);
        }
    }
}
