using System;
using System.Collections.Generic;
using Domodedovo.PhoneBook.Data.Models.Links;
using Domodedovo.PhoneBook.Data.Models.ValueTypes;

namespace Domodedovo.PhoneBook.Data.Models.Entities
{
    //TODO: Think about accessibility
    public class User : EntityBase
    {
        public Name Name { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? Birthday { get; set; }

        public ICollection<UserPicture> Pictures { get; set; }
    }
}