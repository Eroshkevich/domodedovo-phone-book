namespace Domodedovo.PhoneBook.Core.Options
{
    public class PictureLoadingOptions
    {
        public bool StoreInDatabaseEnabled { get; set; }
        public bool StoreInFileSystemEnabled { get; set; }
        public bool ThrowOnExceptions { get; set; } = true;
    }
}