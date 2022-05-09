using Identity.Domain;
using Identity.Persistence.Database;
using Identity.Service.EventHandlers.Commands;
using Identity.Service.EventHandlers.Response;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Service.EventHandlers
{
    public class UserLoginEventHandler :
        IRequestHandler<UserLoginCommand, IdentityAccess>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        //private readonly IConfiguration _configuration;
        private readonly IConfiguration _configuration;


        public UserLoginEventHandler(
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context,
            /*IConfiguration configuration*/
            IConfiguration configuration)
        {
            _signInManager = signInManager;
            _context = context;
            //_configuration = configuration;
            _configuration = configuration;
        }

        public async Task<IdentityAccess> Handle(UserLoginCommand notification, CancellationToken cancellationToken)
        {
            var result = new IdentityAccess();

            var user = await _context.Users.SingleAsync(x => x.Email == notification.Email);
            var response = await _signInManager.CheckPasswordSignInAsync(user, notification.Password, false);

            if (response.Succeeded)
            {
                result.Succeeded = true;
                await GenerateToken(user, result);
            }

            return result;
        }

        
        private async Task GenerateToken(ApplicationUser user, IdentityAccess identity)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName)
            };

            var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["secretKey"]));
            var key = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var roles = await _context.Roles
                                      .Where(x => x.UserRoles.Any(y => y.UserId == user.Id))
                                      .ToListAsync();

            foreach (var role in roles)
            {
                claims.Add(
                    new Claim(ClaimTypes.Role, role.Name)
                );
            }

            var expiration = DateTime.UtcNow.AddHours(2);//token expira en 1 hora 

            JwtSecurityToken tokenDescriptor = new JwtSecurityToken(
               claims: claims,
               expires: expiration,
               signingCredentials: key);

           identity.AccessToken = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
