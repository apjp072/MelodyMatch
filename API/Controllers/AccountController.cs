using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace API.Controllers
{
    public class AccountController : BaseApiController 
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public AccountController(DataContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        } 
        
        [HttpPost("register")] // POST: api/account/register
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto) //[ApiController] automatically binds to objects inside [RegisterDto]
        {
            if (await UserExists(registerDto.Username)) return BadRequest("Username is taken"); //check if the username already exists

            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                UserName = registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)), //create password hash
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user); //adds user to current context

            await _context.SaveChangesAsync(); //save changes into database

            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user) 
            };
        }

            [HttpPost("login")]
            public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username);
                
                if (user == null) return Unauthorized("invalid username or password");

                using var hmac = new HMACSHA512(user.PasswordSalt);

                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

                for(int i = 0; i < computedHash.Length; i++)
                {
                    if(computedHash[i] != user.PasswordHash[i]) return Unauthorized("invalid username or password");
                }
                
                return new UserDto
                { 
                    Username = user.UserName,
                    Token = _tokenService.CreateToken(user) 
                };
            }
            private async Task<bool> UserExists(string username)
            {
                return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
                //need await as its an async function 
                //any async allows us to iterate through all User objects
            }
    }
}


