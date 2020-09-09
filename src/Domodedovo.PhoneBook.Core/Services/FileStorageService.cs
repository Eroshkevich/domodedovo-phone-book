using System;
using System.IO;
using System.Threading.Tasks;
using Domodedovo.PhoneBook.Core.Exceptions;
using Domodedovo.PhoneBook.Core.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Domodedovo.PhoneBook.Core.Services
{
    public class FileStorageService : IFileStorageService
    {
        private readonly FileStorageOptions _fileStorageOptions;
        private readonly ILogger<FileStorageService> _logger;

        public FileStorageService(IOptions<FileStorageOptions> fileStorageOptions, ILogger<FileStorageService> logger)
        {
            _logger = logger;
            _fileStorageOptions = fileStorageOptions.Value;
        }

        public async Task<string> SaveAsync(string path, byte[] byteArray, bool throwOnExceptions = true)
        {
            var filePath = Path.Combine(_fileStorageOptions.Path, path);

            _logger.LogInformation($"Saving file to path {filePath} ...");

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                await File.WriteAllBytesAsync(filePath, byteArray);
                return path;
            }
            catch (Exception e)
            {
                if (throwOnExceptions)
                    throw new FileStorageException(e);

                _logger.LogError($"Save file to path {path} Error.", e);
            }

            return null;
        }
    }
}