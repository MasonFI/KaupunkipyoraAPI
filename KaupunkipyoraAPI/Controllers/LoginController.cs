using KaupunkipyoraAPI.Models.DTO;
using KaupunkipyoraAPI.Models.Entity;
using KaupunkipyoraAPI.Services.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace KaupunkipyoraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class LoginController : ControllerBase
    {
        private APIOptions _APIOptions;
        private List<User> GetUsers()
        {
            return new List<User>()
            {
                new User()
                {
                    Username = "joonas.manninen",
                    Password = "test1234",
                    IncorrectLoginCount = 0,
                },
                new User()
                {
                    Username = "joonas.manninen.locked",
                    Password = "test1234",
                    IncorrectLoginCount = 4,
                },
            };
        }

        public LoginController(IOptionsMonitor<APIOptions> options)
        {
            _APIOptions = options.CurrentValue;
        }

        [HttpPost]
        [Produces("application/text")]
        public IActionResult Login(LoginDTO loginDTO)
        {
            try
            {
                var user = GetUsers().Where(x => x.Username == loginDTO.Username).FirstOrDefault();

                if (user == null)
                {
                    return Unauthorized("Incorrect login");
                }

                if (user.Password != loginDTO.Password)
                {
                    user.IncorrectLoginCount++;
                    if (user.IncorrectLoginCount > 4)
                    {
                        return Unauthorized("Incorrect login");
                    }

                    return Unauthorized("Incorrect login");
                }

                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_APIOptions.JWT.SecretKey));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var jwtSecurityToken = new JwtSecurityToken(
                    issuer: _APIOptions.JWT.Issuer,
                    audience: _APIOptions.JWT.Audience,
                    expires: DateTime.Now.AddMinutes(_APIOptions.JWT.ExpirationInMinutes),
                    signingCredentials: signinCredentials
                );

                return Ok(new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken));
            }
            catch
            {
                return BadRequest("Incorrect login");
            }
        }
    }
}
