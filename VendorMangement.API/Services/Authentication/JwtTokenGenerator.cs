﻿using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace VendorMangement.API.Services.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        public string GenerateJwtToken(Guid userId, string firstName, string lastName)
        {
            var signingCredentials = new SigningCredentials(
                  new SymmetricSecurityKey(
                      Encoding.UTF8.GetBytes("vendor-portal-se")),
                  SecurityAlgorithms.HmacSha256
                );
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,userId.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName,firstName),
                new Claim(JwtRegisteredClaimNames.FamilyName,lastName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            };

            var securityToken = new JwtSecurityToken(
                issuer: "VendorPortalAPI",
                expires: DateTime.UtcNow.AddDays(1),
                claims: claims,
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}
