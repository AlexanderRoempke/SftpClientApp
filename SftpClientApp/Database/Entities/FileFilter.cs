using System.ComponentModel.DataAnnotations;

namespace SftpClientApp.Database.Entities
{
    public class FileFilter
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Pattern { get; set; }
    }
}
