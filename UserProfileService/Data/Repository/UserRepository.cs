using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserProfileService.Data.Models;
using UserProfileService.Domain.Interfaces;

namespace UserProfileService.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserProfileDbContext _context;

        private DbSet<User> DbSet => _context.Set<User>();

        public IUnitOfWork UnitOfWork => _context;

        public UserRepository(UserProfileDbContext context)
        {
            _context = context;
        }

        public async Task<User> Add(User user, CancellationToken cancellationToken = default)
        {
            var entry = await DbSet.AddAsync(user, cancellationToken);

            return entry.Entity;
        }

        public User Update(User user)
        {
            if (!DbSet.Any(x => x.Id == user.Id))
            {
                return null;
            }
            
            _context.Entry(user).State = EntityState.Modified;

            return user;

        }

        public void Remove(User user)
        {
            DbSet.Remove(user);
        }

        public async Task<User> GetBy(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(predicate, cancellationToken);
        }


        public async Task<IEnumerable<User>> Get(int? take, int skip = 0, CancellationToken cancellationToken = default)
        {
            if (take <= 0)
            {
                throw new ArgumentException($"{nameof(take)} should be > 0.", nameof(take));
            }

            if (skip < 0)
            {
                throw new ArgumentException($"{nameof(skip)} should be > 0.", nameof(take));
            }

            var query = DbSet.AsNoTracking().Skip(skip);

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return await query.ToListAsync(cancellationToken);
        }
    
    }
}
