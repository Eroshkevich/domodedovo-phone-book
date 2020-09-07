using Domodedovo.PhoneBook.Data.Models.Entities;

namespace Domodedovo.PhoneBook.Data.Models.Links
{
    public class UserPicture
    {
        public int UserId { get; set; }
        public int PictureId { get; set; }
        
        public User User { get; set; }
        public Picture Picture { get; set; }
    }
}