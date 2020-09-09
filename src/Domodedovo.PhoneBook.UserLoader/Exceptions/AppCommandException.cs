using System;

namespace Domodedovo.PhoneBook.UserLoader.Exceptions
{
    public class AppCommandException : Exception
    {
        public AppCommandException(string command) : base(command)
        {
        }
    }
}