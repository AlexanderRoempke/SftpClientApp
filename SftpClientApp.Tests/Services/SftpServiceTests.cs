using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using SftpClientApp.Database.Entities;
using SftpClientApp.Services;
using Xunit;

namespace SftpClientApp.Tests.Services
{
    public class SftpServiceTests
    {
        private Mock<ISftpClient> GetMockedSftpClient()
        {
            var mockSftpClient = new Mock<ISftpClient>();

            // Mock the ListFiles method to return a list of files
            mockSftpClient.Setup(client => client.ListFiles(It.IsAny<SftpTask>()))
                          .ReturnsAsync(() => new List<string> { "/source/testfile.txt" });

            return mockSftpClient;
        }


        private IConfigurationService GetMockedConfigurationService()
        {
            var mockConfigService = new Mock<IConfigurationService>();
            mockConfigService.Setup(s => s.GetSftpConfigurations())
                .ReturnsAsync(() => new List<SftpConfiguration>
                {
                    new SftpConfiguration
                    {
                        Id = 1,
                        Host = "example.com",
                        Port = 22,
                        Username = "username",
                        Password = "password",
                        IntervalInMinutes = 1,
                        DeleteAfterTransfer = true,
                        SftpTasks = new List<SftpTask>
                        {
                            new SftpTask
                            {
                                Id = 1,
                                SourcePath = "/source",
                                DestinationPath = "/destination",
                                FileFilterId = 1,
                                FileFilter = new FileFilter
                                {
                                    Id = 1,
                                    Name = "Test Filter",
                                    Pattern = "*.txt"
                                }
                            }
                        }
                    }
                });

            return mockConfigService.Object;
        }

        private ILoggingService GetMockedLoggingService()
        {
            var mockConfigService = new Mock<ILoggingService>();

            return mockConfigService.Object;
        }

        [Fact]
        public async Task ExecuteSftpTasks_HandlesSftpTask()
        {
            // Arrange
            var mockSftpClient = GetMockedSftpClient();
            var configurationService = GetMockedConfigurationService();
            var loggingService = GetMockedLoggingService();


            var sftpService = new SftpService(mockSftpClient.Object, configurationService, loggingService);

            // Act
            await sftpService.ExecuteSftpTasks();

            // Assert
            // Verify that the mocked SFTP methods have been called as expected.
            // The exact methods and call counts will depend on your implementation.
            mockSftpClient.Verify(client => client.Connect(It.IsAny<SftpConfiguration>()), Times.Once);
            mockSftpClient.Verify(client => client.ListFiles(It.IsAny<SftpTask>()), Times.Once);
            mockSftpClient.Verify(client => client.TransferFiles(It.IsAny<SftpTask>(), It.IsAny<IEnumerable<string>>()), Times.Once);
            mockSftpClient.Verify(client => client.DeleteFile(It.IsAny<SftpTask>(), It.IsAny<string>()), Times.Once);
            mockSftpClient.Verify(client => client.Disconnect(), Times.Once);
        }
    }
}
