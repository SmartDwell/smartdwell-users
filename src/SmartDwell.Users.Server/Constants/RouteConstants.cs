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
}