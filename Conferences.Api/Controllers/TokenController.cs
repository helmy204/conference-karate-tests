using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Conferences.Api.DAL;
using Conferences.Api.Entities;

namespace Conferences.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    public IConfiguration _configuration;
    private readonly ConferenceContext _context;

    public TokenController(IConfiguration configuration, ConferenceContext context)
    {
        _configuration = configuration ??
            throw new ArgumentNullException(nameof(configuration));

        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Post(User userModel)
    {
        var user = await ValidateUserCredentialsAsync(userModel.Username,userModel.Password);

        if(user == null)
            return Unauthorized();

        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, user.Username),
            new Claim("UserId", user.Id.ToString()),
            new Claim("Username", user.Username)
        };

        var jwtSecurityToken = new JwtSecurityToken(
            _configuration["Authentication:Issuer"],
            _configuration["Authentication:Audience"],
            claims,
            expires: DateTime.UtcNow.AddMinutes(10),
            signingCredentials: signingCredentials);

        var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        return Ok(token);
    }

    private async Task<User> ValidateUserCredentialsAsync(string username, string password)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
    }
}