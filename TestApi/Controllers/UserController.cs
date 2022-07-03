using Application.DTO;
using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly JWTSettings _jwtsettings;
        public UserController(ApplicationDbContext context, IOptions<JWTSettings> jwtsettings)
        {
            _context = context;
            _jwtsettings = jwtsettings.Value;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<Response<int>>> Register(UserDTO userDTO)
        {
            var userInfo = new UserInfo
            {
                Username = userDTO.Username,
                Password = userDTO.Password
            };

            await _context.UserInfo.AddAsync(userInfo);
            CancellationToken cancellationToken = new CancellationToken();
            await _context.SaveChangesAsync(cancellationToken);
            return new Response<int>(userInfo.Id, "User created successfully");
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserWithToken>> Login(UserDTO userDTO)
        {
            var user = await _context.UserInfo.Where(u => u.Username == userDTO.Username
                                                && u.Password == userDTO.Password).FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtsettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim(ClaimTypes.Name, user.Username)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            UserWithToken userWithToken = new UserWithToken
            {
                Username = user.Username,
                Token = tokenHandler.WriteToken(token)
            };

            return userWithToken;
        }
    }
}
