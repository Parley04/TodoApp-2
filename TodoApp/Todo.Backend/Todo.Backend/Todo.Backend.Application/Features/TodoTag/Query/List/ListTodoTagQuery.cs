using MediatR;
using Todo.Backend.Domain.Dtos;
using TS.Result;

namespace Todo.Backend.Application.Features.TodoTag.Query.List
{
    public sealed record ListTodoTagQuery(string UserId): IRequest<Result<List<TodoTagDto>>>;

}
