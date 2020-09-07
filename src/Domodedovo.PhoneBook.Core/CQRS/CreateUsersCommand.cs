using System.Collections.Generic;
using Domodedovo.PhoneBook.Data.DTO;
using MediatR;

namespace Domodedovo.PhoneBook.Core.CQRS
{
    public class CreateUsersCommand : IRequest
    {
        public ICollection<UserDTO> Users { get; set; }
    }
}