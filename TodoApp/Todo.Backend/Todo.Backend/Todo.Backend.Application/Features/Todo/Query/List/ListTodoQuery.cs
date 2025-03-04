using MediatR;
using Microsoft.EntityFrameworkCore;
using Todo.Backend.Domain.Repositor;
using TS.Result;

namespace Todo.Backend.Application.Features.Todo.Query.List
{
    public sealed record ListTodoQuery(string UserId): IRequest<Result<List<Domain.Entities.Todo>>>;

    public sealed record ListTodoQueryHandler(
            ITodoRepository todoRepository
        ) : IRequestHandler<ListTodoQuery, Result<List<Domain.Entities.Todo>>>
    {
        public async Task<Result<List<Domain.Entities.Todo>>> Handle(ListTodoQuery request, CancellationToken cancellationToken)
        {
            List<Domain.Entities.Todo> todo = await todoRepository.GetAll()
                .Include(x => x.Tags)
                .OrderBy(x => x.CreatedDate)
                .Where(x => x.UserId == request.UserId && x.IsActive == true)
                .ToListAsync(cancellationToken);

            if (todo == null)
            {
                return Result<List<Domain.Entities.Todo>>.Failure("Todo not found.");
            }
            return todo;
        }
    }

}
