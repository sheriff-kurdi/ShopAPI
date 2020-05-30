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
using Microsoft.IdentityModel.Tokens;
using shop.InfraStructure.Data;
using shop.Core.Entities;
using shop.Services.Auth;

namespace shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly ITokenAuth tokenAuth;

        public UsersController(AppDbContext context, ITokenAuth tokenAuth)
        {
            this.context = context;
            this.tokenAuth = tokenAuth;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserLoginVM userModel)
        {
            //var token = tokenAuth.Authenticate(userModel.UserName, userModel.Password);
            //if (token == null) return BadRequest(new { Message = "UserName or Password Invalid" });
            //return Ok(token);

            IQueryable<User> user = context.users.Where(u => u.UserName == userModel.UserName && u.Password == userModel.Password);

            if (user.FirstOrDefault() == null)
            {
                return Unauthorized();
            }

            UserVM userVM = new UserVM()
            {
                UserName = user.FirstOrDefault().UserName,
                picUrl = user.FirstOrDefault().picUrl,
                Token = tokenAuth.GenerateToken()
            };
            // return Ok(new { token = tokenAuth.GenerateToken()  });
            return Ok(userVM);

        }

        [AllowAnonymous]
        [HttpPost]
        public  IActionResult Register(User user) 
        {
            context.users.Add(user);
            context.SaveChanges();

            UserVM userVM = new UserVM()
            {
                UserName = user.UserName,
                picUrl = user.picUrl,
                Token = tokenAuth.GenerateToken()
            };

            return Ok(userVM);
        }


    }
}