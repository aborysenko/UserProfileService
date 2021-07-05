using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserProfileService.Data.Models;
using UserProfileService.Domain.Interfaces;

namespace UserProfileService.Data
{
    public class UserProfileDbContext : DbContext, IUnitOfWork
    {
        public UserProfileDbContext(DbContextOptions options)
            :base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        
        public async Task<int> Complete(CancellationToken cancellationToken = default)
        {
            return await SaveChangesAsync(cancellationToken);
        }
    }
}
