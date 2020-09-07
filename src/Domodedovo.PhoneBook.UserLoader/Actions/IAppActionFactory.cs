namespace Domodedovo.PhoneBook.UserLoader.Actions
{
    public interface IAppActionFactory
    {
        FetchUsersAction CreateFetchUsersAction(ushort? count = null);
    }
}