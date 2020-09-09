using System;
using System.Threading.Tasks;

namespace Domodedovo.PhoneBook.Core.Services
{
    public interface IBinaryLoadingService
    {
        Task<byte[]> LoadAsync(Uri url, bool throwOnExceptions = true);
    }
}