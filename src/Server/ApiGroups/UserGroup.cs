using Contracts.Users;
using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Seljmov.Blazor.Identity.Shared;
using RouteConstants = Server.Constants.RouteConstants;

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
            .RequireAuthorization(AuthPolicies.UsersPolicy);
        group.MapGet(RouteConstants.UserData.Users, GetUsers)
            .Produces<UserDto[]>()
            .WithName("GetUsers")
            .WithSummary("Получение списка пользователей.")
            .WithOpenApi();
        group.MapGet(RouteConstants.UserData.UserById, GetUserById)
            .Produces<UserDto>()
            .WithName("GetUsersById")
            .WithSummary("Получение пользователя по идентификатору.")
            .WithOpenApi();
    }

    private static Ok<UserDto[]> GetUsers(DatabaseContext context)
    {
        var users = context.Users
            .Include(user => user.Role)
            .Adapt<UserDto[]>();
        return TypedResults.Ok(users);
    }
    
    private static async Task<IResult> GetUserById(DatabaseContext context, [FromRoute] Guid id)
    {
        var user = await context.Users
            .Include(user => user.Role)
            .FirstOrDefaultAsync(user => user.Id == id);
        return user is null ? TypedResults.NotFound() : TypedResults.Ok(user.Adapt<UserDto>());
    }
}
