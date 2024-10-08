namespace Contracts.Auth;

/// <summary>
/// Модель данных для подтверждения авторизации.
/// </summary>
public class AuthVerifyCodeDto
{
    /// <summary>
    /// Тикет.
    /// </summary>
    public string TicketId { get; set; } = string.Empty;
    
    /// <summary>
    /// Код подтверждения.
    /// </summary>
    public string Code { get; set; } = string.Empty;
}