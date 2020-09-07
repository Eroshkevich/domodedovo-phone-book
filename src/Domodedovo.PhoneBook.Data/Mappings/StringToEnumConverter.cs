using System;
using AutoMapper;

namespace Domodedovo.PhoneBook.Data.Mappings
{
    public class StringToEnumConverter<T> : ITypeConverter<string, T>
        where T : struct, Enum
    {
        public T Convert(string source, T destination, ResolutionContext context) => Enum.Parse<T>(source);
    }
}