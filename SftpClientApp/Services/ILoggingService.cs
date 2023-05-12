using SftpClientApp.Database.Entities;
using SftpClientApp.Enums;

namespace SftpClientApp.Services
{
    public interface ILoggingService
    {
        Task Log(string message, LogLevels messageLogLevel, SftpConfiguration currentConfig);
    }
}