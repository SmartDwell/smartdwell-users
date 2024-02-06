namespace SmartDwell.Users.Server.Constants;

/// <summary>
/// Константы маршрутов
/// </summary>
public static class RouteConstants
{
    /// <summary>
    /// Данные пользователя
    /// </summary>
    public static class UserData
    {
        /// <summary>
        /// Базовый маршрут
        /// </summary>
        public const string Route = "/api/users";

        /// <summary>
        /// Получение списка пользователей
        /// </summary>
        public const string GetUsers = "/";

        /// <summary>
        /// Получение пользователя по идентификатору
        /// </summary>
        public const string GetUserById = "/{id:guid}";
    }

    /// <summary>
    /// Данные аутентификации
    /// </summary>
    public static class AuthData
    {
        /// <summary>
        /// Базовый маршрут
        /// </summary>
        public const string Route = "/api/auth";
        
        /// <summary>
        /// Начало аутентификации
        /// </summary>
        public const string Start = "/start";
        
        /// <summary>
        /// Завершение аутентификации
        /// </summary>
        public const string Complete = "/complete";
        
        /// <summary>
        /// Обновление токенов
        /// </summary>
        public const string Refresh = "/refresh";
    }
}