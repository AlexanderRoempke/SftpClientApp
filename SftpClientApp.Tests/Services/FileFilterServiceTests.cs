using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SftpClientApp.Database;
using SftpClientApp.Database.Entities;
using SftpClientApp.Services;
using Xunit;

namespace SftpClientApp.Tests.Services
{
    public class FileFilterServiceTests
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
        public async Task GetFileFilterById_ReturnsFileFilter()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            var fileFilterService = new FileFilterService(dbContext);
            dbContext.FileFilters.Add(new FileFilter { Id = 1, Name = "Test Filter", Pattern = "*.txt" });
            await dbContext.SaveChangesAsync();

            // Act
            var fileFilter = await fileFilterService.GetFileFilterById(1);

            // Assert
            Assert.NotNull(fileFilter);
            Assert.Equal("Test Filter", fileFilter.Name);
            Assert.Equal("*.txt", fileFilter.Pattern);
        }
    }
}