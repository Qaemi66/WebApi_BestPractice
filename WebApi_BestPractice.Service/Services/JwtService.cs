using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApi_BestPractice.Common;
using WebApi_BestPractice.Data.Repositories;
using WebApi_BestPractice.Domain.Entities;

namespace WebApi_BestPractice.Service.Services
{
    public class JwtService : IJwtService
    {
        private readonly SiteSettings siteSettings;
        private readonly SignInManager<User> signInManager;

        public JwtService(IOptionsSnapshot<SiteSettings> settings, SignInManager<User> signInManager)
        {
            this.siteSettings = settings.Value;
            this.signInManager = signInManager;
        }

        public async Task<string> GenerateAsync(User user, CancellationToken cancellationToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = GetClaimsAsync(user);

            var secretKey = Encoding.UTF8.GetBytes(siteSettings.jwtSettings.SecretKey);

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Audience = siteSettings.jwtSettings.Audience,
                Issuer = siteSettings.jwtSettings.Issuer,
                IssuedAt = DateTime.Now,
                Expires = DateTime.Now.AddMinutes(siteSettings.jwtSettings.ExpireMinutes),
                NotBefore = DateTime.Now.AddMinutes(siteSettings.jwtSettings.NotBeforeMinutes),
                SigningCredentials = signingCredentials,
                Subject = new ClaimsIdentity(await claims)
            };


            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            var jwt = tokenHandler.WriteToken(securityToken);

            return jwt;
        }

        private async Task<IEnumerable<Claim>> GetClaimsAsync(User user)
        {
            var result = await signInManager.ClaimsFactory.CreateAsync(user);
            return result.Claims;
        }
    }
}
