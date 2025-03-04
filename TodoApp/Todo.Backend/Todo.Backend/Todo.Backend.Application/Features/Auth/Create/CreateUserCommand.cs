using AutoMapper;
using GenericRepository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Todo.Backend.Application.Features.Auth.Login;
using Todo.Backend.Application.Services;
using Todo.Backend.Domain.Entities;
using TS.Result;

namespace Todo.Backend.Application.Features.Auth.Create
{
    public sealed record CreateUserCommand(
    string UserName,
    string Email,
    string FirstName,
    string LastName,
    string Password,
    bool IsActive = true
) : IRequest<Result<LoginCommandResponse>>;

    internal sealed class CreateUserCommandHandler(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        IJwtProvider jwtProvider
    ) : IRequestHandler<CreateUserCommand, Result<LoginCommandResponse>>
    {
        public async Task<Result<LoginCommandResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            bool isUserExist = await userManager.Users.AnyAsync(u => u.UserName == request.UserName || u.Email == request.Email, cancellationToken);
            if (isUserExist)
            {
                return Result<LoginCommandResponse>.Failure("Bu kullanıcı adı veya e-posta zaten kullanımda.");
            }

            AppUser user = new()
            {
                UserName = request.UserName,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                EmailConfirmed = true, 
                IsActive = true
            };

            var result = await userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                return Result<LoginCommandResponse>.Failure(string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            var signInResult = await signInManager.PasswordSignInAsync(user, request.Password, false, false);
            if (!signInResult.Succeeded)
            {
                return Result<LoginCommandResponse>.Failure("Kullanıcı oluşturuldu ama giriş yapılamadı.");
            }

            var loginResponse = await jwtProvider.CreateToken(user);

            return loginResponse;
        }
    }


}
