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

        public FetchUsersAction CreateFetchUsersAction(ushort? count = null)
        {
            var action = new FetchUsersAction(_mediator);

            if (count.HasValue)
                action.Count = count.Value;

            return action;
        }
    }
}