using System.Threading.Tasks;
using Domodedovo.PhoneBook.Core.CQRS;
using Domodedovo.PhoneBook.Core.Options;
using MediatR;

namespace Domodedovo.PhoneBook.UserLoader.Actions
{
    public class FetchUsersAction : IAppAction
    {
        private readonly IMediator _mediator;
        private readonly PictureLoadingOptions _pictureLoadingOptions;

        public ushort? Count { get; set; }

        public FetchUsersAction(IMediator mediator, PictureLoadingOptions pictureLoadingOptions)
        {
            _mediator = mediator;
            _pictureLoadingOptions = pictureLoadingOptions;
        }

        public async Task ExecuteAsync()
        {
            var getUsersQuery = new Integrations.RandomUser.CQRS.GetUsersQuery
            {
                Count = Count
            };

            var users = await _mediator.Send(getUsersQuery);

            var createUsersCommand = new CreateUsersCommand
            {
                Users = users,
                PictureLoadingOptions = _pictureLoadingOptions
            };

            await _mediator.Send(createUsersCommand);
        }
    }
}