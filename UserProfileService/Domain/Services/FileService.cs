using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using UserProfileService.Domain.Interfaces;

namespace UserProfileService.Domain.Services
{
    public class FileService : IFileService
    {
        private readonly ILogger<FileService> _logger;

        public FileService(ILogger<FileService> logger)
        {
            _logger = logger;
        }

        public async Task<byte[]> Get(IFormFile formFile, CancellationToken cancellationToken = default)
        {
            try
            {
                if (formFile == null)
                {
                    return null;
                }

                await using var stream = new MemoryStream();
                await formFile.CopyToAsync(stream, cancellationToken);
                return stream.ToArray();
            }
            catch (Exception e)
            {
               _logger.LogError(e, "Couldn't read data from IFormFile.");
            }

            return null;
        }
    }
}
