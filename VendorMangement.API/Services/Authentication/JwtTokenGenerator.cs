using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace VendorMangement.API.Services.Authentication
{
    public struct CustomJwtRegisteredClaimNames
    {
        public const string PartnerId = "PartnerId";

        public const string Partner = "Partner";

        public const string RoleId = "RoleId";

        public const string Role = "Role";
    }
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtSettings _jwtSettings;

        public JwtTokenGenerator(IOptions<JwtSettings> jwtOptions)
        {
            _jwtSettings = jwtOptions.Value;
        }
        public string GenerateJwtToken(Guid userId, string displayName, Guid partnerid, Guid roleid,
            string partner, string role)
        {
            var signingCredentials = new SigningCredentials(
                  new SymmetricSecurityKey(
                      Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
                  SecurityAlgorithms.HmacSha256
                );
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,userId.ToString()),
                new Claim(JwtRegisteredClaimNames.FamilyName,displayName),
                new Claim(CustomJwtRegisteredClaimNames.RoleId,roleid.ToString()),
                new Claim(CustomJwtRegisteredClaimNames.PartnerId,partnerid.ToString()),
                 new Claim(CustomJwtRegisteredClaimNames.Role,role),
                new Claim(CustomJwtRegisteredClaimNames.Partner,partner),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            };

            var securityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                expires: DateTime.UtcNow.AddDays(_jwtSettings.ExpiryDays),
                claims: claims,
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}
