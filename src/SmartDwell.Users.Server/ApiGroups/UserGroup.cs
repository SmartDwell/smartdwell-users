using Microsoft.AspNetCore.Mvc;
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
            .WithOpenApi();
        group.MapGet(RouteConstants.UserData.GetUserById, GetUserById)
            .WithName("GetUsersById")
            .WithOpenApi();
    }

    private static IResult GetUsers(DatabaseContext context)
    {
        return Results.Ok(context.Users.ToList());
    }
    
    private static async Task<IResult> GetUserById(DatabaseContext context, [FromRoute] Guid id)
    {
        var user = await context.Users.FindAsync(id);
        return user is null ? Results.NotFound() : Results.Ok(user);
    }
}