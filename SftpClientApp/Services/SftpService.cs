﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using SftpClientApp.Database.Entities;

namespace SftpClientApp.Services
{
    public class SftpService : ISftpService
    {
        private readonly ISftpClient _sftpClient;
        private readonly IConfigurationService _configurationService;

        public SftpService(ISftpClient sftpClient, IConfigurationService configurationService)
        {
            _sftpClient = sftpClient;
            _configurationService = configurationService;
        }

        public async Task ExecuteSftpTasks()
        {
            var configurations = await _configurationService.GetSftpConfigurations();

            foreach (var configuration in configurations)
            {
                try
                {
                    await _sftpClient.Connect(configuration);

                    foreach (var task in configuration.SftpTasks)
                    {
                        var files = await _sftpClient.ListFiles(task);
                        if (task.FileFilter != null)
                        {
                            var filterPattern = task.FileFilter.Pattern;
                            files = files.Where(file => file.EndsWith(Path.GetExtension(filterPattern))).ToList();
                        }

                        await _sftpClient.TransferFiles(task, files);

                        if (configuration.DeleteAfterTransfer)
                        {
                            foreach (var file in files)
                            {
                                await _sftpClient.DeleteFile(task, file);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions, log errors, or retry if necessary
                }
                finally
                {
                    await _sftpClient.Disconnect();
                }
            }
        }

    }
}
