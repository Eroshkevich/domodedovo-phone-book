using Domodedovo.PhoneBook.Core.Options;
using MediatR;

namespace Domodedovo.PhoneBook.UserLoader.Actions
{
    public class AppActionFactory : IAppActionFactory
    {
        private readonly IMediator _mediator;

        public AppActionFactory(IMediator mediator)
        {
            _mediator = mediator;
        }

        public FetchUsersAction CreateFetchUsersAction(ushort? count = null,
            PictureLoadingOptions pictureLoadingOptions = null)
        {
            var action = new FetchUsersAction(_mediator, pictureLoadingOptions);

            if (count.HasValue)
                action.Count = count.Value;

            return action;
        }
    }
}