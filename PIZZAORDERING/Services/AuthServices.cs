
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PIZZAORDERING.Dtos;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace PIZZAORDERING.Services;

public class AuthServices : Controller
{
    public readonly JwtSettings __jwt;
    public AuthServices(IOptions<JwtSettings> options)
    {
        __jwt = options.Value;
    }

    public string GenerateToken(string name,string email, string role)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name,name),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(__jwt.SecretKey));

        var Credentails = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expiryTime = DateTime.UtcNow.AddMinutes(__jwt.ExpiryMinutes);

        var token = new JwtSecurityToken(
            issuer: __jwt.Issuer,
            audience: __jwt.Audience,
            expires: expiryTime,
            claims: claims,
            signingCredentials: Credentails
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefresh()
    {
        var randonbyte = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randonbyte);
        return Convert.ToBase64String(randonbyte);
    }
    
    public DateTime GetRefreshTokenExpiry()
    {
        return DateTime.UtcNow.AddDays(__jwt.RefreshTokenExpiryTime);
    }
}