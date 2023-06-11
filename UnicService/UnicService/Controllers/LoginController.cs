using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ModelUtil.Contexts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using UnicService.ViewModels.Login;
using ModelUtil.Repositories.UnicBase;
using ModelUtil.Logger;
using Azure.Core;
using ModelUtil;
using UnicService.Filters;

namespace UnicService.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class LoginController : BaseController
    {
        public IConfiguration _configuration;
        private readonly IUnicBaseRepository _unicBaseRepo;

        public LoginController(IUniLogger logger, IConfiguration config, IUnicBaseRepository unicBaseRepo)
            : base(logger)
        {
            _configuration = config;
            _unicBaseRepo = unicBaseRepo;
        }

        [ExcludeFilter]
        [HttpPost]
        public async Task<IActionResult> GetToken(UserLoginParams _userData)
        {
            try
            {
                // If user data params are valid.
                if (_userData != null && _userData.AreAllPropertiesNotNull())
                {
                    // Get user information.
                    var user = await GetUser(_userData.Email);
                    // Verify the password matches the hashed password.
                    if (user.Item1.HasValue && user.Item1 != Guid.Empty && user.Item2.HasValue && !String.IsNullOrEmpty(user.Item3) && BCrypt.Net.BCrypt.Verify(_userData.Password, user.Item3))
                    {
                        // Create claims details based on the user information.
                        var claims = new[] {
                            new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                            new Claim(nameof(UserClaim.UserId), user.Item1.ToString()),
                            new Claim(nameof(UserClaim.UserRole), user.Item2.ToString()),
                        };
                        // Construct token.
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            _configuration["Jwt:Issuer"],
                            _configuration["Jwt:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Jwt:ExpiresMinutes"])),
                            signingCredentials: signIn);
                        // Send token to client.
                        return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                    }
                    else
                    {
                        return BadRequest("Invalid credentials");
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                sendError(message: "Error unable to process login request", exception: ex, parameters: new Dictionary<string, string> { { "Email", _userData?.Email } });
                return StatusCode(500);
            }
        }

        private async Task<(Guid?, UserRole?, string?)> GetUser(string email)
        {
            return await _unicBaseRepo.GetUserId_PasswordHashed(email: email);
        }
    }
}
