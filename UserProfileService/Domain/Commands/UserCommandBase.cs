using MediatR;
using Microsoft.AspNetCore.Http;
using UserProfileService.Data.Models;

namespace UserProfileService.Domain.Commands
{
    public abstract class UserCommandBase : IRequest<User>
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public IFormFile Avatar { get; set; }
    }
}
