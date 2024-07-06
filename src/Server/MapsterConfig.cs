using Contracts.Users;
using Mapster;
using Models;

namespace Server;

internal static class MapsterConfig
{
    public static void Config()
    {
        TypeAdapterConfig<User, UserDto>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Surname, src => src.Surname)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.Phone, src => src.Phone);
        //.Map(dest => dest.Role, src => src.Role);
    }
}