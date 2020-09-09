using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domodedovo.PhoneBook.Data;
using Domodedovo.PhoneBook.Data.Models.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domodedovo.PhoneBook.Core.CQRS
{
    public class CreateUsersCommandHandler : IRequestHandler<CreateUsersCommand>
    {
        private readonly IMapper _mapper;
        private readonly UsersDbContext _usersDbContext;
        private readonly ILogger<CreateUsersCommandHandler> _logger;

        public CreateUsersCommandHandler(UsersDbContext usersDbContext, IMapper mapper,
            ILogger<CreateUsersCommandHandler> logger)
        {
            _usersDbContext = usersDbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(CreateUsersCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Adding {request.Users.Count} Users...");

            var users = _mapper.Map<ICollection<User>>(request.Users);

            _usersDbContext.AddRange(users);

            await _usersDbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"{users.Count} Users added.");

            return Unit.Value;
        }
    }
}