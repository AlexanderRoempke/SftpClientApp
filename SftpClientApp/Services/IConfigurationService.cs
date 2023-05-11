using System.Collections.Generic;
using System.Threading.Tasks;
using SftpClientApp.Database.Entities;

namespace SftpClientApp.Services
{
    public interface IConfigurationService
    {
        Task<List<SftpConfiguration>> GetSftpConfigurations();
    }
}
