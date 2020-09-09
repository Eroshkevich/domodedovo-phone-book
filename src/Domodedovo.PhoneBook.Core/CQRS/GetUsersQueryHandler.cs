using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domodedovo.PhoneBook.Data;
using Domodedovo.PhoneBook.Data.DTO;
using Domodedovo.PhoneBook.Data.Extensions;
using Domodedovo.PhoneBook.Data.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domodedovo.PhoneBook.Core.CQRS
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, ICollection<UserDTO>>
    {
        private const ushort DefaultUsersCount = 10;

        private readonly UsersDbContext _usersDbContext;
        private readonly IMapper _mapper;

        public GetUsersQueryHandler(UsersDbContext usersDbContext, IMapper mapper)
        {
            _usersDbContext = usersDbContext;
            _mapper = mapper;
        }

        public async Task<ICollection<UserDTO>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            IQueryable<User> usersQuery = _usersDbContext.Set<User>()
                .Include(e => e.Pictures)
                .ThenInclude(e => e.Picture);

            if (request.Sorting != null)
            {
                var first = true;
                foreach (var sortingParameter in request.Sorting)
                {
                    usersQuery = sortingParameter.SortingKey switch
                    {
                        GetUsersQuerySortingKey.FirstName => usersQuery.SortBy(e => e.Name.First, first,
                            sortingParameter.IsDescending),
                        GetUsersQuerySortingKey.LastName => usersQuery.SortBy(e => e.Name.Last, first,
                            sortingParameter.IsDescending),
                        GetUsersQuerySortingKey.Birthday => usersQuery.SortBy(e => e.Birthday, first,
                            sortingParameter.IsDescending),
                        _ => throw new ArgumentOutOfRangeException()
                    };

                    first = false;
                }
            }

            if (request.Page.HasValue)
            {
                var usersCount = request.Count ?? DefaultUsersCount;

                usersQuery = usersQuery.Skip(request.Page.Value * usersCount);
            }

            if (request.Count.HasValue)
                usersQuery = usersQuery.Take(request.Count.Value);

            var users = await usersQuery.ToListAsync(cancellationToken);

            return _mapper.Map<ICollection<UserDTO>>(users);
        }
    }
}