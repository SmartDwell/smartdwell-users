using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Seljmov.AspNet.Commons.Options;
using Models;

namespace Server.Services.JwtHelper;

/// <summary>
/// Интерфейс для генерации Jwt-токена
/// </summary>
public interface IJwtHelper
{
    /// <summary>
    /// Создать токен доступа.
    /// </summary>
    /// <param name="user">Пользователь.</param>
    /// <param name="minutesValid">Время действия токена.</param>
    /// <returns>Токен доступа.</returns>
    string CreateAccessToken(User user, int minutesValid);

    /// <summary>
    /// Создать токен обновления.
    /// </summary>
    /// <returns>Токен обновления.</returns>
    string CreateRefreshToken();
}

/// <summary>
/// Класс для генерации Jwt-токена
/// </summary>
public class JwtHelper : IJwtHelper
{
    private const int RefreshTokenLength = 64;
    private readonly JwtOptions _jwtOptions;

    /// <summary>
    /// Конструктор класса <see cref="JwtHelper"/>
    /// </summary>
    /// <param name="jwtOptions">Настройки jwt</param>
    public JwtHelper(JwtOptions jwtOptions)
    {
        _jwtOptions = jwtOptions;
    }
    
    /// <inheritdoc cref="IJwtHelper.CreateAccessToken(User, int)" />
    public string CreateAccessToken(User user, int minutesValid)
    {
        var subject = new ClaimsIdentity(new[]
        {
            new Claim(ClaimsIdentity.DefaultIssuer, user.Id.ToString()),
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.FullName),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.MobilePhone, user.Phone),
        });

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _jwtOptions.Issuer,
            Audience = _jwtOptions.Audience,
            IssuedAt = DateTime.UtcNow,
            NotBefore = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddMinutes(minutesValid),
            Subject = subject,
            SigningCredentials = new SigningCredentials(_jwtOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256Signature)
        };
    
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    /// <inheritdoc cref="IJwtHelper.CreateRefreshToken" />
    public string CreateRefreshToken()
    {
        var token = RandomNumberGenerator.GetBytes(RefreshTokenLength);
        var bytes = Encoding.UTF8.GetBytes(Convert.ToBase64String(token));
        var hash = SHA256.HashData(bytes);
        return Convert.ToBase64String(hash);
    }
}
