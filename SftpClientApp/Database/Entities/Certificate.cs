using System.ComponentModel.DataAnnotations;

namespace SftpClientApp.Database.Entities
{
    public class Certificate
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string CertificateData { get; set; }
    }
}
