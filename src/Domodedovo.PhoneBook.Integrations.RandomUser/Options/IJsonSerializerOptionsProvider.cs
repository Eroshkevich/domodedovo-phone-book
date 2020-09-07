using System.Text.Json;

namespace Domodedovo.PhoneBook.Integrations.RandomUser.Options
{
    public interface IJsonSerializerOptionsProvider
    {
        JsonSerializerOptions GetOptions();
    }
}