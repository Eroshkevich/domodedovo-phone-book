using System.Threading.Tasks;
using Domodedovo.PhoneBook.Core.CQRS;
using Domodedovo.PhoneBook.Integrations.RandomUser.CQRS;
using MediatR;

namespace Domodedovo.PhoneBook.UserLoader.Actions
{
    public class FetchUsersAction : IAppAction
    {
        private const ushort DefaultUsersCount = 1000;

        private readonly IMediator _mediator;

        public ushort Count { get; set; } = DefaultUsersCount;

        public FetchUsersAction(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Execute()
        {
            var getUsersQuery = new GetUsersQuery
            {
                Count = Count
            };

            var users = await _mediator.Send(getUsersQuery);

            var createUsersCommand = new CreateUsersCommand
            {
                Users = users
            };

            await _mediator.Send(createUsersCommand);
        }
    }
}