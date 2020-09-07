using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domodedovo.PhoneBook.Data.DTO;
using Domodedovo.PhoneBook.Integrations.RandomUser.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domodedovo.PhoneBook.Integrations.RandomUser.CQRS
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, ICollection<UserDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IRandomUserClient _randomUserClient;
        private readonly ILogger<GetUsersQueryHandler> _logger;

        public GetUsersQueryHandler(IMapper mapper, IRandomUserClient randomUserClient,
            ILogger<GetUsersQueryHandler> logger)
        {
            _mapper = mapper;
            _randomUserClient = randomUserClient;
            _logger = logger;
        }

        public async Task<ICollection<UserDTO>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Getting {request.Count} Users...");

            var responseDTO = await _randomUserClient.GetUsers(request.Count);

            _logger.LogInformation($"{responseDTO.Results.Length} Users received.");

            return _mapper.Map<UserDTO[]>(responseDTO.Results);
        }
    }
}