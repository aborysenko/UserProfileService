using MediatR;
using UserProfileService.Data.Models;

namespace UserProfileService.Domain.Query
{
    public class GetUserByIdQuery : IRequest<User>
    {
        public int Id { get; set; }
    }
}
