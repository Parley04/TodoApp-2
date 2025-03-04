using MediatR;
using Microsoft.EntityFrameworkCore;
using Todo.Backend.Domain.Repositor;
using TS.Result;

namespace Todo.Backend.Application.Features.Todo.Query.GetBy
{
    public sealed record GetByIdTodoQuery(Guid Id): IRequest<Result<Domain.Entities.Todo>>;

    internal sealed record GetByIdTodoQueryHandler(
            ITodoRepository todoRepository
        ) : IRequestHandler<GetByIdTodoQuery, Result<Domain.Entities.Todo>>
    {
        public async Task<Result<Domain.Entities.Todo>> Handle(GetByIdTodoQuery request, CancellationToken cancellationToken)
        {
            Domain.Entities.Todo? todo = await todoRepository
                .Where(x => x.Id == request.Id && x.IsActive == true)
                .FirstOrDefaultAsync(cancellationToken);
            if (todo == null)
            {
                return Result<Domain.Entities.Todo>.Failure("Todo was not found.");
            }
            return todo;
        }
    }   
}
