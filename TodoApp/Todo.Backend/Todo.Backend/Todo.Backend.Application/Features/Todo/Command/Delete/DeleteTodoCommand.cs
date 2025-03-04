using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Todo.Backend.Domain.Repositor;
using TS.Result;

namespace Todo.Backend.Application.Features.Todo.Command.Delete
{
    public sealed record DeleteTodoCommand
        (
        Guid Id,
        DateTime DeletedDate,
        bool IsActive = false
        ): IRequest<Result<string>>;

    internal sealed class DeleteTodoCommandHandler
        (
            ITodoRepository todoRepository,
            IUnitOfWork unitOfWork
        ) : IRequestHandler<DeleteTodoCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Todo? todo = await todoRepository
                .WhereWithTracking(t => t.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (todo == null)
            {
                return Result<string>.Failure("Todo not found");
            }

            todo.IsActive = false;
            todo.DeletedDate = DateTime.UtcNow;

            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<string>.Succeed(todo.Id.ToString());
        }
    }
}
