using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using UserProfileService.Data.Models;

namespace UserProfileService.Domain.Interfaces
{
    public interface IUserRepository : IRepository
    {
        Task<User> Add(User user, CancellationToken cancellationToken = default);

        User Update(User user);

        void Remove(User user);

        Task<User> GetBy(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken = default);

        Task<IEnumerable<User>> Get(int? take, int skip = 0, CancellationToken cancellationToken = default);
    }
}
