using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using taskmanagerback.Models;

namespace taskmanagerback.Controllers
{
    [Route("api/auth")]
    [ApiController]
    [Authorize]

    public class AuthController : ControllerBase
    {
        private IConfiguration _config;
        private TaskManagerContext _context;

        public AuthController(IConfiguration config,TaskManagerContext context)
        {
            _config = config;
            _context = context;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] User user)
        {
            var postUser = user;

            if (Authentificate(user) != null)
            {
                return new JsonResult(new { token = GenerateJWTToken(user), user = user });
            }

            return new UnauthorizedResult();
        }


        [HttpGet()]
       public IActionResult getAllUsers()
        {
            return new JsonResult(_context.users);
        }
        private User Authentificate(User cred)
        {
            var user = _context.users.FirstOrDefault(user => user.Login == cred.Login && user.Password == cred.Password);

            return user;
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
new Claim("fullName", userInfo.Login.ToString()),
new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
};
            var token = new JwtSecurityToken(
            issuer: issuer,
            audience: issuer,
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }



    }
}
