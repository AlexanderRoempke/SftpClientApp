using Microsoft.EntityFrameworkCore;
using SftpClientApp.Database.Entities;
using SftpClientApp.Enums;

namespace SftpClientApp.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<SftpConfiguration> SftpConfigurations { get; set; }
        public DbSet<LogLevel> LogLevels { get; set; }
        public DbSet<SftpTask> SftpTasks { get; set; }
        public DbSet<LogEntry> LogEntries { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<FileFilter> FileFilters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LogLevel>().HasData(
                Enum.GetValues(typeof(LogLevels))
                    .Cast<LogLevels>()
                    .Select(level => new LogLevel { Id = (int)level, Name = level.ToString() })
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}
