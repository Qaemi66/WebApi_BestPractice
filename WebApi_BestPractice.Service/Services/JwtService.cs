using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApi_BestPractice.Common;
using WebApi_BestPractice.Data.Contracts;
using WebApi_BestPractice.Data.Repositories;
using WebApi_BestPractice.Domain.Etities;

namespace WebApi_BestPractice.Service.Services
{
    public class JwtService : IJwtService
    {
        private readonly IUserRoleRepository UserRoleRepository;
        private readonly SiteSettings siteSettings;

        public JwtService(IUserRoleRepository claimRepository, SiteSettings siteSettings)
        {
            this.UserRoleRepository = claimRepository;
            this.siteSettings = siteSettings;
        }

        public async Task<string> GenerateAsync(User user, CancellationToken cancellationToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = getClaimsAsync(user, cancellationToken);

            var secretKey = Encoding.UTF8.GetBytes(siteSettings.jwtSettings.SecretKey);

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Audience = siteSettings.jwtSettings.Audience,
                IssuedAt = DateTime.Now,
                Expires = DateTime.Now.AddMinutes(siteSettings.jwtSettings.ExpireMinutes),
                NotBefore = DateTime.Now.AddMinutes(siteSettings.jwtSettings.NotBeforeMinutes),
                Issuer = siteSettings.jwtSettings.Issuer,
                SigningCredentials = signingCredentials,
                Subject = new System.Security.Claims.ClaimsIdentity(await claims)
            };


            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            var jwt = tokenHandler.WriteToken(securityToken);

            return jwt;
        }

        private async Task<IEnumerable<Claim>> getClaimsAsync(User user, CancellationToken cancellationToken)
        {
            var list = new List<Claim>();

            list.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            list.Add(new Claim(ClaimTypes.Name, user.UserName));
            list.Add(new Claim(ClaimTypes.GivenName, user.FullName));

            await UserRoleRepository.GetRoleAsync(user, cancellationToken);

            return list;
        }
    }
}
