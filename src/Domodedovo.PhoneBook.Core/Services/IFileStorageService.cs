using System.Threading.Tasks;

namespace Domodedovo.PhoneBook.Core.Services
{
    public interface IFileStorageService
    {
        Task<string> SaveAsync(string path, byte[] byteArray, bool throwOnExceptions = true);
    }
}