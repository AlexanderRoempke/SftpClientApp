using System.Collections.Generic;
using System.Threading.Tasks;
using SftpClientApp.Database.Entities;

namespace SftpClientApp.Services
{
    public interface ISftpClient
    {
        Task Connect(SftpConfiguration configuration);
        Task<IEnumerable<string>> ListFiles(SftpTask task);
        Task TransferFiles(SftpTask task, IEnumerable<string> filesToTransfer);
        Task DeleteFile(SftpTask task, string filePath);
        Task Disconnect();
    }
}
