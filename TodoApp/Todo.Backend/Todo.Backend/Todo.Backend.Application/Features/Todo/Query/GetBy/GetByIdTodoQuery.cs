using MediatR;
using Microsoft.EntityFrameworkCore;
using Todo.Backend.Domain.Dtos;
using Todo.Backend.Domain.Repositor;
using TS.Result;

namespace Todo.Backend.Application.Features.Todo.Query.GetBy
{
    public sealed record GetByIdTodoQuery(Guid Id) : IRequest<Result<TodoDto>>;

    internal sealed record GetByIdTodoQueryHandler(
                ITodoRepository todoRepository
            ) : IRequestHandler<GetByIdTodoQuery, Result<TodoDto>>
    {
        public async Task<Result<TodoDto>> Handle(GetByIdTodoQuery request, CancellationToken cancellationToken)
        {
            var todo = await todoRepository
               .Where(x => x.Id == request.Id && x.IsActive)
               .Include(x => x.TodoTags)
                   .ThenInclude(tt => tt.Tag)
               .FirstOrDefaultAsync(cancellationToken);

            if (todo == null)
            {
                return Result<TodoDto>.Failure("Todo not found.");
            }

            var todoDto = new TodoDto
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                BackgroundColor = todo.BackgroundColor,
                IsCompleted = todo.IsCompleted,
                Tags = todo.TodoTags
                    .Where(tt => tt.IsActive) 
                    .Select(tt => new TagDto
                    {
                        Id = tt.Tag!.Id,
                        Name = tt.Tag.Name
                    }).ToList()
            };

            return Result<TodoDto>.Succeed(todoDto);
        }
    }
}
