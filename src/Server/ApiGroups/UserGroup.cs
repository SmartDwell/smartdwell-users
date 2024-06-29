using Contracts.Users;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Models;
using Server.Constants;

namespace Server.ApiGroups;

/// <summary>
/// Группа методов для работы с пользователями.
/// </summary>
public static class UserGroup
{
    /// <summary>
    /// Определение маршрутов работы с пользователями.
    /// </summary>
    /// <param name="endpoints">Маршруты.</param>
    public static void MapUserGroup(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints
            .MapGroup(RouteConstants.UserData.Route)
            .RequireAuthorization();
        group.MapGet(RouteConstants.UserData.Users, GetUsers)
            .Produces<UserDto[]>()
            .WithName("GetUsers")
            .WithSummary("Получение списка пользователей")
            .WithOpenApi();
        group.MapGet(RouteConstants.UserData.UserById, GetUserById)
            .Produces<UserDto>()
            .WithName("GetUsersById")
            .WithSummary("Получение пользователя по идентификатору")
            .WithOpenApi();
    }

    private static IResult GetUsers(DatabaseContext context)
    {
        return TypedResults.Ok(context.Users.Adapt<UserDto[]>());
    }
    
    private static async Task<IResult> GetUserById(DatabaseContext context, [FromRoute] Guid id)
    {
        var user = await context.Users.FindAsync(id);
        return user is null ? TypedResults.NotFound() : TypedResults.Ok(user.Adapt<UserDto>());
    }
}