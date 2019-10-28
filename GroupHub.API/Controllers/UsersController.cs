
using GroupHub.Core.Domain;
using GroupHub.Core.DTO;
using GroupHub.Core.Services;
using GroupHub.Infra.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using static GroupHub.Core.Util;

namespace GroupHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        // GET api/values
        IConfiguration Configuration { get; set; }
        ISvcUser SvcUser { get; set; }

        public UsersController(IConfiguration configuration, ISvcUser svcUser)
        {
            SvcUser = svcUser;
            Configuration = configuration;
        }



        [HttpPost("register")]
        public async Task<User> Register([FromBody]UserRegistrationDTO userDTO)
        {
            var hashedPassword = Crypto.getHashSha256(userDTO.Password);

            User user = new User();
            user.Email = userDTO.Email;
            user.HashedPassword = hashedPassword;

            await SvcUser.Add(user);

            return user;
        }


        [HttpGet("profile")]
        [Authorize]
        public User GetProfile()
        {
           return SvcUser.Get(this.GetActiveUserId());
        }




    }
}