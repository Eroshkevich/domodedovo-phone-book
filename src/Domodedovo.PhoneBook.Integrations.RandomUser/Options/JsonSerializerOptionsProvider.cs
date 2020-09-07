using System.Text.Json;

namespace Domodedovo.PhoneBook.Integrations.RandomUser.Options
{
    public class JsonSerializerOptionsProvider : IJsonSerializerOptionsProvider
    {
        public JsonSerializerOptions GetOptions()
        {
            return new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }
    }
}