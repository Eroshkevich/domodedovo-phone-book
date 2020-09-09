using System;
using System.Threading.Tasks;

namespace Domodedovo.PhoneBook.Core.Services
{
    public interface IImageLoadService
    {
        Task<byte[]> LoadAsync(Uri url, bool throwOnExceptions = true);
    }
}