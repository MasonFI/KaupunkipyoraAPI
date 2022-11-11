using AutoMapper;
using KaupunkipyoraAPI.Contracts;
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
    public class LoginController : BaseController
    {
        public LoginController(IUnitOfWork uow,
            IMapper mapper,
            IOptionsMonitor<APIOptions> options) : base(uow, mapper, options) { }

        [HttpPost]
        [Produces("application/text")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            try
            {
                var user = await _UOW.UserRepository.GetByUsername(loginDTO.Username);

                if (user == null || user.Password != loginDTO.Password)
                {
                    return Unauthorized("Incorrect login");
                }

                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_APIOptions.JWT.SecretKey));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: _APIOptions.JWT.Issuer,
                    audience: _APIOptions.JWT.Audience,
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(_APIOptions.JWT.ExpirationInMinutes),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

                return Ok(new AuthenticatedResponseDTO { Token = tokenString });
            }
            catch (Exception ex)
            {
#if DEBUG
                return StatusCode(500, $"Exception: {ex.Message}");
#endif
                return StatusCode(500, "Exception");
            }
        }
    }
}
