using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Domodedovo.PhoneBook.WebAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// Authenticate with GitHub
        /// </summary>
        /// <returns>Hello there</returns>
        [HttpGet]
        public IActionResult Auth()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Content($"Hello {User.Identity.Name}");
            }

            return Challenge(new AuthenticationProperties {RedirectUri = "/api/auth"});
        }
    }
}