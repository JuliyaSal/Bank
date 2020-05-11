using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EventStore.ClientAPI.SystemData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.Models.Services;
using WebApplication1.Models.Token;

namespace WebApplication1.Controllers
{
    [Route("api")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private AuthOptions _authOptions;

        public TokenController(IOptions<AuthOptions> authOptionsAcessor)
        {
            _authOptions = authOptionsAcessor.Value;
        }
        // GET: api/Token
        [HttpPost]
        [Route("token")]
        public IActionResult Post([FromBody] UserCredentials user)
        {
            var _service = new UserService();
            if (_service.IsValidUser(user.Username, user.Password))
            {
                var authClaims = new[]
                {
                    new Claim( JwtRegisteredClaimNames.Sub, user.Username),
                    new Claim( JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var token = new JwtSecurityToken(
                    _authOptions.Issuer,
                    _authOptions.Audience,
                    expires: DateTime.Now.AddHours(_authOptions.ExpiresInMinutes),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authOptions.SecureKey)),
                        SecurityAlgorithms.HmacSha256Signature)
                        );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expitation = token.ValidTo
                });
            }
            return Unauthorized();
        }

    }
}
