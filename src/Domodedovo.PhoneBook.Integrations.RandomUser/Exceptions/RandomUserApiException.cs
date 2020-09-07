using System;

namespace Domodedovo.PhoneBook.Integrations.RandomUser.Exceptions
{
    public class RandomUserApiException : Exception
    {
        public RandomUserApiException(Exception e) : base("RandomUser API response error.",
            e)
        {
        }
    }
}