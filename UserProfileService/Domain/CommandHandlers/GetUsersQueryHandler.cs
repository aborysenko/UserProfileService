using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using UserProfileService.Data.Models;
using UserProfileService.Domain.Interfaces;
using UserProfileService.Domain.Query;

namespace UserProfileService.Domain.CommandHandlers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<User>>
    {
        private readonly IUserRepository _repository;

        private readonly ILogger<GetUsersQueryHandler> _logger;

        public GetUsersQueryHandler(IUserRepository repository, ILogger<GetUsersQueryHandler> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = 
                (await _repository.Get(request.PageSize, request.CurrentPage, cancellationToken))
                .ToList();

            _logger.LogInformation($"Get {users.Count} users.");

            return users;
        }
    }
}
