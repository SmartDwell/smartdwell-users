using SmartDwell.Users.Server.Constants;

namespace SmartDwell.Users.Server.ApiGroups;

public static class UserGroup
{
    public static void MapUserGroup(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup(RouteConstants.UserData.Route);
    }
}