using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using To_Do_List_API.Functions.User;

namespace To_Do_List_API.Controllers.Auth;

[ApiController]
[Route("api/[controller]")]
public class AuthC(IUserF userF) : ControllerBase
{
    private readonly IUserF _userF;
    
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthREQ dto)
    {
        //var userIp = HttpContext.Connection.RemoteIpAddress?.ToString();

        var auth = await _userF.Authenticate(dto.Email, dto.Password);
        if (auth == null) return Unauthorized();
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("Jwt:Key");
    
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] {
                new Claim("id", auth.Id.ToString()), // attach user id
                new Claim(ClaimTypes.Email, dto.Email)
            }),
            Expires = DateTime.UtcNow.AddMinutes(30),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwt = tokenHandler.WriteToken(token);
        
        auth.Token = jwt;
        
        return Ok(new { auth });
    }
}