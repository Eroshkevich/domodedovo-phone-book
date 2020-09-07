using Domodedovo.PhoneBook.UserLoader.Actions;

namespace Domodedovo.PhoneBook.UserLoader.Services
{
    public interface IAppActionService
    {
        public IAppAction GetAppCommand();
    }
}