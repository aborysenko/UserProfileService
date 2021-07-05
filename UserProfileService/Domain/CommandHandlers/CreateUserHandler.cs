using System;
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
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, User>
    {
        private readonly IUserRepository _repository;

        private readonly IFileService _fileService;

        private readonly ILogger<CreateUserHandler> _logger;

        private readonly IMapper _mapper;

        public CreateUserHandler(IUserRepository repository, IFileService fileService, IMapper mapper, ILogger<CreateUserHandler> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userToAdd = _mapper.Map<User>(request);

            userToAdd.Avatar = await _fileService.Get(request.Avatar, cancellationToken);

            var user = await _repository.Add(userToAdd, cancellationToken);

            await _repository.UnitOfWork.Complete(cancellationToken);

            _logger.LogInformation($"User with Id = {user.Id} was created.");

            return user;
        }
    }
}
