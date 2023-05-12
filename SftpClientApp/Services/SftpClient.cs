using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using SftpClientApp.Database.Entities;

namespace SftpClientApp.Services
{
    public class SftpClient : ISftpClient
    {
        private Renci.SshNet.SftpClient _sftpClient;
        private SftpConfiguration _sftpConfiguration;
        private ILoggingService _loggingService;
        public SftpClient(ILoggingService loggingService)
        {
            _sftpClient = null;
            _loggingService = loggingService;
        }

        public async Task Connect(SftpConfiguration configuration)
        {
            _sftpConfiguration = configuration;

            var methods = new AuthenticationMethod[2];
            methods[0] = new PasswordAuthenticationMethod(configuration.Username, configuration.Password);
            methods[1] = new PrivateKeyAuthenticationMethod(configuration.Username, new PrivateKeyFile(configuration.Certificate.CertificateData));
            _sftpClient = new Renci.SshNet.SftpClient(new ConnectionInfo(configuration.Host, configuration.Port, configuration.Username, methods));

            await Task.Run(() => _sftpClient.Connect());

            var host = Dns.GetHostEntry(Dns.GetHostName());
            var ipAddress = host.AddressList.FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);

            _loggingService.Log($"Client Connected with IP Address: {ipAddress} to Host : {configuration.Host}", Enums.LogLevels.Info, configuration).Start();
        }

        public async Task<IEnumerable<string>> ListFiles(SftpTask task)
        {
            return await Task.Run(() =>
            {
                var files = _sftpClient.ListDirectory(task.SourcePath);
                _loggingService.Log($"ListFiles {string.Join(" ;\n",files.Select(x=>x.Name))}", Enums.LogLevels.Info, _sftpConfiguration).Start();
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
                    _loggingService.Log($"File created from {filePath} to {destinationPath}", Enums.LogLevels.Info, _sftpConfiguration).Start();

                }
            });
        }

        public async Task DeleteFile(SftpTask task, string filePath)
        {
            File.Delete(filePath);
            _loggingService.Log($"File localy deleted from {filePath} after beeing uploaded", Enums.LogLevels.Warn, _sftpConfiguration).Start();

        }

        public async Task Disconnect()
        {
            await Task.Run(() => _sftpClient.Disconnect());
            _loggingService.Log($"Disconected", Enums.LogLevels.Info, _sftpConfiguration).Start();
        }
    }
}
