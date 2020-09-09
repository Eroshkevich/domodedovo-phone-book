using System.Collections.Generic;
using Domodedovo.PhoneBook.Data.DTO;
using MediatR;

namespace Domodedovo.PhoneBook.Core.CQRS
{
    public class GetUsersQuery : IRequest<ICollection<UserDTO>>
    {
        public ushort? Count { get; set; }
        public ushort? Page { get; set; }

        public ICollection<SortingParameter<GetUsersQuerySortingKey>> Sorting { get; set; }
    }
}