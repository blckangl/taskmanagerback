using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using taskmanagerback.Models;

namespace taskmanagerback.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IConfiguration _config;

        private List<User> users = new List<User>(new[] { new User() { UserId = 0, Login = "user", Password = "123" } });
        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody]User user)
        {
            if (Authentificate(user) != null)
            {
                return new JsonResult(new { token = GenerateJWTToken(user), user = user });
            }

            return new UnauthorizedResult();
        }


        private User Authentificate(User cred)
        {
           return users.FirstOrDefault(user => user.Login == cred.Login && user.Password == cred.Password);
        }


        string GenerateJWTToken(User userInfo)
        {
            var issuer = _config.GetSection("Jwt")["Issuer"];
            var secretKey = _config.GetSection("Jwt")["SecretKey"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Login),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };


      
            var token = new JwtSecurityToken(
            issuer: issuer,
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
