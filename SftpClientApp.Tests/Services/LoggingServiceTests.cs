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
                .UseInMemoryDatabase(databaseName: "TestDatabase")
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
            var logLevel = new LogLevel { Id = 1, Name = "Info" };
            dbContext.LogLevels.Add(logLevel);
            await dbContext.SaveChangesAsync();

            // Act
            await loggingService.Log(logLevel, "Test log entry");

            // Assert
            var logEntries = dbContext.LogEntries.ToList();
            Assert.Single(logEntries);
            Assert.Equal("Test log entry", logEntries[0].Message);
            Assert.Equal(logLevel.Id, logEntries[0].LogLevelId);
        }
    }
}
