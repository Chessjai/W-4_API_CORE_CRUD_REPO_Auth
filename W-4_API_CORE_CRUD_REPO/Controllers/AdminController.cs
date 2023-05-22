using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace W_4_API_CORE_CRUD_REPO.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        public class User
        {
            public string Username { get; set; }

            public string Password { get; set; }
        }
        public AdminController(ILogger<AdminController> logger)
        {
        }

        [HttpPost]
        public IActionResult Login([FromBody] User User)
        {
            // TODO: Authenticate Admin with Database
            // If not authenticate return 401 Unauthorized
            // Else continue with below flow

            var Claims = new List<Claim>
            {
                new Claim("type", "Admin"),
            };

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SXkSqsKyNUyvGbnHs7ke2NCq8zQzNLW7mPmHbnZZ"));

            var Token = new JwtSecurityToken(
                "https://fbi-demo.com",
                "https://fbi-demo.com",
                Claims,
                expires: DateTime.Now.AddDays(30.0),
                signingCredentials: new SigningCredentials(Key, SecurityAlgorithms.HmacSha256)
            );

            return new OkObjectResult(new JwtSecurityTokenHandler().WriteToken(Token));
        }

    }
}