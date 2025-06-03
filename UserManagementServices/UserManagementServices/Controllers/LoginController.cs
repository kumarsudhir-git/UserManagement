using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserManagementServices.BO.Interfaces;
using UserManagementServices.Models;

namespace UserManagementServices.Controllers
{
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserManagementBO _userManagementBO;
        private readonly JwtSettings _jwtSettings;
        public LoginController(IUserManagementBO userManagementBO, IOptions<JwtSettings> jwtOptions)
        {
            _userManagementBO = userManagementBO;
            _jwtSettings = jwtOptions.Value;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/login")]
        public async Task<IActionResult> Login([FromBody] LoginMaster login)
        {
            if (login == null || string.IsNullOrEmpty(login.UserName) || string.IsNullOrEmpty(login.Password))
            {
                return BadRequest("Username or Password can not be null");
            }
            // In real scenario, validate username/password from DB
            UserMaster userMasterDTO = await _userManagementBO.ValidateUserAsync(login.UserName, login.Password);

            if (userMasterDTO == null)
            {
                return Unauthorized();
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);

            var claims = new[]
            {
            new Claim(ClaimTypes.Name, login.UserName),
            new Claim(ClaimTypes.Role, userMasterDTO.RoleName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Ok(new { token = tokenHandler.WriteToken(token) });
        }
    }
}
