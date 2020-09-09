using System;

namespace Domodedovo.PhoneBook.Core.Exceptions
{
    public class ImageLoadException : Exception
    {
        private const string DefaultMessage = "Image loading Error.";

        public ImageLoadException(Exception innerException) : base(DefaultMessage, innerException)
        {
        }
    }
}