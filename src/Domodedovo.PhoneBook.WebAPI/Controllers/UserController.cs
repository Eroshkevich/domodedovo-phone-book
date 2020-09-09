using System.Collections.Generic;
using System.Threading.Tasks;
using Domodedovo.PhoneBook.Core.CQRS;
using Domodedovo.PhoneBook.Data.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Domodedovo.PhoneBook.WebAPI.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get Users
        /// </summary>
        /// <param name="count">Count of Users</param>
        /// <param name="page">Number of Users page. From 0</param>
        /// <param name="sorting">
        /// Sorting expression.
        /// Format: 'sortingKey1' [desc]; 'sortingKey2' [desc]; ..'.
        /// Sort Users by 'sortingKey1', then by 'sortingKey2' etc.
        /// [desc] flag is optional for sort in descending order.
        /// Available orderingKeys: firstName; lastName; birthday;
        /// Example: 'firstName desc;lastName'
        /// </param>
        /// <returns>Users</returns>
        [HttpGet]
        public async Task<ActionResult<ICollection<UserDTO>>> GetAsync(
            [FromQuery] ushort? count,
            [FromQuery] ushort? page, [FromQuery] ICollection<SortingParameter<GetUsersQuerySortingKey>> sorting)
        {
            var query = new GetUsersQuery
            {
                Count = count,
                Page = page,
                Sorting = sorting
            };

            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}