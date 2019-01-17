using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace JWTSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        [HttpGet("GetToken")]
        public IActionResult GetToken()
        {
            string secretKey = "This is my secret key, no body will know it";

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var signingCredential = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, "Administrator"),
                new Claim("taolao", "taolaovalue")
            };

            var token = new JwtSecurityToken(
                issuer: "ModernShopping.org",
                audience: "All api clients",
                expires: DateTime.Now.AddHours(1),
                signingCredentials: signingCredential,
                claims: claims
            );

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}