using System.ComponentModel.DataAnnotations;

namespace SftpClientApp.Database.Entities
{
    public class SftpTask
    {
        [Key]
        public int Id { get; set; }
        public int SftpConfigurationId { get; set; }
        public SftpConfiguration SftpConfiguration { get; set; }
        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }
        public int? FileFilterId { get; set; }
        public FileFilter FileFilter { get; set; }
    }
}
