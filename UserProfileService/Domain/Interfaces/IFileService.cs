using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace UserProfileService.Domain.Interfaces
{
    public interface IFileService
    {
        Task<byte[]> Get(IFormFile formFile, CancellationToken cancellationToken = default);
    }
}
