using API.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Service
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration config;

        public AuthService(UserManager<IdentityUser> userManager,IConfiguration config)
        {
            this.userManager = userManager;
            this.config = config;
        }

        public string GenerateTokenString(LoginUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,user.UserName),
                new Claim(ClaimTypes.Role,"Admin")

            };
            var securityKey=new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(config.GetSection("Jwt:Key").Value));

            SigningCredentials signingCredentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha512Signature);
            var securityToken = new JwtSecurityToken(claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                issuer: config.GetSection("Jwt:Issuer").Value,
                audience: config.GetSection("Jwt:Audience").Value,
                signingCredentials: signingCredentials);
            string tokenString= new JwtSecurityTokenHandler().WriteToken(securityToken);
            return tokenString;

        }

        public async Task<bool> Login(LoginUser user)
        {
            var identityUser = await userManager.FindByEmailAsync(user.UserName);
            if(identityUser is null)
            {
                return false;
            }

            return await userManager.CheckPasswordAsync(identityUser, user.Password);
        }

        public async Task<bool> RegisterUser(LoginUser user)
        {
            IdentityUser AddUser= new IdentityUser(){
                UserName=user.UserName,
                Email=user.UserName
            };

            var result = await userManager.CreateAsync(AddUser,user.Password);
            return result.Succeeded;
        }

    }
}
