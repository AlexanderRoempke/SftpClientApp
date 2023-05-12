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
        private readonly string _workstationname;

        public ConfigurationService(AppDbContext dbContext, string workstationname)
        {
            _dbContext = dbContext;
            _workstationname = workstationname;
        }

        public async Task<List<SftpConfiguration>> GetSftpConfigurations()
        {
            return await _dbContext.SftpConfigurations.Include(c => c.SftpTasks).Where(config => config.Workstationname == _workstationname).ToListAsync();
        }
    }
}