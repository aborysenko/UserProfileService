using System.Collections.Generic;
using MediatR;
using UserProfileService.Data.Models;

namespace UserProfileService.Domain.Query
{
    public class GetUsersQuery : IRequest<IEnumerable<User>>
    {
        public int? PageSize { get; set; }

        public int CurrentPage { get; set; }
    }
}
