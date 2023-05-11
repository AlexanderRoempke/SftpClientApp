using System.Threading.Tasks;
using SftpClientApp.Database.Entities;

namespace SftpClientApp.Services
{
    public interface ILoggingService
    {
        Task Log(LogLevel logLevel, string message, SftpConfiguration sftpConfiguration = null);
    }
}
