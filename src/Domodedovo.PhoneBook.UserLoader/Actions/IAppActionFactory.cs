using Domodedovo.PhoneBook.Core.Options;

namespace Domodedovo.PhoneBook.UserLoader.Actions
{
    public interface IAppActionFactory
    {
        FetchUsersAction CreateFetchUsersAction(ushort? count = null,
            PictureLoadingOptions pictureLoadingOptions = null);
    }
}