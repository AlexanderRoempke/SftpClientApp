using SftpClientApp.Database.Entities;

namespace SftpClientApp.Services
{
    public interface IConfigurationService
    {
        Task<List<SftpConfiguration>> GetSftpConfigurations();
    }
}