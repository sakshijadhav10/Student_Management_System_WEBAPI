using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Student_Management_System_BLL.DTOS.Auth;
using Student_Management_System_BLL.Interfaces;

namespace Student_Management_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            var result = await _authService.RegisterAsync(dto);
            //if (result == "User already exists")
            //    return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var token = await _authService.LoginAsync(dto);

            //if (string.IsNullOrEmpty(token))
            //    return Unauthorized("Invalid email or password");

            return Ok(new { Token = token });
        }
    
}
}
