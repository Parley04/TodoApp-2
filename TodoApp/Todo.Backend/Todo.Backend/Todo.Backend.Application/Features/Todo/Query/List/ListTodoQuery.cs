using MediatR;
using Microsoft.EntityFrameworkCore;
using Todo.Backend.Domain.Dtos;
using Todo.Backend.Domain.Repositor;
using TS.Result;

namespace Todo.Backend.Application.Features.Todo.Query.List
{
    public sealed record ListTodoQuery(string UserId): IRequest<Result<List<TodoDto>>>;

    public sealed record ListTodoQueryHandler(
            ITodoRepository todoRepository
        ) : IRequestHandler<ListTodoQuery, Result<List<TodoDto>>>
    {
        public async Task<Result<List<TodoDto>>> Handle(ListTodoQuery request, CancellationToken cancellationToken)
        {
            var todos = await todoRepository
                .Where(x => x.UserId == request.UserId && x.IsActive)
                .OrderBy(x => x.CreatedDate)
                .Include(x => x.TodoTags)
                    .ThenInclude(tt => tt.Tag)
                .ToListAsync(cancellationToken);

            if (todos == null || !todos.Any())
            {
                return Result<List<TodoDto>>.Failure("Todo not found.");
            }

            var todoDtos = todos.Select(todo => new TodoDto
            {
                Id = todo.Id,
                Title = todo.Title,
                Description =todo.Description,
                BackgroundColor=todo.BackgroundColor,
                IsCompleted=todo.IsCompleted,
                Tags = todo.TodoTags.Select(tt => new TagDto
                {
                    Id = tt.Tag!.Id,
                    Name = tt.Tag.Name
                }).ToList()
            }).ToList();

            return Result<List<TodoDto>>.Succeed(todoDtos);
        }
    }

}
