using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using UserProfileService.Data.Models;
using UserProfileService.Domain.Interfaces;
using UserProfileService.Domain.Query;

namespace UserProfileService.Domain.CommandHandlers
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, User>
    {
        private readonly IUserRepository _repository;

        private readonly ILogger<GetUserByIdHandler> _logger;

        public GetUserByIdHandler(IUserRepository repository, ILogger<GetUserByIdHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = _repository.GetBy(x => x.Id == request.Id, cancellationToken);

            if (user == null)
            {
                _logger.LogWarning($"User with id = {request.Id} was not found.");

                return null;
            }

            return user;
        }
    }
}
