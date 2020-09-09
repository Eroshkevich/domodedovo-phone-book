using System.Collections.Generic;
using System.Threading.Tasks;
using Domodedovo.PhoneBook.Core.CQRS;
using Domodedovo.PhoneBook.Data.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Domodedovo.PhoneBook.WebAPI.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<UserDTO>>> GetAsync([FromQuery] GetUsersQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}