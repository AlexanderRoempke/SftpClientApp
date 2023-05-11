using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SftpClientApp.Database;
using SftpClientApp.Database.Entities;

namespace SftpClientApp.Services
{
    public class FileFilterService : IFileFilterService
    {
        private readonly AppDbContext _dbContext;

        public FileFilterService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<FileFilter> GetFileFilterById(int fileFilterId)
        {
            return await _dbContext.FileFilters.FindAsync(fileFilterId);
        }
    }
}
