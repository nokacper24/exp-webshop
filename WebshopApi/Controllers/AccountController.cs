using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebshopApi.Models;


namespace WebshopApi.Controllers
{
    [Route("account")]
    [Authorize]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;

        public AccountController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet("me")]
        public async Task<ActionResult> Me()
        {
            var userId = User.Claims
            .First(
                    x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
                )
                .Value;
            var user = await this.userManager.FindByIdAsync(userId);
            return Ok(user);
        }

    }
}
