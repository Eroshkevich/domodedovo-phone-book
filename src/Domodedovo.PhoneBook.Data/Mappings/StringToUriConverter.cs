using System;
using AutoMapper;

namespace Domodedovo.PhoneBook.Data.Mappings
{
    public class StringToUriConverter : ITypeConverter<string, Uri>
    {
        public Uri Convert(string source, Uri destination, ResolutionContext context) => new Uri(source);
    }
}