using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using UserProfileService.Data.Models;
using UserProfileService.Domain.Commands;
using UserProfileService.Domain.Interfaces;

namespace UserProfileService.Domain.CommandHandlers
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, User>
    {
        private readonly IUserRepository _repository;

        private readonly IFileService _fileService;

        private readonly ILogger<CreateUserHandler> _logger;

        private readonly IMapper _mapper;

        public UpdateUserHandler(IUserRepository repository, IFileService fileService, IMapper mapper, ILogger<CreateUserHandler> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userToUpdate = _mapper.Map<User>(request);

            userToUpdate.Avatar = await _fileService.Get(request.Avatar, cancellationToken);

            var user = _repository.Update(userToUpdate);

            if (user == null)
            {
                _logger.LogWarning($"User with id = {userToUpdate.Id} was not found.");
                return null;
            }

            await _repository.UnitOfWork.Complete(cancellationToken);

            _logger.LogInformation($"User with Id = {user.Id} was updated.");

            return user;
        }
    }
}
