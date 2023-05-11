using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SftpClientApp.Database;
using SftpClientApp.Database.Entities;

namespace SftpClientApp.Services
{
    public class CertificateService : ICertificateService
    {
        private readonly AppDbContext _dbContext;

        public CertificateService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Certificate> GetCertificateById(int certificateId)
        {
            return await _dbContext.Certificates.FindAsync(certificateId);
        }
    }
}
