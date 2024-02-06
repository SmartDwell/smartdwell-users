using SmartDwell.Contracts.Users;

namespace SmartDwell.Contracts.Auth;

/// <summary>
/// Модель завершения авторизации.
/// </summary>
public class AuthCompletedDto
{
    /// <summary>
    /// Данные токенов.
    /// </summary>
    public TokensDto Tokens { get; set; } = new();
    
    /// <summary>
    /// Данные пользователя.
    /// </summary>
    public UserDto User { get; set; } = new();
}