using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SftpClientApp.Database.Entities
{
    public class LogLevel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<LogEntry> LogEntries { get; set; }
    }
}
