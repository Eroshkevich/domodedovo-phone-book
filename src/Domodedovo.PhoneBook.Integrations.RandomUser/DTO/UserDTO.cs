using System.Text.Json.Serialization;

namespace Domodedovo.PhoneBook.Integrations.RandomUser.DTO
{
    public class UserDTO
    {
        public NameDTO Name { get; set; }
        [JsonPropertyName("dob")] public DateOfBirthDTO DateOfBirth { get; set; }
        [JsonPropertyName("phone")] public string PhoneNumber { get; set; }
        public PictureDTO Picture { get; set; }
    }
}