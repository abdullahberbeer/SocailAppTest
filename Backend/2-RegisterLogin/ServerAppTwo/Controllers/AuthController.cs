using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServerAppTwo.DTO;
using ServerAppTwo.Models;

namespace ServerAppTwo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController:ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        public AuthController(UserManager<User> userManager,SignInManager<User> signInManager,IConfiguration configuration)
        {
            _userManager=userManager;
            _signInManager=signInManager;
            _configuration=configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterForDTO model){
                if(!ModelState.IsValid){
 return BadRequest(ModelState);
                }
               

                var user = new User{
                    UserName=model.UserName,
                    Email=model.Email,
                    Name=model.Name,
                    Gender=model.Gender,
                    DateOfBirth=model.DateOfBirth,
                    Country=model.Country,
                    City=model.City,
                    Created=DateTime.Now,
                    LastActive=DateTime.Now
                    
                };

                var result = await _userManager.CreateAsync(user,model.Password);
                if(result.Succeeded){
                    return StatusCode(201);

                }
                return BadRequest("Hesap oluştururken bir hata oluştu");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginForDTO model){
               if(!ModelState.IsValid){
                    return BadRequest(ModelState);
               } 

               var user =await _userManager.FindByNameAsync(model.UserName);
               if(user==null){
                   return BadRequest("Böyle bir kullanıcı bulunamadı");
               }
               var result = await _signInManager.CheckPasswordSignInAsync(user,model.Password,true);

               if(result.Succeeded)
               {
                   return Ok( new {
                       token=GenerateJwtToken(user)
                   });
               }
               return Unauthorized();
        }

        private string GenerateJwtToken(User user)
        {
           var tokenHandler = new JwtSecurityTokenHandler();
            var key =Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Secret").Value);

            var tokenDescriptior = new SecurityTokenDescriptor{
                Subject=new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(ClaimTypes.Name,user.Name),
                    new Claim(ClaimTypes.Gender,user.Gender)

                }),
                Issuer ="akronsoft.online",
                Audience="",
                Expires=DateTime.UtcNow.AddDays(1),
                SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenHandler.CreateToken(tokenDescriptior);
            return tokenHandler.WriteToken(token);


        }
    }
}