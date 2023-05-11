using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SftpClientApp.Database;
using SftpClientApp.Database.Entities;

namespace SftpClientApp.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly AppDbContext _dbContext;

        public ConfigurationService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<SftpConfiguration>> GetSftpConfigurations()
        {
            return await _dbContext.SftpConfigurations.Include(c => c.SftpTasks).ToListAsync();
        }
    }
}