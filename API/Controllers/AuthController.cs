using API.Model;
using API.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(LoginUser user)
        {
            if (await authService.RegisterUser(user))
            {
                return Ok("Successfuly done");
            }
            return BadRequest("Something Went Wrong");
            
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login (LoginUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (await authService.Login(user))
            {
                var tokenString= authService.GenerateTokenString(user);
                return Ok(tokenString);
            }
            return BadRequest();
        }
    }
}
