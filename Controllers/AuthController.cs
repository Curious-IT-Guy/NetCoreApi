using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using NetCoreApi.Data.Entities;
using NetCoreApi.Data.Models;
using System.Security.Claims;
using NetCoreApi.Helpers;
using System.Text;
using System;

namespace NetCoreApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public AuthController(IConfiguration config, ApplicationDbContext context)
        {
            _configuration = config;
            _context = context;
        }


        [HttpPost("/login")]
        public async Task<IActionResult> Login(User user) => await Authenticate(user);


        [HttpPost("/register")]
        public async Task<IActionResult> Register(User user)
        {
            if (user.Firstname != null && user.Lastname != null && user.Email != null && user.Password != null)
            {
                if (await UserExists(user.Email))
                {
                    return BadRequest("Already exist!");
                }
                else
                {
                    user.Password = Password.Encrypt(user.Email, user.Password);

                    _context.User.Add(user);
                    _context.SaveChanges();

                    return Ok("User Added");
                }
            }
            else
            {
                return BadRequest();
            }
        }


        private async Task<IActionResult> Authenticate(User user)
        {
            if (user.Email != null && user.Password != null)
            {
                User u = await GetUser(user.Email, user.Password);

                if (u != null)
                {
                    var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Id", u.Id.ToString()),
                    new Claim("Firstname", u.Firstname),
                    new Claim("Lastname", u.Lastname),
                    new Claim("Email", u.Email)
                   };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }


        private async Task<User> GetUser(string email, string password)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Email == email && u.Password == Password.Encrypt(email, password));
        }

        private async Task<bool> UserExists(string email)
        {
            User user = await _context.User.FirstOrDefaultAsync(u => u.Email == email);
            return user != null;
        }
    }
}