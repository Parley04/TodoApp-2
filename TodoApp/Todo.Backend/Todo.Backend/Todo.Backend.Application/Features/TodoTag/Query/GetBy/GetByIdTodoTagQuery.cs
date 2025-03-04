using MediatR;
using Microsoft.EntityFrameworkCore;
using Todo.Backend.Domain.Repository;
using TS.Result;

namespace Todo.Backend.Application.Features.TodoTag.Query.GetBy
{
    public sealed record GetByIdTodoTagQuery(Guid Id) : IRequest<Result<Domain.Entities.TodoTag>>;

    internal sealed record GetByIdTodoTagQueryHandler(
            ITodoTagRepository todoTagRepository
        ) : IRequestHandler<GetByIdTodoTagQuery, Result<Domain.Entities.TodoTag>>
    {
        public async Task<Result<Domain.Entities.TodoTag>> Handle(GetByIdTodoTagQuery request, CancellationToken cancellationToken)
        {
            Domain.Entities.TodoTag? todoTag = await todoTagRepository
                .Where(x => x.Id == request.Id && x.IsActive == true)
                .FirstOrDefaultAsync(cancellationToken);
            if (todoTag == null)
            {
                return Result<Domain.Entities.TodoTag>.Failure("Todo's Tag was not found.");
            }
            return todoTag;
        }
    }
}
