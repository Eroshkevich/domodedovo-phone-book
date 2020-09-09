using System;
using System.Collections.Generic;

namespace Domodedovo.PhoneBook.Data.DTO
{
    public class UserDTO
    {
        public NameDTO Name { get; set; }
        public DateTime? Birthday { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<PictureDTO> Pictures { get; set; }
    }
}