using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO.Auth;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Username
            };

            var result = await userManager.CreateAsync(identityUser, registerDto.Password);

            if (result.Succeeded)
            {
                if (registerDto.Roles != null && registerDto.Roles.Any())
                {
                    result = await userManager.AddToRolesAsync(identityUser, registerDto.Roles);

                    if (result.Succeeded)
                    {
                        return Ok("User registered successfully.");
                    }
                }
            }

            return BadRequest("User registration failed.");
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
        {
            var user = await userManager.FindByNameAsync(loginDto.Username);

            if (user != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginDto.Password);

                if (checkPasswordResult)
                {
                    var roles = await userManager.GetRolesAsync(user);

                    if (roles != null && roles.Any())
                    {
                        var token = tokenRepository.CreateJwtToken(user, roles.ToList());
                        
                        var response = new LoginResponseDto
                        {
                            JwtToken = token
                        };

                        return Ok(response);
                    }
                }
            }

            return Unauthorized("Invalid username or password.");
        }
    }
}
