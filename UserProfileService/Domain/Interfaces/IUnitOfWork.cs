using System.Threading;
using System.Threading.Tasks;

namespace UserProfileService.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> Complete(CancellationToken cancellationToken = default);
    }
}
