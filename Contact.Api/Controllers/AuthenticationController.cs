using Contact.Core.Enitties;
using Contact.Infraestructure.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;
        public AuthenticationController(UserManager<ApplicationUser> userManager
            , RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] User model)
        {
            var userExist = await userManager.FindByNameAsync(model.UserName);
            if (userExist != null)
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new /*Response*/ { Status = "Error", Message = "User Already Exist" });

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName
            };

            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                //return Ok(new  { Status = "Success", Message = "User Successfully" });
                var token = GenerateToken(model);
                return Ok(new { token });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError
                    , new { Status = "Error", Message = "User Creation Failed" });
            }            
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] User model)
        {
            var user = await userManager.FindByNameAsync(model.UserName);
            if (user != null
                && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var token = GenerateToken(model);
                return Ok(new { token });

            }
            return Unauthorized();
        }

        private string GenerateToken(User login)
        {
            //Header
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);

            //Claims
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, login.UserName),
                new Claim("User", login.Email),
                //new Claim(ClaimTypes.Role, "Admin"),
            };

            //Payload
            var payload = new JwtPayload
            (
                configuration["JWT:Issuer"],
                configuration["JWT:Audience"],
                claims,
                DateTime.Now,
                DateTime.UtcNow.AddMinutes(10)
            );

            var token = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
