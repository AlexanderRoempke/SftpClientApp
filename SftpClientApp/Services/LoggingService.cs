using System;
using System.Threading.Tasks;
using SftpClientApp.Database;
using SftpClientApp.Database.Entities;

namespace SftpClientApp.Services
{
    public class LoggingService : ILoggingService
    {
        private readonly AppDbContext _dbContext;

        public LoggingService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Log(LogLevel logLevel, string message, SftpConfiguration sftpConfiguration = null)
        {
            var logEntry = new LogEntry
            {
                LogLevelId = logLevel.Id,
                LogLevel = logLevel,
                Timestamp = DateTime.UtcNow,
                Message = message,
                SftpConfigurationId = sftpConfiguration?.Id,
                SftpConfiguration = sftpConfiguration
            };

            _dbContext.LogEntries.Add(logEntry);
            await _dbContext.SaveChangesAsync();
        }
    }
}