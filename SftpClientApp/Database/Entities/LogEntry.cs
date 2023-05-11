using System;
using System.ComponentModel.DataAnnotations;

namespace SftpClientApp.Database.Entities
{
    public class LogEntry
    {
        [Key]
        public int Id { get; set; }
        public int LogLevelId { get; set; }
        public LogLevel LogLevel { get; set; }
        public DateTime Timestamp { get; set; }
        public string Message { get; set; }
        public int? SftpConfigurationId { get; set; }
        public SftpConfiguration SftpConfiguration { get; set; }
    }
}
