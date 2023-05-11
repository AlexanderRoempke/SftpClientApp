using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using SftpClientApp.Database.Entities;

namespace SftpClientApp.Services
{
    public class SftpClient : ISftpClient
    {
        private Renci.SshNet.SftpClient _sftpClient;

        public SftpClient()
        {
            _sftpClient = null;
        }

        public async Task Connect(SftpConfiguration configuration)
        {
            _sftpClient = new Renci.SshNet.SftpClient(configuration.Host, configuration.Port, configuration.Username, configuration.Password);
            await Task.Run(() => _sftpClient.Connect());
        }

        public async Task<IEnumerable<string>> ListFiles(SftpTask task)
        {
            return await Task.Run(() =>
            {
                var files = _sftpClient.ListDirectory(task.SourcePath);
                return files.Where(f => !f.IsDirectory).Select(f => f.FullName);
            });
        }

        public async Task TransferFiles(SftpTask task, IEnumerable<string> filesToTransfer)
        {
            await Task.Run(() =>
            {
                foreach (var filePath in filesToTransfer)
                {
                    using var sourceStream = _sftpClient.OpenRead(filePath);
                    var destinationPath = Path.Combine(task.DestinationPath, Path.GetFileName(filePath));
                    using var destinationStream = File.Create(destinationPath);
                    sourceStream.CopyTo(destinationStream);
                }
            });
        }

        public async Task DeleteFile(SftpTask task, string filePath)
        {
            await Task.Run(() => _sftpClient.DeleteFile(filePath));
        }

        public async Task Disconnect()
        {
            await Task.Run(() => _sftpClient.Disconnect());
        }
    }
}
