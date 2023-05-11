using System.Threading.Tasks;
using SftpClientApp.Database.Entities;

namespace SftpClientApp.Services
{
    public interface ICertificateService
    {
        Task<Certificate> GetCertificateById(int certificateId);
    }
}