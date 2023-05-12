using System;
using System.Threading.Tasks;
using SftpClientApp.Database;
using SftpClientApp.Database.Entities;
using SftpClientApp.Enums;

namespace SftpClientApp.Services
{
    public class LoggingService : ILoggingService
    {
        private readonly AppDbContext _dbContext;

        public LoggingService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Log(string message, LogLevels messageLogLevel, SftpConfiguration currentConfig)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (!shouldIWriteALog(currentConfig.LogLevel, messageLogLevel)) return;
            var logEntry = new LogEntry
            {
                Message = message,
                Timestamp = DateTime.UtcNow,
                SftpConfigurationId = currentConfig.Id,
                SftpConfiguration = currentConfig,
                LogLevel = currentConfig.LogLevel
            };

            _dbContext.LogEntries.Add(logEntry);
            await _dbContext.SaveChangesAsync();
        }

        private bool shouldIWriteALog(LogLevel assignedLogLevel, LogLevels messageLoglevel)
        {
            Enum.TryParse<LogLevels>(assignedLogLevel.Name, out var level);

            return level != null && level <= messageLoglevel;
        }
    }
}