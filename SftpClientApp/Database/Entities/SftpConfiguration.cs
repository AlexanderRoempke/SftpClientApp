using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SftpClientApp.Database.Entities
{
    public class SftpConfiguration
    {
        [Key]
        public int Id { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int? CertificateId { get; set; }
        public Certificate Certificate { get; set; }
        public int IntervalInMinutes { get; set; }
        public bool DeleteAfterTransfer { get; set; }

        public ICollection<SftpTask> SftpTasks { get; set; }
    }
}
