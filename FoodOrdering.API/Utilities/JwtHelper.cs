using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FoodOrdering.API.Utilities
{
    public class JwtHelper
    {
        private readonly IConfiguration _configuration;

        public JwtHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(
            Guid userGuid,
            string role)
        {
            var claims = new[]
            {
                new Claim(
                    "UserGuid",
                    userGuid.ToString()),

                new Claim(
                    ClaimTypes.Role,
                    role)
            };

            var key =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        _configuration["JwtSettings:Key"]!));

            var credentials =
                new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256);

            var token =
                new JwtSecurityToken(
                    issuer:
                        _configuration["JwtSettings:Issuer"],

                    audience:
                        _configuration["JwtSettings:Audience"],

                    claims: claims,

                    expires:
                        DateTime.Now.AddMinutes(
                            Convert.ToDouble(
                                _configuration[
                                    "JwtSettings:ExpiryMinutes"])),

                    signingCredentials:
                        credentials);

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}