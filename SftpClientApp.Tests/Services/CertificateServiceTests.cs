using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SftpClientApp.Database;
using SftpClientApp.Database.Entities;
using SftpClientApp.Services;
using Xunit;

namespace SftpClientApp.Tests.Services
{
    public class CertificateServiceTests
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
        public async Task GetCertificateById_ReturnsCertificate()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            var certificateService = new CertificateService(dbContext);
            dbContext.Certificates.Add(new Certificate { Id = 1, Name = "Test Certificate", CertificateData = "Test Data" });
            await dbContext.SaveChangesAsync();

            // Act
            var certificate = await certificateService.GetCertificateById(1);

            // Assert
            Assert.NotNull(certificate);
            Assert.Equal("Test Certificate", certificate.Name);
            Assert.Equal("Test Data", certificate.CertificateData);
        }
    }
}
