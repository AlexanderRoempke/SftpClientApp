using System.Threading.Tasks;
using SftpClientApp.Database.Entities;

namespace SftpClientApp.Services
{
    public interface IFileFilterService
    {
        Task<FileFilter> GetFileFilterById(int fileFilterId);
    }
}