using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using Domodedovo.PhoneBook.Integrations.RandomUser.DTO;
using Domodedovo.PhoneBook.Integrations.RandomUser.Exceptions;
using Domodedovo.PhoneBook.Integrations.RandomUser.Options;

namespace Domodedovo.PhoneBook.Integrations.RandomUser.Services
{
    public class RandomUserClient : IRandomUserClient
    {
        private const ushort DefaultUsersCount = 1000;

        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public RandomUserClient(HttpClient httpClient, IJsonSerializerOptionsProvider jsonSerializerOptionsProvider)
        {
            _httpClient = httpClient;
            _jsonSerializerOptions = jsonSerializerOptionsProvider.GetOptions();
        }

        public async Task<ResponseDTO> GetUsers(ushort? count = null)
        {
            var usersCount = count ?? DefaultUsersCount;

            var query = HttpUtility.ParseQueryString(string.Empty);
            query["results"] = usersCount.ToString();

            var response = await _httpClient.GetAsync($"?{query}");

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new RandomUserApiException(e);
            }

            var responseBody = await response.Content.ReadAsStringAsync();

            var responseDTO = JsonSerializer.Deserialize<ResponseDTO>(responseBody, _jsonSerializerOptions);

            return responseDTO;
        }
    }
}