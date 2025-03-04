using Todo.Backend.Application.Features.Auth.Login;
using Todo.Backend.Domain.Entities;

namespace Todo.Backend.Application.Services;
public interface IJwtProvider
{
    Task<LoginCommandResponse> CreateToken(AppUser user);
}
