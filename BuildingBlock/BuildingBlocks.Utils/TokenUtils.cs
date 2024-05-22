using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BuildingBlocks.Utils;

public record TokenArgs(Guid Id,string Email,string FirstName,string LastName);

public static class TokenUtils
{
    private static string JWT_KEY = "your_secret_key_here_your_secret_key_here";
    private static string JWT_ISSUER = "your_issuer";
    private static string JWT_AUDIENCE = "your_audience";
    public static string CreateToken(TokenArgs tokenArgs)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, tokenArgs.Id.ToString()),
                new Claim(ClaimTypes.Email, tokenArgs.Email),
                new Claim(ClaimTypes.Name, tokenArgs.FirstName),
                new Claim(ClaimTypes.Surname, tokenArgs.LastName)
            }),
            Expires = DateTime.UtcNow.AddHours(24),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JWT_KEY)), SecurityAlgorithms.HmacSha256Signature),
            Issuer = JWT_ISSUER,
            Audience = JWT_AUDIENCE
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public static async Task<TokenValidationResult> ValidateToken(string authToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParams = new TokenValidationParameters
        {
            ValidateLifetime = true,
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidIssuer = JWT_ISSUER,
            ValidAudience = JWT_AUDIENCE,
            ValidateIssuerSigningKey = true,
            //ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JWT_KEY))
        };
        SecurityToken _;
        return await tokenHandler.ValidateTokenAsync(authToken, validationParams);
    }
}
