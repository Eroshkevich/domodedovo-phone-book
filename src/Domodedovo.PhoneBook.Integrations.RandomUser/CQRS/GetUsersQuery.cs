using System.Collections.Generic;
using Domodedovo.PhoneBook.Data.DTO;
using MediatR;

namespace Domodedovo.PhoneBook.Integrations.RandomUser.CQRS
{
    public class GetUsersQuery : IRequest<ICollection<UserDTO>>
    {
        public ushort? Count { get; set; }
    }
}