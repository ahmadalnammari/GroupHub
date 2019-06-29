using GroupHub.Core.Domain;
using GroupHub.Core.DTO;
using GroupHub.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using static GroupHub.Core.Util;

namespace GroupHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : Controller
    {
        // GET api/values
        IConfiguration Configuration { get; set; }
        ISvcSecurity SvcSecurity { get; set; }
        ISvcUser SvcUser { get; set; }

        public TokenController(IConfiguration configuration, ISvcSecurity svcSecurity, ISvcUser svcUser)
        {

            Configuration = configuration;
            SvcUser = svcUser;
            SvcSecurity = svcSecurity;
        }

        [AllowAnonymous]
        [HttpPost("")]
        public IActionResult GetToken(string email, string password)
        {
            User user = SvcSecurity.Authenticate(email, Crypto.getHashSha256(password));

            if (user != null)
            {
                string access_token = getToken(user);
                if (access_token != null)
                    return Ok(new { access_token = access_token });
            }

            return BadRequest("Invalid Authentication");
        }







        private string getToken(User user)
        {

            List<Claim> claims = new List<Claim>();


            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(CustomClaimType.Email, user.Email));


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Security:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: Configuration["Info:Domain"],
                audience: Configuration["Info:Domain"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(1440),
                signingCredentials: creds);



            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}