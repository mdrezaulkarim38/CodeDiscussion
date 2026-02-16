using CodeDiscussion.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUserIdentity> _userManager;
    private readonly ITokenService _tokenService;
    public AuthController(UserManager<ApplicationUserIdentity> userManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(string username, string email, string password)
    {
        var user = new ApplicationUserIdentity
        {
            UserName = username,
            Email = email
        };
        var result = await _userManager.CreateAsync(user, password);
        if(!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
        return Ok("User created");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if(user == null)
        {
            return Unauthorized("Invalid credentials");
        }
        var valid = await _userManager.CheckPasswordAsync(user, password);
        if(!valid)
        {
            return Unauthorized("Invalid credentials");
        }
        var roles = await _userManager.GetRolesAsync(user);
        var token = _tokenService.GenerateToken(user, roles);
        return Ok(new { token });
    }

    [Authorize]
    [HttpGet("secure")]
    public IActionResult Secure()
    {
        return Ok("You are authenticated");
    }
}