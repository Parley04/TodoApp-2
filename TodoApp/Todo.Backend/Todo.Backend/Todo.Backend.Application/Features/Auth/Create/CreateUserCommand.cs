using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Todo.Backend.Application.Features.Auth.Login;
using Todo.Backend.Application.Services;
using Todo.Backend.Domain.Entities;
using TS.Result;

namespace Todo.Backend.Application.Features.Auth.Create;

public sealed record CreateUserCommand(
    string UserName,
    string Email,
    string FirstName,
    string LastName,
    string Password,
    bool IsActive = true
) : IRequest<Result<string>>;

internal sealed class CreateUserCommandHandler(
    UserManager<AppUser> userManager
) : IRequestHandler<CreateUserCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        bool isUserExist = await userManager.Users
            .AnyAsync(u => u.UserName == request.UserName || u.Email == request.Email, cancellationToken);
        if (isUserExist)
        {
            return Result<string>.Failure("This email already using.");
        }

        AppUser user = new()
        {
            UserName = request.UserName,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            IsActive = true
        };

        var result = await userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            return Result<string>.Failure(string.Join(", ", result.Errors.Select(e => e.Description)));
        }


        return Result<string>.Succeed("Register is Successful");
    }
}
