namespace Models;

/// <summary>
/// Роль пользователя.
/// </summary>
public enum UserRole
{
    /// <summary>
    /// Житель.
    /// </summary>
    Resident,
    
    /// <summary>
    /// Администратор.
    /// </summary>
    Admin,
    
    /// <summary>
    /// Менеджер.
    /// </summary>
    Manager,
    
    /// <summary>
    /// Диспетчер.
    /// </summary>
    Dispatcher,
    
    /// <summary>
    /// Выездной специалист.
    /// </summary>
    FieldSpecialist
}