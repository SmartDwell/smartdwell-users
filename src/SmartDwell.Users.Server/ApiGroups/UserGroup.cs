using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using SmartDwell.Users.Server.Constants;

namespace SmartDwell.Users.Server.ApiGroups;

/// <summary>
/// Группа методов для работы с пользователями
/// </summary>
public static class UserGroup
{
    /// <summary>
    /// Определение маршрутов работы с пользователями
    /// </summary>
    /// <param name="endpoints">Маршруты</param>
    public static void MapUserGroup(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup(RouteConstants.UserData.Route);
        group.MapGet(RouteConstants.UserData.GetUsers, GetUsers)
            .WithName("GetUsers")
            .WithSummary("Получение списка пользователей")
            .WithOpenApi();
        group.MapGet(RouteConstants.UserData.GetUserById, GetUserById)
            .WithName("GetUsersById")
            .WithSummary("Получение пользователя по идентификатору")
            .WithOpenApi();
    }

    private static IResult GetUsers(DatabaseContext context)
    {
        return TypedResults.Ok(context.Users.ToList());
    }
    
    private static async Task<IResult> GetUserById(DatabaseContext context, [FromRoute] Guid id)
    {
        var user = await context.Users.FindAsync(id);
        return user is null ? TypedResults.NotFound() : TypedResults.Ok(user);
    }
}