using Microsoft.EntityFrameworkCore;
using Models;

namespace Server;

/// <summary>
/// Контекст базы данных
/// </summary>
public sealed class DatabaseContext : DbContext
{
    #region Tables

    /// <summary>
    /// Коллекция тикетов авторизации
    /// </summary>
    public DbSet<AuthTicket> AuthTickets { get; init; } = null!;
    
    /// <summary>
    /// Коллекция пользователей
    /// </summary>
    public DbSet<User> Users { get; init; } = null!;

    #endregion
    
    /// <summary>
    /// Конструктор по умолчанию
    /// </summary>
    public DatabaseContext() { }
    
    /// <summary>
    /// Конструктор с параметрами
    /// </summary>
    /// <param name="options">Параметры</param>
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) 
    {
        if (Database.GetPendingMigrations().Any())
            Database.Migrate();
    }

    /// <inheritdoc cref="DbContext.OnModelCreating(ModelBuilder)"/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuthTicket>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Code).IsRequired();
            entity.Property(e => e.Login).IsRequired();
            entity.Property(e => e.ExpiresAt).IsRequired();
            entity.Property(e => e.IsUsed).IsRequired().HasDefaultValue(false);
        });
        
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Surname).IsRequired();
            entity.Property(e => e.Patronymic);
            entity.Property(e => e.Phone).IsRequired();
            entity.Property(e => e.Email).IsRequired();
            //entity.Property(e => e.Role).IsRequired();
            entity.Property(e => e.Note);
            entity.Property(e => e.RefreshToken);
            entity.Property(e => e.RefreshTokenExpires);
            entity.Property(e => e.IsBlocked).IsRequired().HasDefaultValue(false);
            entity.Property(e => e.BlockReason);
            entity.Property(e => e.Created).IsRequired().HasDefaultValueSql("now()");
            
            entity.HasData(new List<User>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    //Role = UserRole.Resident,
                    Note = "Создано автоматически",
                    Phone = "79887897788",
                    Email = "17moron@bk.ru",
                    Name = "Джон",
                    Surname = "Уик",
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    //Role = UserRole.Resident,
                    Note = "Создано автоматически",
                    Phone = "79887893311",
                    Email = "guest@example.com",
                    Name = "Джон",
                    Surname = "Уик",
                }
            });
        });
    }
}