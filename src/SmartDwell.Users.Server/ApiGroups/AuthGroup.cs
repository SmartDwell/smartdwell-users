using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Seljmov.AspNet.Commons.Options;
using SmartDwell.Contracts.Auth;
using SmartDwell.Contracts.Users;
using SmartDwell.Models;
using SmartDwell.Users.Server.Constants;
using SmartDwell.Users.Server.Services;
using SmartDwell.Users.Server.Services.JwtHelper;

namespace SmartDwell.Users.Server.ApiGroups;

/// <summary>
/// Группа методов работы с аутентификацией.
/// </summary>
public static class AuthGroup
{
    /// <summary>
    /// Определение маршрутов аутентификации.
    /// </summary>
    /// <param name="endpoints">Маршруты.</param>
    public static void MapAuthGroup(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup(RouteConstants.AuthData.Route);
        group.MapPost(RouteConstants.AuthData.Start, Start)
            .WithName("StartAuth")
            .WithSummary("Начало аутентификации")
            .WithOpenApi();
        group.MapPost(RouteConstants.AuthData.Complete, Complete)
            .WithName("CompleteAuth")
            .WithSummary("Завершение аутентификации")
            .WithOpenApi();
    }

    private static async Task<IResult> Start(DatabaseContext context,
        [FromBody] AuthRequestCodeDto requestCodeDto,
        [FromServices] IEmailCodeSender emailCodeSender)
    {
        var codeSender = requestCodeDto.AuthLoginType switch
        {
            AuthLoginType.Email => emailCodeSender,
            _ => null,
        };

        if (codeSender is null)
            return TypedResults.StatusCode(StatusCodes.Status501NotImplemented);

        if (string.IsNullOrEmpty(requestCodeDto.Login))
            return TypedResults.BadRequest($"Поле {nameof(requestCodeDto.Login)} не может быть пустым");

        var user = await context.Users.FirstOrDefaultAsync(c =>
            c.Phone == requestCodeDto.Login || c.Email == requestCodeDto.Login);
        if (user is null)
            return TypedResults.NotFound($"Пользователь с логином {requestCodeDto.Login} не найден");

        try
        {
            var ticket = AuthTicket.Create(requestCodeDto.Login);
            await codeSender.Send(ticket);
            await context.AuthTickets.AddAsync(ticket);
            await context.SaveChangesAsync();
            return TypedResults.Ok(new TicketDto
            {
                Name = user.Name,
                TicketId = ticket.Id.ToString()
            });
        }
        catch (Exception ex)
        {
            return TypedResults.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    private static async Task<IResult> Complete(DatabaseContext context,
        [FromBody] AuthVerifyCodeDto verifyCodeDto,
        [FromServices] IJwtHelper jwtHelper,
        [FromServices] JwtOptions jwtOptions)
    {
        if (string.IsNullOrEmpty(verifyCodeDto.TicketId) ||
            !Guid.TryParse(verifyCodeDto.TicketId, out var ticketId) ||
            string.IsNullOrEmpty(verifyCodeDto.Code))
            return TypedResults.BadRequest($"Некорректный тикет или код");

        var ticket = context.AuthTickets.FirstOrDefault(t => t.Id == ticketId);
        if (ticket is null)
            return TypedResults.NotFound($"Не найден тикет. TicketId: {ticketId}");

        if (ticket.Code != verifyCodeDto.Code)
            return TypedResults.Conflict($"Код не совпадает. Code: {verifyCodeDto.Code}");

        var user = context.Users.FirstOrDefault(c => c.Phone == ticket.Login || c.Email == ticket.Login);
        if (user is null)
            return TypedResults.NotFound($"Пользователь с логином {ticket.Login} не найден");

        user.RefreshToken = jwtHelper.CreateRefreshToken();
        user.RefreshTokenExpires = DateTime.UtcNow.AddMinutes(jwtOptions.RefreshTokenLifetime);
        context.Users.Update(user);
        context.AuthTickets.Remove(ticket);
        await context.SaveChangesAsync();

        var authCompletedDto = new AuthCompletedDto
        {
            Tokens = new TokensDto
            {
                AccessToken = jwtHelper.CreateAccessToken(user, jwtOptions.AccessTokenLifetime),
                RefreshToken = user.RefreshToken,
            },
            User = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Phone = user.Phone,
                Name = user.Name,
                Surname = user.Surname,
            },
        };
        return TypedResults.Ok(authCompletedDto);
    }
}