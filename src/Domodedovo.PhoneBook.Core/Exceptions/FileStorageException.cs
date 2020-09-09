using System;

namespace Domodedovo.PhoneBook.Core.Exceptions
{
    public class FileStorageException : Exception
    {
        private const string DefaultMessage = "File storage Error.";

        public FileStorageException(Exception innerException) : base(DefaultMessage, innerException)
        {
        }
    }
}